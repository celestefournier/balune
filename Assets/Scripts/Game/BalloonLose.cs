public class BalloonLose : Balloon
{
    public override void Push(float rotation)
    {
        Pop();
    }
}
