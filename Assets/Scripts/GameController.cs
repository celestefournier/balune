using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float forca;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.transform?.tag == "Ballon")
            {
                hit.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forca);
            }
        }
    }
}
