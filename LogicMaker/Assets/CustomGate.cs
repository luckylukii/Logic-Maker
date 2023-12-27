using System.Collections.Generic;
using UnityEngine;
public class CustomGate : Gate
{
    public Dictionary<Gate, int> gates = new();
    public bool[] Execute(params bool[] inputs)
    {
        if (inputs.Length != NumInputs) throw new System.ArgumentException("The length of the input array is not the correct length", nameof(inputs));
        

        return new bool[inputs.Length];
    }
}