using UnityEngine;

public class Bottle : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown() {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void OnMouseDrag() {
        Vector2 currentScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentPos;
    }
}
