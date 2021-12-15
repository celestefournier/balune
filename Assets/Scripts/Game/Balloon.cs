using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float pushForce;
    [SerializeField] float rotateForce;
    [SerializeField] GameController gameController;
    [SerializeField] ScoreManager scoreManager;

    Rigidbody2D rb;
    Collider2D col;
    bool canInteract;
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

    public void Init(GameController gameController, ScoreManager scoreManager)
    {
        this.gameController = gameController;
        this.scoreManager = scoreManager;
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

    public virtual void Push(float rotation)
    {
        rb.AddForce(Vector2.up * pushForce);
        rb.AddTorque(rotation * rotateForce);
        anim.SetTrigger("pushed");
        scoreManager.AddScore();
    }

    public void Pop()
    {
        col.enabled = false;
        anim.SetBool("popped", true);
        gameController.GameOver();
    }

    void OnBecameInvisible()
    {
        if (canInteract) Destroy(transform.parent.gameObject);
    }
}
