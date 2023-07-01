using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 5f;
    public float scaleDuration = 2f;
    private Vector3 initialScale;
    private bool isScaling = false;

    private void Start()
    {
        initialScale = transform.localScale;
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
}
