using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private Gate connected;
    [SerializeField] private int connectionIndex;
    public PowerState powerState = PowerState.LOW;
    public PinType pinType;

    private void Awake()
    {
        connected.pins[connectionIndex] = this;
    }
}