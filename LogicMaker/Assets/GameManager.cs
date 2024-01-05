using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Gradient highPowerGrad;
    public Gradient lowPowerGrad;

    private void Update()
    {
        foreach (var wire in GameObject.FindGameObjectsWithTag("Wire"))
        {
            if (wire.GetComponent<Line>().powerState == PowerState.HIGH)
            {
                wire.GetComponent<LineRenderer>().colorGradient = highPowerGrad;
            }
            else if (wire.GetComponent<Line>().powerState == PowerState.LOW)
            {
                wire.GetComponent<LineRenderer>().colorGradient = lowPowerGrad;
            }
        }
    }
}