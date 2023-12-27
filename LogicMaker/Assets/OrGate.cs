public class OrGate : Gate
{
    public void Update() => pins[2].powerState = (PowerState)((int)pins[0].powerState | (int)pins[1].powerState);
}