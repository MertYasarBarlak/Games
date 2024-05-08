using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapArray;

    public void SummonWithPosition(Vector3 pos)
    {
        int random = Random.Range(0, mapArray.Length);
        Instantiate(mapArray[random], pos, mapArray[random].transform.rotation);
    }
}