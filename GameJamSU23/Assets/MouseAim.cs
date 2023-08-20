using UnityEngine;

public class MouseAim : MonoBehaviour
{
    private void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - Camera.main.transform.position.z));

        // Update the aim object's position to follow the mouse
        transform.position = worldPosition;
    }
}
