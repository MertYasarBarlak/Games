using UnityEngine;

public class Gate : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player") anim.SetBool("AmIOpened", true);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player") anim.SetBool("AmIOpened", false);
    }
}