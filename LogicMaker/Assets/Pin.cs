using UnityEngine;

public class Pin : MonoBehaviour
{
    // TODO: Convert every pin a connecting pin so that wires won't be having that much responsibility
    public bool interactable = true;
    public Gate connected;
    public int connectionIndex;
    public PowerState powerState = PowerState.LOW;
    public PinType pinType;
}