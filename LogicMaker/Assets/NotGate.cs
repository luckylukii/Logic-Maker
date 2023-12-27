using UnityEngine;

public class NotGate : Gate
{
    public bool Execute(bool input) => !input;
}