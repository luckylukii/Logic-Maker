using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GatePreview : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text nameText;
    public Image img;
    [System.NonSerialized] public Dragable associatedPrefab;

    public void OnPointerDown(PointerEventData eventData) => Instantiate(associatedPrefab);
}