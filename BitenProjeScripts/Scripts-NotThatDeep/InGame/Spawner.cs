using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    int lastObjectType = 0;
    float lane = 0;
    float timer = 0;

    public ScubaDiver playerScript;
    public GameObject[] objects;
    public float distance = 0;

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public IEnumerator SpawnObject()
    {
        while (!playerScript.isDead)
        {
            float waitingTime = 0.5f;
            if (timer < 40) waitingTime = 1.5f - (timer / 50f);

            int cesit = Random.Range(0, 3);     // 0-trash 1-coral 2-barrier
            int objectType;

            if (cesit == 0)
            {
                lane = Random.Range(-1, 2);
                trash:
                objectType = Random.Range(0, 6);
                if (objectType == lastObjectType) goto trash;
                Instantiate(objects[objectType], new Vector3(10, (lane * distance), 0), Quaternion.identity);
            }
            else if (cesit == 1)
            {
                lane = -1;
                coral:
                objectType = Random.Range(6, 12);
                if (objectType == lastObjectType) goto coral;
                Instantiate(objects[objectType], new Vector3(10, (lane * distance), 0), Quaternion.identity);
            }
            else if (cesit == 2)
            {
                lane = Random.Range(0, 2);
                barrier:
                objectType = Random.Range(12, objects.Length);
                if (objectType == lastObjectType) goto barrier;
                Instantiate(objects[objectType], new Vector3(10, (lane * distance), 0), Quaternion.identity);
            }

            yield return new WaitForSeconds(waitingTime);
        }
    }
}