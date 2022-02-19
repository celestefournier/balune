using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    [SerializeField] public float spawnRate;
    [SerializeField] protected float pushForce;
    [SerializeField] protected float rotateForce;
    [SerializeField] protected Animator anim;

    protected UnityEvent onDestroy = new UnityEvent();
    protected ScoreManager scoreManager;
    protected Collider2D col;
    protected Rigidbody2D rb;
    GameController gameController;
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
        if (canInteract)
            CheckForCollision();
        else
            CheckForInteract();

        transform.parent.position += transform.localPosition;
        transform.localPosition = Vector3.zero;
    }

    public void Init(GameController gameController, ScoreManager scoreManager, UnityAction onDestroy)
    {
        this.gameController = gameController;
        this.scoreManager = scoreManager;
        this.onDestroy.AddListener(onDestroy);
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

    void CheckForInteract()
    {
        if (transform.position.y < 4)
        {
            canInteract = true;
            col.enabled = true;
        }
    }

    public virtual void Push(float rotation)
    {
        rb.AddForce(Vector2.up * pushForce);
        rb.AddTorque(rotation * rotateForce);
        anim.SetTrigger("pushed");
        scoreManager.AddScore();
    }

    public virtual void Pop(bool gameOver = false)
    {
        col.enabled = false;
        anim.SetBool("popped", true);

        if (gameOver)
            gameController.GameOver();
        else
            scoreManager.AddScore();
    }

    void OnBecameInvisible()
    {
        if (canInteract)
        {
            onDestroy.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }
}
