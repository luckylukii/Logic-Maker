public class ConnectingPin : Pin
{
    [System.NonSerialized] new public Pin connected;
    new public PowerState powerState
    {
        get => connected.powerState;
        set => connected.powerState = value;
    }
}