using UnityEngine;

public class BalloonTNT : Balloon
{
    [SerializeField] GameObject explosionPrefab;

    bool clicked;
    float explosionRange = 3.5f;

    public override void Push(float rotation)
    {
        rb.AddForce(Vector2.up * pushForce);
        rb.AddTorque(rotation * rotateForce);
        scoreManager.AddScore();

        if (!clicked)
        {
            anim.SetTrigger("pushed");
            return;
        }

        clicked = true;
    }

    public override void Pop(bool gameOver = false)
    {
        col.enabled = false;
        anim.SetBool("popped", true);

        RaycastHit2D[] objects = Physics2D.CircleCastAll(transform.position, explosionRange,
            Vector2.zero);

        foreach (var obj in objects)
        {
            if (obj.transform != transform && obj.transform.GetComponent<Balloon>())
            {
                obj.transform.GetComponent<Balloon>().Pop();
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShake>().Shake();
    }
}
