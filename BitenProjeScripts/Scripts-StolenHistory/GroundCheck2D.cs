using UnityEngine;

public class GroundCheck2D : MonoBehaviour
{
    public bool grounded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) grounded = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!grounded) if (other.CompareTag("Ground")) grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) grounded = false;
    }
}