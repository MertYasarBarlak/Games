using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private GroundCheck2D check;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float moveSpeed = 6f;
    private SpriteRenderer sprite;
    private Animator anim;
    private float dirX = 0f;
    private Health heal;
    

    private enum MovementState { idle,running,jumping,falling, }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        heal = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heal.isAlive) { 
        dirX = Input.GetAxisRaw("Horizontal");
        rb. velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown("space"))
        {
            if (check.grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            
        }

        UpdateAnimationState();
        }

    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping; 
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }


}
