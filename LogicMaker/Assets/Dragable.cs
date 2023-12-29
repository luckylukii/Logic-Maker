using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dragable : MonoBehaviour
{
    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}