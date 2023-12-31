using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CustomChipGenerator : MonoBehaviour
{
    [SerializeField] private CustomGate prefab;
    [SerializeField] private ChipDraggingUI UI;
    private string chipName = "Unnamed";
    public void CreateBuiltChip()
    {
        GameObject[] toggleGOs = GameObject.FindGameObjectsWithTag("Toggle");
        GameObject[] lightGOs = GameObject.FindGameObjectsWithTag("Light");

        if (toggleGOs.Length == 0 || lightGOs.Length == 0)
        {
            Debug.LogWarning("No inputs/outputs found. Chip will not be created");
            return;
        }

        Toggle[] toggles = Array.ConvertAll(toggleGOs, (GameObject g) => g.GetComponent<Toggle>());
        OutputLight[] lights = Array.ConvertAll(toggleGOs, (GameObject g) => g.GetComponent<OutputLight>());

        Generate(toggles, lights);
    }
    private async void Generate(Toggle[] inputGates, OutputLight[] outputGates)
    {
        Dictionary<PowerState[], PowerState[]> thruthTable = new();

        foreach (var combination in GetAllPossibleCombinations(new PowerState[inputGates.Length]))
        {
            for (int i = 0; i < combination.Length; i++)
            {
                inputGates[i].pins[0].powerState = combination[i];
            }
            await Task.Delay(10);

            PowerState[] results = new PowerState[outputGates.Length];
            for (int i = 0; i < outputGates.Length; i++)
            {
                results[i] = outputGates[i].pins[0].powerState;
            }
            thruthTable.Add(combination, results);
        }

        CustomGate inst = Instantiate(prefab);
        inst.Init(inputGates.Length, outputGates.Length, chipName, thruthTable);
        UI.AddChip(inst);
    }

    //This method is AI Generated
    private PowerState[][] GetAllPossibleCombinations(PowerState[] prefix, int idx = 0)
    {
        List<PowerState[]> result = new();

        if (idx == prefix.Length)
        {
            result.Add((PowerState[])prefix.Clone());
        }
        else
        {
            prefix[idx] = PowerState.LOW;
            result.AddRange(GetAllPossibleCombinations(prefix, idx + 1));
            prefix[idx] = PowerState.HIGH;
            result.AddRange(GetAllPossibleCombinations(prefix, idx + 1));
        }

        return result.ToArray();
    }

    public void SetChipName(string s)
    {
        chipName = s;
    }
}