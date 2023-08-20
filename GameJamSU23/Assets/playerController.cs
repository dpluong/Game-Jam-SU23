using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5f;
    public float scaleDuration = 2f;
    public float dashDistance = 2f;
    public LineRenderer laserBeam;
    private Vector3 initialScale;
    private bool isScaling = false;
    private bool isFiring = false;

    public bool isInRestrictedArea = true; // Indicates whether the player is in the restricted area

    private MouseAim mouseAim; // Reference to the MouseAim script

    private void Start()
    {
        initialScale = transform.localScale;
        laserBeam.enabled = false; // Disable the laser beam at the start

        // Get the MouseAim script component
        mouseAim = GetComponentInChildren<MouseAim>();
    }

    private void Update()
    {
        // Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(horizontalMovement * speed * Time.deltaTime, verticalMovement * speed * Time.deltaTime, 0f);

        // Scaling
        if (Input.GetKeyDown(KeyCode.X) && !isScaling)
        {
            StartCoroutine(ScaleOverTime(initialScale * 2f, scaleDuration));
        }
        else if (Input.GetKeyDown(KeyCode.C) && !isScaling)
        {
            StartCoroutine(ScaleOverTime(initialScale / 2f, scaleDuration));
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(Dash());
        }

        // Firing laser beam
        if (Input.GetMouseButtonDown(0) && !isInRestrictedArea) // Added check for isInRestrictedArea
        {
            isFiring = true;
            laserBeam.enabled = true;
        }
        else if (Input.GetMouseButtonUp(0) || isInRestrictedArea)
        {
            isFiring = false;
            laserBeam.enabled = false;
        }

        if (isFiring)
        {
            // Set the start position of the laser beam
            laserBeam.SetPosition(0, transform.position);

            // Get the end position from the MouseAim object
            Vector3 aimPosition = mouseAim.transform.position;
            laserBeam.SetPosition(1, aimPosition);
        }
    }

    private IEnumerator ScaleOverTime(Vector3 targetScale, float duration)
    {
        isScaling = true;

        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        yield return new WaitForSeconds(2f);

        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
        isScaling = false;
    }

    private IEnumerator Dash()
    {
        Vector3 startPosition = transform.position;
        Vector3 dashDirection = GetDashDirection();

        float dashDistance = 2f;
        float dashDuration = dashDistance / 5f; // Velocity = Distance / Time

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float distance = Mathf.Lerp(0f, dashDistance, elapsedTime / dashDuration);
            Vector3 newPosition = startPosition + dashDirection * distance;
            newPosition.y = startPosition.y; // Maintain the same y position
            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition + dashDirection * dashDistance;
    }

    private Vector3 GetDashDirection()
    {
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput -= 1f; // Left arrow held, move left (-2 on x-axis)
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput += 1f; // Right arrow held, move right (+2 on x-axis)
        }

        Vector3 dashDirection = new Vector3(horizontalInput, 0f, 0f).normalized;

        // If no input is given, dash in the direction the player is facing
        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.right;
        }

        return dashDirection;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("RestrictedArea"))
        {
            isInRestrictedArea = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("RestrictedArea"))
        {
            isInRestrictedArea = true;
        }
    }
}
