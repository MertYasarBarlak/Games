using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cloud")
        {
            Destroy(collision.gameObject);
            
        }
        if (collision.tag == "Player")
        {
            collision.GetComponent<GeneralHealth>().GetDamage(100);
        }
        
    }
}
