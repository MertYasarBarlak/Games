using UnityEngine;

public class MapSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject map1;
    [SerializeField] private GameObject map2;
    [SerializeField] private float mapSwitchCooldown = 0.2f;
    private float mapSwitchTimer;

    private void Update()
    {
        if (mapSwitchTimer > 0) mapSwitchTimer -= Time.deltaTime;
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (map1.activeSelf && !map2.activeSelf)
                {
                    mapSwitchTimer = mapSwitchCooldown;
                    map2.SetActive(true);
                    map1.SetActive(false);
                }
                else
                {
                    mapSwitchTimer = mapSwitchCooldown;
                    map1.SetActive(true);
                    map2.SetActive(false);
                }
            }
        }
    }
}