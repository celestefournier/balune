using UnityEngine;

public class Balloon : MonoBehaviour
{
    bool canInteract;

    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
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
    }

    void CheckForInteract()
    {
        if (transform.position.y < 4)
        {
            canInteract = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    void CheckForCollision()
    {
        if (transform.position.y > 4)
        {
            GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity;
            transform.position = new Vector2(transform.position.x, 4);
        }
        if (transform.position.x < -8)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x,
                GetComponent<Rigidbody2D>().velocity.y
            );
            transform.position = new Vector2(-8, transform.position.y);
        }
        if (transform.position.x > 8)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x,
                GetComponent<Rigidbody2D>().velocity.y
            );
            transform.position = new Vector2(8, transform.position.y);
        }
    }
}
