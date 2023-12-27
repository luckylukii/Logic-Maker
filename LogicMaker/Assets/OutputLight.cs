using UnityEngine;
using UnityEngine.UI;

public class OutputLight : Gate
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        //For simplicity, we just take the first color of the gradient, since all of them should be the same
        image.color = pins[0].powerState == PowerState.HIGH ? gameManager.highPowerGrad.colorKeys[0].color : gameManager.lowPowerGrad.colorKeys[0].color;
    }
}