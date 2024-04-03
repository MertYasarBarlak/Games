using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    public GameManager gm;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash") gm.UpdateHealth(-1);
        Destroy(collision.gameObject);
    }
}
