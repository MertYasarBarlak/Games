using UnityEngine;

public class SideCheck2D : MonoBehaviour
{
    public bool sideCheck;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) sideCheck = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!sideCheck) if (other.CompareTag("Ground")) sideCheck = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) sideCheck = false;
    }
}