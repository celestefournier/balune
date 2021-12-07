using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] GameController gameController;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (ModeManager.mode == "normal" && col.transform.tag == "Balloon")
        {
            // gameController.GameOver();
        }
    }
}
