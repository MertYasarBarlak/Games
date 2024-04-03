using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.position += Vector3.left * speed;
    }

    
}