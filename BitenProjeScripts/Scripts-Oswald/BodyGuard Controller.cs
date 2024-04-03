using UnityEngine;
using UnityEngine.UI;

public class BodyGuardController : MonoBehaviour
{
    GameObject textBox;

    public string cantGo;
    public string canGo;

    private void Start()
    {
        textBox = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerProofManager>().proofCount >= collision.gameObject.GetComponent<PlayerProofManager>().enoughProofCount)
            {
                textBox.GetComponent<Text>().text = canGo;
                textBox.transform.parent.gameObject.SetActive(true);
                GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                textBox.GetComponent<Text>().text = cantGo;
                textBox.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        textBox.transform.parent.gameObject.SetActive(false);
    }
}
