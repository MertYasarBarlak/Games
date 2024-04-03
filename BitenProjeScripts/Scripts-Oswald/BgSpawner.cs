using System.Collections;
using UnityEngine;

public class BgSpawner : MonoBehaviour
{
    public GameObject[] bgObjects;
    public float distance = 0;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    public IEnumerator SpawnObject()
    {
        while (true)
        {
            Instantiate(bgObjects[(int)Random.Range(0, bgObjects.Length)], new Vector3(transform.position.x,transform.position.y+ Random.Range(-1f * distance, distance), 0), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}