using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 100;
    public float hp;
    public bool isAlive;
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private bool respawnable = false;
    [SerializeField] private float respawnCooldown;
    [SerializeField] private float deathTime;
    [SerializeField] private Vector3 spawnpoint;
    private Animator anim;
    

    private void Start()
    {
        if (spawnOnStart) Spawn();
        anim = GetComponent<Animator>();
    }

    public void ChangeHp(float changeHp)
    {
        hp += changeHp;
        if (changeHp < 0)
        {
            anim.SetTrigger("damage");
            
        }
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        
        if (hp < 0) hp = 0;
        if (hp == 0)
        {
            isAlive = false;
            StartCoroutine(Death());
        }
    }

    public void Spawn()
    {
        hp = maxHp;
        isAlive = true;
        transform.position = spawnpoint;
    }

    public void SpawnWithHp(float startHp)
    {
        if (startHp == 0 || startHp > maxHp) hp = maxHp;
        else hp = startHp;
        isAlive = true;
    }

    public void SpawnWithPosition(Vector3 position)
    {
        hp = maxHp;
        isAlive = true;
        transform.position = position;
    }

    public void SpawnWithHpAndPosition(float startHp, Vector3 position)
    {
        if (startHp == 0 || startHp > maxHp) hp = maxHp;
        else hp = startHp;
        isAlive = true;
        transform.position = position;
    }

    public IEnumerator Death()
    {
        anim.SetBool("death", true);
        yield return new WaitForSeconds(deathTime);
        if (respawnable)
        {
            anim.SetBool("death", true);
            yield return new WaitForSeconds(respawnCooldown);
            SpawnWithPosition(spawnpoint);
            
        }
        else
        {
            Destroy(gameObject);
            
        }
        anim.SetBool("death", false);
    }

    public void ChangeSpawnpoint(Vector2 position)
    {
        spawnpoint = position;
    }
}