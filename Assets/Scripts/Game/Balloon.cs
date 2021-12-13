using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float pushForce;
    [SerializeField] float rotateForce;

    bool canInteract;
    Rigidbody2D rb;
    Collider2D col;
    float cameraWidth;
    float balloonSize = 0.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;
    }

    void Update()
    {
        if (!canInteract)
        {
            CheckForInteract();
        }
        else
        {
            CheckForCollision();
        }
        transform.parent.position += transform.localPosition;
        transform.localPosition = Vector3.zero;
    }

    void CheckForInteract()
    {
        if (transform.position.y < 4)
        {
            canInteract = true;
            col.enabled = true;
        }
    }

    void CheckForCollision()
    {
        if (transform.position.y > 4)
        {
            rb.velocity = -rb.velocity;
            transform.position = new Vector2(transform.position.x, 4);
        }
        if (transform.position.x < -cameraWidth)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            transform.position = new Vector2(-cameraWidth, transform.position.y);
        }
        if (transform.position.x > cameraWidth)
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            transform.position = new Vector2(cameraWidth, transform.position.y);
        }
    }

    public void Push(float rotation)
    {
        rb.AddForce(Vector2.up * pushForce);
        rb.AddTorque(rotation * rotateForce);
        anim.SetTrigger("pushed");
    }

    public void Pop()
    {
        anim.SetBool("popped", true);
    }
}
