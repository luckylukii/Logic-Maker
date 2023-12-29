using System.Collections.Generic;
using UnityEngine;

public class ChipDraggingUI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GatePreview previewPrefab;
    [SerializeField] private List<Gate> gates = new();

    private void Awake() => Refresh();

    public void AddChip(Gate chip) => gates.Add(chip);
    public void Refresh()
    {
        //Destroy children
        while (parent.childCount > 0)
        {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }

        foreach (var chip in gates)
        {
            var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GatePreview prev = Instantiate(previewPrefab, mousePos, Quaternion.identity, parent);
            prev.nameText.text = chip.chipName;
            prev.associatedPrefab = chip.GetComponent<Dragable>();
        }
    }
}