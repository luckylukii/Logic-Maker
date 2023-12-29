using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Toggle : Gate
{
    public PowerState powerState;

    private void OnMouseDown()
    {
        powerState = powerState switch
        {
            PowerState.HIGH => PowerState.LOW,
            PowerState.LOW => PowerState.HIGH,
            _ => PowerState.LOW
        };
        pins[0].powerState = powerState;
    }
}