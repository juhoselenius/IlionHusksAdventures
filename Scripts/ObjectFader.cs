using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class ObjectFader makes object fade in and out according to the text file lines in the TextBoxManager.
/// </summary>

public class ObjectFader : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private int fadeInAtLine;
    [SerializeField] private int fadeOutAtLine;

    // Start is called before the first frame update
    void Start()
    {
        if(fadeInAtLine >= 0)
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            this.GetComponent<Renderer>().material.color = objectColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutAtLine == GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().currentLine)
        {
            StartCoroutine(FadeOutObject());
        }

        if (fadeInAtLine == GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().currentLine)
        {
            StartCoroutine(FadeInObject());
        }
    }

    IEnumerator FadeOutObject()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        while (objectColor.a > 0)
        {
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            Debug.Log("Alpha :" + objectColor.a);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
    }

    IEnumerator FadeInObject()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        Debug.Log("Alpha:" + objectColor.a);
        while (objectColor.a < 1)
        {
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            Debug.Log("Alpha new :" + objectColor.a);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
    }
}
