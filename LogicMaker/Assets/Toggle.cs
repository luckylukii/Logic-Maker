using UnityEngine;
public class Toggle : Gate
{
    [SerializeField] private OutputLight oLight;
    public PowerState powerState;

    private void OnMouseDown()
    {
        switch (powerState)
        {
            case PowerState.HIGH:
                powerState = PowerState.LOW;
                break;
            case PowerState.LOW:
                powerState = PowerState.HIGH;
                break;
        }
        pins[0].powerState = powerState;
    }
}