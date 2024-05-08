using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Lane Movement")]
    [SerializeField] private int laneCount = 3;
    [SerializeField] private float laneSize = 5f;
    [SerializeField] private int startLane = 1;
    [SerializeField] private float slideSpeed = 0.1f;
    [NonSerialized] public int lane;
    private bool laneSwitch = false;
    private Vector3 startPos;

    [Header("Jump")]
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private int jumpCount = 1;
    private Rigidbody rb;
    private bool jumpSwitch = false;
    private int remainingJumps = 0;

    [Header("Lean")]
    [SerializeField] private float leanDuration = 1f;
    [SerializeField] private float jumpCancelSpeed = 5f;
    private CapsuleCollider col;
    private bool leanSwitch;

    private void Start()
    {
        lane = startLane;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        startPos = rb.position;
        rb.position = startPos + new Vector3(lane * laneSize, 0, 0);
    }

    private void Update()
    {
        LaneMovement();
        Jump();
        Lean();
    }

    private void LaneMovement()
    {
        if (laneSwitch)
        {
            if ((int)Input.GetAxisRaw("Horizontal") == 0) laneSwitch = false;
        }
        if (!laneSwitch)
        {
            if ((int)Input.GetAxisRaw("Horizontal") != 0)
            {
                if ((lane > 0 && Input.GetAxisRaw("Horizontal") < -0.5f) || (lane < laneCount - 1 && Input.GetAxisRaw("Horizontal") > 0.5f))
                {
                    laneSwitch = true;
                    lane += (int)Input.GetAxisRaw("Horizontal");
                }
            }
        }

        startPos.y = rb.position.y;
        //transform.position = Vector3.Lerp(transform.position, startPos + new Vector3(lane * laneSize, 0, 0), slideSpeed);
        rb.MovePosition(Vector3.Lerp(rb.position, startPos + new Vector3(lane * laneSize, 0, 0), slideSpeed));
    }

    public void ChangeLane(int changeBy)
    {
        if (Time.timeScale != 0)
        {
            if (lane + changeBy >= 0 && lane + changeBy <= laneCount - 1)
            {
                lane += changeBy;
            }
            else
            {
                if (lane + changeBy < 0) lane = 0;
                if (lane + changeBy > laneCount - 1) lane = laneCount - 1;
            }
        }
    }

    private void Jump()
    {
        if (groundCheck.grounded)
        {
            if (remainingJumps != jumpCount) remainingJumps = jumpCount;
        }
        else if (remainingJumps == jumpCount) remainingJumps--;

        if (jumpSwitch)
        {
            if ((int)Input.GetAxisRaw("Vertical") < 0.5f) jumpSwitch = false;
        }
        if (!jumpSwitch)
        {
            if ((int)Input.GetAxisRaw("Vertical") > 0.5f)
            {
                if (remainingJumps > 0)
                {
                    jumpSwitch = true;
                    remainingJumps--;
                    rb.velocity += new Vector3(0, jumpSpeed - rb.velocity.y, 0);
                }
            }
        }
    }

    private void Lean()
    {
        if (!leanSwitch)
        {
            if ((int)Input.GetAxisRaw("Vertical") < -0.5f)
            {
                StartCoroutine(LeanWait(leanDuration));
            }
        }
    }

    private IEnumerator LeanWait(float waitTime)
    {
        leanSwitch = true;
        if (groundCheck.grounded)
        {
            col.height = col.height * 0.5f;
            transform.position -= new Vector3(0, 0.2f, 0);
            yield return new WaitForSeconds(waitTime);
            transform.position += new Vector3(0, 0.6f, 0);
            col.height = col.height * 2;
        }
        else
        {
            col.height = col.height * 0.5f;
            rb.velocity -= new Vector3(0, jumpCancelSpeed - rb.velocity.y, 0);
            yield return new WaitForSeconds(waitTime);
            transform.position += new Vector3(0, 0.6f, 0);
            col.height = col.height * 2;
        }
        leanSwitch = false;
    }
}
