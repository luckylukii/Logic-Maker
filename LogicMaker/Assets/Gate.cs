using UnityEngine;

public class Gate : MonoBehaviour
{
    public int NumInputs;
    public int NumOutputs;

    public void ChangeInputNum(int num) => NumInputs = num;
    public void ChangeOutputNum(int num) => NumOutputs = num;
}