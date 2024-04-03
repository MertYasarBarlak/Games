using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Health>())
            collision.GetComponent<Health>().ChangeSpawnpoint(transform.position);
        }
    }
}