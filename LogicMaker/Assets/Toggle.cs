using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Toggle : Gate
{
    public PowerState powerState;
    public Pin hiddenInput;
    public bool IsInGate
    {
        get => isInGate;
        set
        {
            isInGate = value;
            GetComponent<OutputLight>().IsInGate = value;
        }
    }
    private bool isInGate = false;

    private void OnMouseDown()
    {
        if (isInGate) return;

        powerState = powerState switch
        {
            PowerState.HIGH => PowerState.LOW,
            PowerState.LOW => PowerState.HIGH,
            _ => PowerState.LOW
        };
        pins[0].powerState = powerState;
    }
}