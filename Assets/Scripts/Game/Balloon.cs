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
    protected CircleCollider2D col;
    protected Rigidbody2D rb;
    GameController gameController;
    bool canInteract;
    float cameraWidth;
    float balloonSize = 0.8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect - balloonSize;
    }

    void Update()
    {
        if (canInteract)
            CheckForCollision();
        else
            CheckForInteract();

        transform.parent.position = transform.position;
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
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
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
        var maxAngle = 45;
        var normalizeRotation = 90;
        var angle = ((rotation / col.radius) * maxAngle) + normalizeRotation;
        var anglePosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        var angleDifference = angle - transform.parent.rotation.eulerAngles.z;

        rb.AddForce(anglePosition * pushForce);
        rb.AddTorque(rotation * rotateForce);
        transform.parent.rotation = Quaternion.Euler(0, 0, angle);
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z - angleDifference);
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
