using System.Collections;
using UnityEngine;

public class CharacterFlasher : MonoBehaviour
{
    public Material flashMaterial;
    SpriteRenderer spriteRenderer;
    Material defaultMaterial;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void Flash(float duration)
    {
        StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float duration)
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = defaultMaterial;
    }
}