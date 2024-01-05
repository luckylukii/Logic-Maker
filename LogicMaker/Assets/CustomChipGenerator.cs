using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CustomChipGenerator : MonoBehaviour
{
    #region Singleton Reference

    public static CustomChipGenerator Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public List<Toggle> inputs = new();
    public List<OutputLight> outputs = new();

    [SerializeField] private CustomGate prefab;
    public ChipDraggingUI UI;
    private string chipName = "Unnamed";
    public void CreateBuiltChip()
    {
        if (inputs.Count == 0 || outputs.Count == 0)
        {
            Debug.LogWarning("No inputs/outputs found. Chip will not be created");
            return;
        }

        CustomGate inst = Instantiate(prefab, UI.ChipParent);
        inst.Init(inputs.ToArray(), outputs.ToArray(), chipName);
        UI.AddChip(inst);
    }

    public void SetChipName(string s)
    {
        chipName = s;
    }
}