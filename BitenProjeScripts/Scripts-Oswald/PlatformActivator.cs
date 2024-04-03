using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    private void Start()
    {
        GetComponent<WayPointFollowe>().isActive = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GetComponent<WayPointFollowe>().isActive = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GetComponent<WayPointFollowe>().isActive = false;
        }
    }
}
