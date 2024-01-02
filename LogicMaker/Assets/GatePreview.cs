using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatePreview : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text nameText;
    public Image img;
    [System.NonSerialized] public Gate associatedPrefab;
    [System.NonSerialized] public Transform parent;

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(associatedPrefab, parent);
    }
}