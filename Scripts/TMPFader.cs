using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class TMPFader makes TextMeshPro object fade in and out according to the text file lines in the TextBoxManager.
/// </summary>

public class TMPFader : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private int fadeInAtLine;
    [SerializeField] private int fadeOutAtLine;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeInAtLine > 0)
        {
            Color objectColor = GetComponent<TMP_Text>().color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            GetComponent<TMP_Text>().color = objectColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOutAtLine == GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().currentLine)
        {
            StartCoroutine(FadeOutObject());
        }

        if(fadeInAtLine == GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().currentLine)
        {
            StartCoroutine(FadeInObject());
        }
    }

    IEnumerator FadeOutObject()
    {
        Color objectColor = GetComponent<TMP_Text>().color;
        while (objectColor.a >= 0)
        {
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<TMP_Text>().color = objectColor;
            yield return null;
        }
    }

    IEnumerator FadeInObject()
    {
        Color objectColor = GetComponent<TMP_Text>().color;
        while (objectColor.a <= 1)
        {
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<TMP_Text>().color = objectColor;
            yield return null;
        }
    }
}
