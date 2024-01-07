using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PowerState { LOW = 0, HIGH = 1 }
public enum PinType { Input = 0, Output = 1 }
public class ComponentToggler : MonoBehaviour
{
    [SerializeField] protected List<SpriteRenderer> renderers = new();
    [SerializeField] private List<TMP_Text> textElements = new();
    [SerializeField] private List<Collider2D> colliders = new();
    public virtual void SetGraphicsActive(bool enabled)
    {
        foreach (var r in renderers)
        {
            r.enabled = enabled;
        }
        foreach (var t in textElements)
        {
            t.enabled = enabled;
        }
        foreach (var c in colliders)
        {
            c.enabled = enabled;
        }
    }
}