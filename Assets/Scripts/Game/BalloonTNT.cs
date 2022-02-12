using UnityEngine;

public class BalloonTNT : Balloon
{
    [SerializeField] GameObject explosionPrefab;

    public override void Pop()
    {
        RaycastHit2D[] objects = Physics2D.CircleCastAll(transform.position, 4, Vector2.zero);

        foreach (var obj in objects)
        {
            if (obj.transform != transform && obj.transform.GetComponent<Balloon>())
            {
                obj.transform.GetComponent<Balloon>().Pop();
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        col.enabled = false;
        anim.SetBool("popped", true);
    }
}
