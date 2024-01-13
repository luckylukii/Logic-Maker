using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    public Pin start;
    public Pin end;

    [HideInNormalInspector] public PowerState powerState;

    private void Update()
    {
        if (start != null) powerState = start.powerState;
        if (end != null) end.powerState = powerState;
    }

    public void ChangeFirstPosition(Vector2 pos) => ChangePosition(0, pos);
    public void ChangeLastPosition(Vector2 pos) => ChangePosition(_renderer.positionCount - 1, pos);
    public void ChangePosition(int index, Vector2 pos) => _renderer.SetPosition(index, pos);
    public void NewPosition(Vector2 pos)
    {
        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);
    }

    public void TrySetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);
    }
    private bool CanAppend(Vector2 pos)
    {
        if (_renderer.positionCount == 0) return true;

        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) >= DrawManager.RESOLUTION;
    }
}
