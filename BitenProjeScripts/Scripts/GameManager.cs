using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Speed")]
    [SerializeField] private float startSpeed = 0.2f;
    [SerializeField] private float endSpeed = 2f;
    [SerializeField] private float acceleration = 0.1f;
    //[NonSerialized]
    public float speed;

    [Header("Player")]
    [NonSerialized] public int coin;
    [NonSerialized] public int score = 1;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        speed = startSpeed;
    }

    private void FixedUpdate()
    {
        if (speed != endSpeed)
        {
            if (speed < endSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else speed = endSpeed;
        }
    }

    public void ChangeCoin(int changeBy)
    {
        coin += changeBy;
        coinText.SetText(coin.ToString());
    }
}