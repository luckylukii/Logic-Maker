using UnityEngine;

public class Gate : ComponentToggler
{
    public Pin[] pins;
    public string chipName;

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            RemoveSceneInOut();
            Destroy(gameObject);
        }
    }
    private void RemoveSceneInOut()
    {
        if (TryGetComponent<Toggle>(out var t)) CustomChipGenerator.Instance.inputs.Remove(t);
        else if (TryGetComponent<OutputLight>(out var l)) CustomChipGenerator.Instance.outputs.Remove(l);
    }

    public override void SetGraphicsActive(bool enabled)
    {
        base.SetGraphicsActive(enabled);
        foreach (var pin in pins)
        {
            pin.interactable = false;
        }
    }
}