using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        if (health == null) Debug.LogWarning("Object has not Health script!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            health.Spawn();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            health.Spawn();
        }
    }
}