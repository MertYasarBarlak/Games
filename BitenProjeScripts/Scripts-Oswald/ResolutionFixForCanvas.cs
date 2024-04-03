using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionFixForCanvas : MonoBehaviour
{
    float xPos;
    float yPos;
    float xScale;
    float yScale;
    [SerializeField] float workingResolutionY;
    [SerializeField] bool positioner = true;
    [SerializeField] bool scaler = true;

    void Start()
    {
        if (positioner)
        {
            xPos = GetComponent<RectTransform>().anchoredPosition.x;
            yPos = GetComponent<RectTransform>().anchoredPosition.y;
            GetComponent<RectTransform>().anchoredPosition = new Vector3(xPos * (Screen.height / workingResolutionY), yPos * (Screen.height / workingResolutionY), 1f);
        }
        if (scaler)
        {
            xScale = GetComponent<RectTransform>().sizeDelta.x;
            yScale = GetComponent<RectTransform>().sizeDelta.y;
            GetComponent<RectTransform>().sizeDelta = new Vector3(xScale * (Screen.height / workingResolutionY), yScale * (Screen.height / workingResolutionY), 1f);
        }
    }
}