using UnityEngine;

public class Pin : MonoBehaviour
{
    public bool interactable = true;
    public Gate connected;
    public int connectionIndex;
    public PowerState powerState = PowerState.LOW;
    public PinType pinType;
}