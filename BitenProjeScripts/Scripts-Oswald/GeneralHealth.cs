using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool alive;
    public bool flashable;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        health = maxHealth;
        alive = true;
    }

    public void GetDamage(float damage)
    {
        if (flashable) GetComponent<CharacterFlasher>().Flash(0.15f);
            health -= damage;
        if (health <= 0) alive = false;
    }
}