using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line linePrefab;

    private const float SNAP_DISTANCE = 0.3f;

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

        if (Input.GetKey(KeyCode.LeftControl)) HandleErasing();
        else HandleLines();
    }

    private void HandleLines()
    {
        GameObject nearestPin = NearestPinFromMousePos(out float distance);

        if (nearestPin == null) return;

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
        try
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
        catch (System.IndexOutOfRangeException)
        {
            distance = 0f;
            return null;
        }
        
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

    private void HandleErasing()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        //save performance by returning if left mouse not pressed
        float nearestDistance = float.MaxValue;
        GameObject nearestLine = null;
        
        foreach (var line in GameObject.FindGameObjectsWithTag("Wire"))
        {
            var rend = line.GetComponent<LineRenderer>();
            for (int i = 0; i < rend.positionCount; i++)
            {
                Vector3 position = rend.GetPosition(i);

                float dist = Vector2.Distance(mousePos, position);
                if (dist < nearestDistance)
                { 
                    nearestDistance = dist;
                    nearestLine = line;
                }
            }
        }

        if (nearestDistance <= SNAP_DISTANCE) Destroy(nearestLine);
    }
}