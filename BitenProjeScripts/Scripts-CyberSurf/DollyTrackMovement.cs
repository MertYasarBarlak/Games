using UnityEngine;

public class DollyTrackMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private bool followZ = true;
    [SerializeField] private float followSpeed = 0.1f;

    private void LateUpdate()
    {
        Vector3 goTo = transform.position;
        if (followX) goTo.x = target.position.x;
        if (followY) goTo.y = target.position.y;
        if (followZ) goTo.z = target.position.z;

        transform.position = Vector3.Lerp(transform.position, goTo, followSpeed);   
    }
}