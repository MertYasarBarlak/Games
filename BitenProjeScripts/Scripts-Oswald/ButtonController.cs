using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllewr : MonoBehaviour
{
    float firstY;
    float targetY;

    public float screenHeightMultiplier;
    public float speed;

    void Start()
    {
        firstY = transform.position.y;
        targetY = Screen.height * screenHeightMultiplier;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, firstY + targetY), speed);
    }
}