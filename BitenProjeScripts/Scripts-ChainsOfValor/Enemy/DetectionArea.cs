using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public bool playerDetected;
    [SerializeField] private EnemyMoveFollewer follower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            follower.target = collision;
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false;
            follower.target = null;
        }
    }
}