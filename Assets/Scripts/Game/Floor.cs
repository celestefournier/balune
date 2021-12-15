using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] GameController gameController;

    void Start()
    {
        if (GameController.gameMode == "normal")
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Balloon")
        {
            col.GetComponent<Balloon>().Pop();
        }
    }
}
