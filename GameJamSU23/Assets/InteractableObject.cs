using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject objectToInteract;  // Reference to the object to interact with
    public GameObject bubble;  // Reference to the bubble object
    public float destroyDelay = 2f;  // Delay before destroying the rocks
    private bool isInteracting = false;  // Flag to track if the object is currently interacting

    private void Update()
    {
        playerController bubbleController = bubble.GetComponent<playerController>();

        if (Input.GetMouseButtonDown(0) && !bubbleController.isInRestrictedArea)  // Left mouse button is pressed and bubble is not in the restricted area
        {
            isInteracting = true;
            objectToInteract.SetActive(true);
        }

        if (isInteracting)
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position from screen coordinates to world coordinates
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, objectToInteract.transform.position.z - Camera.main.transform.position.z));

            // Update the object's position to follow the mouse
            objectToInteract.transform.position = worldPosition;
        }

        if (isInteracting)
        {
            // Check if the object hit any rocks
            Collider2D[] hitRocks = Physics2D.OverlapCircleAll(objectToInteract.transform.position, 0.2f);
            foreach (Collider2D hitRock in hitRocks)
            {
                if (hitRock.CompareTag("Rock"))
                {
                    // Destroy the rock after a delay
                    Destroy(hitRock.gameObject, destroyDelay);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) || bubbleController.isInRestrictedArea)  // Left mouse button is released or bubble is in restricted area
        {
            isInteracting = false;
            objectToInteract.SetActive(false);
        }
    }
}
