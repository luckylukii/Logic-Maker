using UnityEngine;

public class Gate : MonoBehaviour
{
    public Pin[] pins;
    public string chipName;

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl)) Destroy(gameObject);
    }
}