public class BalloonLose : Balloon
{
    public override void Pop(bool gameOver = true)
    {
        base.Pop(true);
    }

    public override void Push(float rotation)
    {
        Pop(true);
    }
}
