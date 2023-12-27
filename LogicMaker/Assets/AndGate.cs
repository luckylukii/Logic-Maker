using UnityEngine;

public class AndGate : Gate
{
    public bool Execute(bool in1, bool in2) => in1 & in2;
}