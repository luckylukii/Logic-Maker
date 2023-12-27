public class NotGate : Gate
{
    public void Update()
    {
        pins[1].powerState = pins[0].powerState switch
        {
            PowerState.LOW => PowerState.HIGH,
            PowerState.HIGH => PowerState.LOW,
            _ => PowerState.LOW
        };
    }
}