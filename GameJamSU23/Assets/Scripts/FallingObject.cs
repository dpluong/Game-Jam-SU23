using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Vector2 movementDirection;
    public Rigidbody2D rigbody2D;
    [SerializeField]
    private float forceAmount;
    private float timeElapsed;
    private bool isMoving = false;
    void Start()
    {
        rigbody2D.velocity = Vector3.down * forceAmount;
    }

    void Update() { 
        
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        timeElapsed += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            isMoving = false;
        }
        if (isMoving) {
            rigbody2D.velocity = movementDirection * forceAmount;
        }
    }
}
