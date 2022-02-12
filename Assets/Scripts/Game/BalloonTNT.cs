using UnityEngine;

public class BalloonTNT : Balloon
{
    public override void Push(float rotation)
    {
        Pop();
    }

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

        col.enabled = false;
        anim.SetBool("popped", true);
    }
}
