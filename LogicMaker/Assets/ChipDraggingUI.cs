using System.Collections.Generic;
using UnityEngine;

public class ChipDraggingUI : MonoBehaviour
{
    [SerializeField] private Transform uiParent;
    [SerializeField] private Transform chipParent;
    [SerializeField] private GatePreview previewPrefab;
    [SerializeField] private List<Gate> gates = new();

    public Transform ChipParent
    {
        get => chipParent;
    }

    private void Awake() => Refresh();

    public void AddChip(Gate chip)
    {
        gates.Add(chip);
        Refresh();
    }
    private void Refresh()
    {
        //Destroy children
        while (uiParent.childCount > 0)
        {
            DestroyImmediate(uiParent.GetChild(0).gameObject);
        }

        foreach (Gate chip in gates)
        {
            GatePreview prev = Instantiate(previewPrefab, uiParent);
            prev.nameText.text = chip.chipName;
            prev.associatedPrefab = chip;
            prev.parent = chipParent;
        }
    }
}