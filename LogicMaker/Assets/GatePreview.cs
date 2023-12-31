using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatePreview : MonoBehaviour, IPointerClickHandler
{
    /*private const float START_POSITION_X = -5f;
    private const float START_POSITION_Y = 3.5f;
    private const float OFFSET_Y = -1f;
    private const float OFFSET_X = 2.5f;
    private const int START_X_OFFSET_AT = 6;
    private const int MAX_BLOCKS = 30;

    public static int numInstantiated;*/

    public TMP_Text nameText;
    public Image img;
    [System.NonSerialized] public Gate associatedPrefab;

    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (++numInstantiated >= MAX_BLOCKS)
        {
            Debug.LogWarning("Max Block limit reached");
            return;
        }*/

        /*Gate inst = */Instantiate(associatedPrefab);
        //inst.transform.position = GetInstancePosition();
    }
    /*private Vector2 GetInstancePosition()
    {
        Vector2 result = Vector2.zero;
        result.y = numInstantiated % START_X_OFFSET_AT * OFFSET_Y + START_POSITION_Y;
        result.x = Mathf.FloorToInt(numInstantiated / START_X_OFFSET_AT) * OFFSET_X + START_POSITION_X; //Mathf.Floor is not needed here (integer division)

        return result;
    }*/
}