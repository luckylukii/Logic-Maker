using UnityEngine;

public class GameManager : MonoBehaviour
{
    public readonly static Color highPowerCol = Color.red;
    public readonly static Color lowPowerCol = new(1, 0.3f, 0, 1);

    private void Update()
    {
        foreach (var wire in GameObject.FindGameObjectsWithTag("Wire"))
        {
            if (wire.GetComponent<Line>().powerState == PowerState.HIGH)
            {
                wire.GetComponent<LineRenderer>().material.color = highPowerCol;
            }
            else if (wire.GetComponent<Line>().powerState == PowerState.LOW)
            {
                wire.GetComponent<LineRenderer>().material.color = lowPowerCol;
            }
        }
    }
}