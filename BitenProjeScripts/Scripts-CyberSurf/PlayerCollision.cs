using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //public Animator anim;
    public GameObject gameOverScreen;
    public Transform path1;// buras� olusturdugumuz haritala�n yolu
    public Transform path2;// ka� tane harita olusturduysak ekleyeblirdsiniz
    public GameManager gameManager;
    private PlayerMovement movement;
    private bool trainRightSwitch = false;
    private bool trainLeftSwitch = false;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Road End 1")// g�r�nmez panelleri Road End 1 veya daha fazlas� seklinde ayarlayabi,irsiniz
        {
            path2.position = new Vector3(path1.position.x, path1.position.y, path1.position.z + 16.0f);//e�er ki ilk yol biterse ikinci yolu �a��r ve + 16 fazlas�n� ekle bu + 16 y� de�istirebilirsinizyolun uzunluguna g�re
        }
        if (other.gameObject.name == "Road End 2") // Bu ve bir �stteki if'e ihtiyac�m�z kalmad�. -Umut
        {
            path1.position = new Vector3(path2.position.x, path2.position.y, path2.position.z + 16.0f);
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            gameManager.ChangeCoin(1);
        }
        if (other.gameObject.CompareTag("Train Right"))
        {
            if (!trainRightSwitch)
            {
                trainRightSwitch = true;
                movement.ChangeLane(1);
            }
        }
        if (other.gameObject.CompareTag("Train Left"))
        {
            if (!trainLeftSwitch)
            {
                trainLeftSwitch = true;
                movement.ChangeLane(-1);
            }
        }
        if (other.gameObject.CompareTag("Kill Zone"))
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Train Right"))
        {
            trainRightSwitch = false;
        }
        if (other.gameObject.CompareTag("Train Left"))
        {
            trainLeftSwitch = false;
        }
    }
}