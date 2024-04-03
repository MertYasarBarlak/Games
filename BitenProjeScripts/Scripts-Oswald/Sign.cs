using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    GameObject textBox;
    public string saying;
    bool firstSign = false;

    private void Start()
    {
        firstSign = false;
        textBox = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !firstSign)
        {
            firstSign = true;
            textBox.GetComponent<Text>().text = saying;
            textBox.transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && textBox.transform.parent.gameObject.activeSelf) textBox.transform.parent.gameObject.SetActive(false);
    }
}
