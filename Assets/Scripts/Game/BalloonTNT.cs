using UnityEngine;

public class BalloonTNT : Balloon
{
    [SerializeField] GameObject explosionPrefab;

    bool clicked;
    float explosionRange = 3.5f;

    public override void Push(float rotation)
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
        scoreManager.AddScore();

        var objects = Physics2D.CircleCastAll(transform.position, explosionRange, Vector2.zero);

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
