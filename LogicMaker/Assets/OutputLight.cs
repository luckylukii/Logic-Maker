using UnityEngine;

public class OutputLight : Gate
{
    [SerializeField] private SpriteRenderer image;
    public Pin hiddenOutput;
    public bool IsInGate = false;

    private void Update()
    {
        if (IsInGate)
        {
            hiddenOutput.powerState = pins[0].powerState;
            return;
        }

        var gameManager = GameManager.Instance;
        //For simplicity, we just take the first color of the gradient, since all of them should be the same
        image.color = pins[0].powerState == PowerState.HIGH ? gameManager.highPowerGrad.colorKeys[0].color : gameManager.lowPowerGrad.colorKeys[0].color;
    }
}