using UnityEngine;
using TMPro;
using System.Linq;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line linePrefab;

    private const float SNAP_DISTANCE = 0.6f;

    public const float RESOLUTION = 0.1f;

    private Line _currentLine;

    Vector2 mousePos;
    GameObject startPin;

    public void Awake()
    {
        _cam = Camera.main;     
    }

    private void Update()
    {
        mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        HandleLines();
    }

    public void HandleLines()
    {
        GameObject nearestPin = NearestPinFromMousePos(out float distance);

        if (Input.GetMouseButtonDown(0))
        {
            if (distance <= SNAP_DISTANCE)
            {
                _currentLine = Instantiate(linePrefab, nearestPin.transform.position, Quaternion.identity);
                _currentLine.TrySetPosition(nearestPin.transform.position);
                SetCurrentLinePin(nearestPin.GetComponent<Pin>());
                startPin = nearestPin;
            }
            else
            {
                _currentLine = null;
                return;
            }
        }

        if (Input.GetMouseButton(0) && _currentLine != null) _currentLine.TrySetPosition(mousePos);

        if (Input.GetMouseButtonUp(0) && _currentLine != null)
        {
            SetCurrentLinePin(nearestPin.GetComponent<Pin>());

            //Checks if start or end is null (that means either two inputs or two ouputs are connected)
            bool invalidWire = _currentLine.start == null || _currentLine.end == null;

            if (distance > SNAP_DISTANCE || nearestPin == startPin || invalidWire)
            {
                CancelCurrentLine();
                return;
            }

            _currentLine.TrySetPosition(nearestPin.transform.position);
        }
    }

    private void CancelCurrentLine()
    {
        Destroy(_currentLine.gameObject);
        _currentLine = null;
    }

    private GameObject NearestPinFromMousePos(out float distance)
    {
        GameObject current = GameObject.FindGameObjectsWithTag("Pin")[0];
        distance = Vector2.Distance(mousePos, current.transform.position);
        foreach (var pin in GameObject.FindGameObjectsWithTag("Pin"))
        {
            float dist = Vector2.Distance(mousePos, pin.transform.position);
            if (dist < distance)
            {
                current = pin;
                distance = dist;
            }
        }

        return current;
    }
    private void SetCurrentLinePin(Pin pin)
    {
        if (pin.pinType == PinType.Input)
        {
            _currentLine.end = pin;
        }
        else if (pin.pinType == PinType.Output)
        {
            _currentLine.start = pin;
        }
    }
}