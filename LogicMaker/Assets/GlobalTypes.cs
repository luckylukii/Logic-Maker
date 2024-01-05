using System.Collections.Generic;
using UnityEngine;

public enum PowerState { LOW = 0, HIGH = 1 }
public enum PinType { Input = 0, Output = 1 }
public class ToggleableGraphics : MonoBehaviour
{
    [SerializeField] protected List<SpriteRenderer> renderers;

    public virtual void SetGraphicsActive(bool enabled)
    {
        foreach (SpriteRenderer rend in renderers)
        {
            rend.enabled = enabled;
        }
    }
}