using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CustomGate : Gate
{
    private const float PIN_DISTRIBUTION_LENGTH = 0.5f;

    private const float PIN_INPUT_POSITION_X = -1;
    private const float PIN_OUTPUT_POSITION_X = 1;

    private const float GRAPHIC_SIZE_MIN = 0.75f;

    private const float COLLIDER_SCALE_REDUCTION_X = 0.3f;

    [NonSerialized] new public ConnectingPin[] pins;

    [SerializeField] private Transform graphicsObject;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private ConnectingPin pinPrefab;

    private int numInputPins = 0;
    private int numOutputPins = 0;
    private Toggle[] inputs;
    private OutputLight[] outputs;

    public void Init(Toggle[] inputs, OutputLight[] outputs, string name)
    {
        nameText.text = name;
        chipName = name;

        numInputPins = inputs.Length;
        numOutputPins = outputs.Length;

        this.inputs = inputs;
        this.outputs = outputs;

        FitSize();
        InitPins();
        MoveGates(); // This needs to be called after InitPins()
        ResetChipOutIn();
    }
    private void MoveGates()
    {
        Converter<ConnectingPin, SpriteRenderer> conv = new((c) => c.GetComponent<SpriteRenderer>());
        SpriteRenderer[] renderers = Array.ConvertAll(pins, conv);
        base.renderers.AddRange(renderers);

        // hacky solution but I can't be bothered to find a good way
        // unity transforms are weird
        while (transform.parent.childCount > 1)
        {
            foreach (Transform child in transform.parent)
            {
                child.GetComponent<ComponentToggler>().SetGraphicsActive(false);

                child.SetParent(transform);
            }
        }
    }
    private void FitSize()
    {
        Vector3 newScale = graphicsObject.transform.localScale;

        newScale.y = Mathf.Max(GRAPHIC_SIZE_MIN, CalculateYPosOffset(Mathf.Max(numInputPins, numOutputPins)));

        Vector2 colliderScale = newScale;
        colliderScale.x -= COLLIDER_SCALE_REDUCTION_X;

        graphicsObject.transform.localScale = newScale;
        GetComponent<BoxCollider2D>().size = colliderScale;
    }
    private void ResetChipOutIn()
    {
        var gen = CustomChipGenerator.Instance;
        gen.inputs.Clear();
        gen.outputs.Clear();
    }
    private void InitPins()
    {
        float inputPinYStartPos = CalculateYPosOffset(numInputPins - 1) / 2;
        float outputPinYStartPos = CalculateYPosOffset(numOutputPins - 1) / 2;

        pins = new ConnectingPin[numInputPins + numOutputPins];

        for (int i = 0; i < numInputPins + numOutputPins; i++)
        {
            pins[i] = Instantiate(pinPrefab, transform);

            bool isInputPin = i < numInputPins;

            pins[i].connectionIndex = i;

            if (isInputPin)
            {
                inputs[i].IsInGate = true;

                pins[i].connected = inputs[i].hiddenInput;
                pins[i].pinType= PinType.Input;

                float y = inputPinYStartPos + CalculateYPosOffset(i);
                float x = PIN_INPUT_POSITION_X;

                pins[i].transform.localPosition = new Vector2(x, y);
            }
            else
            {
                int j = i - numInputPins;

                outputs[j].IsInGate = true;

                pins[i].connected = outputs[j].hiddenOutput;
                pins[i].pinType = PinType.Output;

                float y = outputPinYStartPos + CalculateYPosOffset(j);
                float x = PIN_OUTPUT_POSITION_X;

                pins[i].transform.localPosition = new Vector2(x, y);
            }
        }
    }
    private float CalculateYPosOffset(float i) => i * PIN_DISTRIBUTION_LENGTH;
}