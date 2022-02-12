using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] BalloonTNT balloon;

    public void Pop()
    {
        balloon.Pop();
    }
}
