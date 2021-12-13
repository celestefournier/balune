using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] GameController gameController;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameController.gameMode == "normal" && col.transform.tag == "Balloon")
        {
            gameController.GameOver();
            col.gameObject.GetComponent<Balloon>().Pop();
        }
    }
}
