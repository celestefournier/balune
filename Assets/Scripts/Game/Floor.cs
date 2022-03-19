using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] GameController gameController;

    void Start()
    {
        if (GameController.gameMode == "normal")
            GetComponent<Collider2D>().isTrigger = true;
        else
            GetComponent<Collider2D>().isTrigger = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Balloon" && GameController.gameMode == "normal")
        {
            col.GetComponent<Balloon>().Pop(true);
        }
    }
}
