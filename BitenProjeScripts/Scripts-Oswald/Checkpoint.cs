using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().GetCheckpoint(transform.position.x, transform.position.y);
        }
    }
}
