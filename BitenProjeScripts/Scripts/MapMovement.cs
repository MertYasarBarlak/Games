using UnityEngine;

public class MapMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float mapLenght = 510f;
    [SerializeField] private float mapTeleportZPosition = -340f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, -gameManager.speed);

        if (transform.position.z <= mapTeleportZPosition) transform.position += new Vector3(0, 0, mapLenght * 2);
    }
}