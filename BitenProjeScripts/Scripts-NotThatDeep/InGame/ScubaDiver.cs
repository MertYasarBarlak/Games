using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScubaDiver : MonoBehaviour
{
    Animator animator;
    float lastY;
    float specialY;
    float startY;

    public Animator sand1Animator;
    public Animator sand2Animator;
    public IGMenuManager menuManager;
    public GameManager gm;
    public Transform diver;
    public bool isDead;
    public float distance;
    public float speed;

    void Start()
    {
        Time.timeScale = 1f;
        animator = GetComponent<Animator>();
        startY = diver.position.y;
        specialY = startY;
    }

    void Update()
    {
        if (gm.health <= 0)
        {
            sand1Animator.enabled = false;
            sand2Animator.enabled = false;
            animator.enabled = false;
            isDead = true;
            menuManager.GameOver();
        } else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerUp();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerDown();
            }
        }
    }

    private void FixedUpdate()
    {
        diver.position = Vector2.Lerp(diver.position, new Vector2(diver.position.x, specialY), speed);
        animator.SetFloat("deltaY", (transform.position.y - lastY) * 50);
        lastY = transform.position.y;
    }

    void playerUp()
    {
        if (specialY < distance + startY * -1)
        {
            specialY += distance;
        }
    }

    void playerDown()
    {
        if (specialY > (distance + startY * -1) * -1)
        {
            specialY -= distance;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            gm.UpdateScore(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Coral")
        {
            gm.UpdateHealth(-1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "BarrierFish")
        {
            gm.UpdateHealth(-1);
            Destroy(collision.gameObject);
        }
    }
}