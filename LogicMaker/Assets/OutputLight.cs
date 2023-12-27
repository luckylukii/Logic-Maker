using UnityEngine;
using UnityEngine.UI;

public class OutputLight : Gate
{
    [SerializeField] private SpriteRenderer image;

    private void Update()
    {
        image.color = pins[0].powerState == PowerState.HIGH ? GameManager.highPowerCol : GameManager.lowPowerCol;
    }
}