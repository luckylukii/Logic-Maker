public class ConnectingPin : Pin
{
    public Pin connectedPin;
    new public PowerState powerState
    {
        get => connectedPin.powerState;
        set => connectedPin.powerState = value;
    }
}