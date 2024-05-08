using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [NonSerialized]
    public bool grounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain")) grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain")) grounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!grounded) if (other.CompareTag("Terrain")) grounded = true;
    }
}