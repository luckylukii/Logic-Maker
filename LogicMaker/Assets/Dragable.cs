using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dragable : MonoBehaviour
{
    [SerializeField] private bool moveX = true;
    [SerializeField] private bool moveY = true;
    private void OnMouseDrag()
    {
        var mPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += new Vector3(moveX ? mPos.x : 0, moveY ? mPos.y : 0);
    }
}