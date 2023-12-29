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

    [Header("DEBUG")]
    [SerializeField] private CustomGate prefab;
    [SerializeField] private int inputs = 3;
    [SerializeField] private int outputs = 2;
    [SerializeField] private string Name = "Test";
    [System.Serializable] public struct serializedArray { public PowerState[] arr; }
    [SerializeField] private serializedArray[] inputsArr;
    [SerializeField] private serializedArray[] outputsArr;


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

        if (Input.GetKeyDown(KeyCode.C))
            SpawnCustomGate();
    }

    [ContextMenu("Spawn Custom")]
    public void SpawnCustomGate()
    {
        //This is an or gate
        System.Collections.Generic.Dictionary<PowerState[], PowerState[]> thruthTable = new();
        for (int i = 0; i < inputsArr.Length; i++)
        {
            thruthTable.Add(inputsArr[i].arr, outputsArr[i].arr);
        }
        Instantiate(prefab).Init(inputs, outputs, Name, thruthTable);
    }
}