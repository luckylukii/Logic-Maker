using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomGate : Gate
{
    private const float PIN_DISTRIBUTION_LENGTH = 0.5f;

    private const float PIN_INPUT_POSITION_X = -1;
    private const float PIN_OUTPUT_POSITION_X = 1;

    private const float GRAPHIC_SIZE_MIN = 0.75f;

    //Serialized Fields for assigning on prefab
    [SerializeField] private Transform graphicsObject;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject pinPrefab;

    public Dictionary<PowerState[], PowerState[]> thruthTable;

    private bool initialized = false;

    private int numInputPins = 0;
    private int numOutputPins = 0;

    private int biggerSide;

    public void Init(int numInputPins, int numOutputPins, string name, Dictionary<PowerState[], PowerState[]> thruthTable)
    {
        nameText.text = name;

        this.thruthTable = new(thruthTable, new PowerStateComparer());

        this.numInputPins = numInputPins;
        this.numOutputPins = numOutputPins;

        biggerSide = Mathf.Max(numInputPins, numOutputPins);

        FitSize();
        InitPins();

        initialized = true;
    }
    private void FitSize()
    {
        Vector3 newScale = graphicsObject.transform.localScale;

        newScale.y = Mathf.Max(GRAPHIC_SIZE_MIN, CalculateYPosOffset(biggerSide));

        graphicsObject.transform.localScale = newScale;
    }
    private void InitPins()
    {
        float inputPinYStartPos = CalculateYPosOffset(numInputPins - 1) / 2;

        float outputPinYStartPos = CalculateYPosOffset(numOutputPins - 1) / 2;

        pins = new Pin[numInputPins + numOutputPins];

        for (int i = 0; i < numInputPins + numOutputPins; i++)
        {
            //Values
            pins[i] = Instantiate(pinPrefab, transform).GetComponent<Pin>();

            bool isInputPin = i < numInputPins;

            pins[i].connected = this;
            pins[i].connectionIndex = i;

            pins[i].pinType = isInputPin ? PinType.Input : PinType.Output;

            //Positions
            float start = isInputPin ? inputPinYStartPos : outputPinYStartPos;

            float y = start - (isInputPin ? CalculateYPosOffset(i) : CalculateYPosOffset(i - numInputPins));

            float xPos = isInputPin ? PIN_INPUT_POSITION_X : PIN_OUTPUT_POSITION_X;
            pins[i].transform.localPosition = new Vector2(xPos, y);
        }
    }
    private float CalculateYPosOffset(float i) => i * PIN_DISTRIBUTION_LENGTH;

    private void Update()
    {
        if (!initialized)
            return;

        PowerState[] currentInput = new PowerState[numInputPins];
        for (int i = 0; i < numInputPins; i++)
        {
            currentInput[i] = pins[i].powerState;
        }

        for (int i = 0; i < numOutputPins; i++)
        {
            try
            {
                pins[i + numInputPins].powerState = thruthTable[currentInput][i];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning("Thruth table generation not complete");
            }
        }
    }
}
class PowerStateComparer : IEqualityComparer<PowerState[]>
{
    public bool Equals(PowerState[] obj1, PowerState[] obj2)
    {
        return StructuralComparisons.StructuralEqualityComparer.Equals(obj1, obj2);
    }

    public int GetHashCode(PowerState[] obj)
    {
        return StructuralComparisons.StructuralEqualityComparer.GetHashCode(obj);
    }
}