using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float chaseSpeed;
    [SerializeField] float chaseDistanceX;
    [SerializeField] float chaseDistanceY;

    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x + (target.GetComponent<Rigidbody2D>().velocity.x * chaseDistanceX), target.transform.position.y + (target.GetComponent<Rigidbody2D>().velocity.y * chaseDistanceY), target.transform.position.z - 10), chaseSpeed);
    }
}