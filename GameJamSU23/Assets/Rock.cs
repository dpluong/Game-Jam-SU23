using UnityEngine;

public class Rock : MonoBehaviour
{
    public float requiredHitDuration = 2f;
    private float currentHitDuration = 0f;

    private void Update()
    {
        if (currentHitDuration >= requiredHitDuration)
        {
            Destroy(gameObject); // Destroy the rock object when hit for the required duration
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaserBeam"))
        {
            currentHitDuration += Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaserBeam"))
        {
            currentHitDuration = 0f; // Reset the hit duration if the laser beam exits the collision
        }
    }
}
