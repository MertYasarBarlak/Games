using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] float lifeTime;
    public float bulletSpeed;
    float bulletDamage;
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Terrain") rb2D.gravityScale = 1f;
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<GeneralHealth>().GetDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetBulletDamage(float damage)
    {
        bulletDamage = damage;
    }
}