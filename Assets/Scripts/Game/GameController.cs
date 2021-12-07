using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] ScoreManager scoreManager;

    public static bool gameOver;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform?.tag == "Balloon")
            {
                hit.transform.GetComponent<Balloon>().Push(hit.point.x - hit.transform.position.x);
                scoreManager.AddScore();
            }
        }
    }
}
