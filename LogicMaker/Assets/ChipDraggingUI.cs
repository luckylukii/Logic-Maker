using System.Collections.Generic;
using UnityEngine;

public class ChipDraggingUI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GatePreview previewPrefab;
    [SerializeField] private List<Gate> gates = new();

    private void Awake() => Refresh();

    public void AddChip(Gate chip)
    {
        gates.Add(chip);
        Refresh();
    }
    private void Refresh()
    {
        //Destroy children
        while (parent.childCount > 0)
        {
            DestroyImmediate(parent.GetChild(0).gameObject);
        }

        foreach (var chip in gates)
        {
            GatePreview prev = Instantiate(previewPrefab, parent);
            prev.nameText.text = chip.chipName;
            prev.associatedPrefab = chip;
        }
    }
}