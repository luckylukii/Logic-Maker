using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private const float SNAP_DISTANCE = 0.15f;
    public const float RESOLUTION = 0.1f;


    [SerializeField] private bool useVertexDraw = true;
    private Camera _cam;
    [SerializeField] private Line linePrefab;

    private Line _currentLine;

    Vector2 mousePos;

    public void Awake()
    {
        _cam = Camera.main;     
    }

    private void Update()
    {

        mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftControl)) HandleErasing();
        else if (useVertexDraw) HandleVertexDrawing();
        else HandleDrawing();
    }

    private bool isDrawing = false;
    private void HandleVertexDrawing()
    {
        GameObject nearestPin = NearestPinFromMousePos(out float distance);

        if (nearestPin == null) return; // if there are no pins


        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) // Cancel a line with right click or esc
        {
            CancelCurrentLine(true);
            return;
        }

        // TODO: unnest this mess
        if (Input.GetMouseButtonDown(0))
        {
            if (!isDrawing) // if starting a line
            {
                if (distance > SNAP_DISTANCE) return;

                isDrawing = true;
                _currentLine = Instantiate(linePrefab, nearestPin.transform.position, Quaternion.identity);
                _currentLine.NewPosition(nearestPin.transform.position); // will always set the position
                _currentLine.NewPosition(mousePos); // only for previewing

                SetCurrentLinePin(nearestPin.GetComponent<Pin>());
            }
            else if (_currentLine != null) // if already having a in-progress line
            {
                if (distance <= SNAP_DISTANCE) // If trying to connect to a pin
                {
                    SetCurrentLinePin(nearestPin.GetComponent<Pin>());

                    // Checks if start or end is null (that means either two inputs or two ouputs are connected)
                    bool invalidWire = _currentLine.start == null || _currentLine.end == null;

                    if (invalidWire || !nearestPin.GetComponent<Pin>().interactable)
                    {
                        CancelCurrentLine(true);
                        return;
                    }

                    _currentLine.NewPosition(nearestPin.transform.position);
                    CancelCurrentLine(false);
                    return;
                }
                _currentLine.NewPosition(mousePos);
            }
        }

        // Handle previews
        if (_currentLine != null) _currentLine.ChangeLastPosition(mousePos);
    }

    private void HandleDrawing()
    {
        GameObject nearestPin = NearestPinFromMousePos(out float distance);

        if (nearestPin == null) return;

        if (Input.GetMouseButtonDown(0) && nearestPin.GetComponent<Pin>().interactable)
        {
            if (distance <= SNAP_DISTANCE)
            {
                _currentLine = Instantiate(linePrefab, nearestPin.transform.position, Quaternion.identity);
                _currentLine.TrySetPosition(nearestPin.transform.position);
                SetCurrentLinePin(nearestPin.GetComponent<Pin>());
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

            if (distance > SNAP_DISTANCE || invalidWire || !nearestPin.GetComponent<Pin>().interactable)
            {
                CancelCurrentLine(true);
                return;
            }

            _currentLine.TrySetPosition(nearestPin.transform.position);
            CancelCurrentLine(false);
        }
    }

    private void CancelCurrentLine(bool destroy)
    {
        if (destroy) Destroy(_currentLine.gameObject);
        _currentLine = null;
        isDrawing = false;
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
        //save performance by returning if left mouse not pressed
        if (!Input.GetMouseButtonDown(0)) return;

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