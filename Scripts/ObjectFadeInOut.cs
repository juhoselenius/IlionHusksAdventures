using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class ObjectFadeInOut makes an object fade in, wait for a specified amount of time, then fade out.
/// </summary>

public class ObjectFadeInOut : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private int timeInView;
    private int round; // Counts the rounds of the fades. Fading in and fading out counts as one round.
    private string objectTag;

    private void Awake()
    {
        objectTag = this.tag;

        if(objectTag.Equals("FadeableObject"))
        {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            this.GetComponent<Renderer>().material.color = objectColor;
            round = 0;
        }

        if(objectTag.Equals("Text"))
        {
            Color objectColor = GetComponent<TMP_Text>().color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            GetComponent<TMP_Text>().color = objectColor;
            round = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objectTag.Equals("FadeableObject"))
        {
            if (round == 0)
            {
                StartCoroutine(FadeInOutObject(timeInView));
            }
        }

        if(objectTag.Equals("Text"))
        {
            if (round == 0)
            {
                StartCoroutine(FadeInOutText(timeInView));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeInOutObject(int time)
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        while (objectColor.a < 1 && round == 0)
        {
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }
        
        yield return new WaitForSeconds(time);

        objectColor = this.GetComponent<Renderer>().material.color;
        while (objectColor.a > 0 && round == 0)
        {
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;
            yield return null;
        }

        round++;
    }

    IEnumerator FadeInOutText(int time)
    {
        Color objectColor = GetComponent<TMP_Text>().color;
        while (objectColor.a < 1 && round == 0)
        {
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<TMP_Text>().color = objectColor;
            yield return null;
        }

        yield return new WaitForSeconds(time);

        objectColor = GetComponent<TMP_Text>().color;
        while (objectColor.a > 0 && round == 0)
        {
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            GetComponent<TMP_Text>().color = objectColor;
            yield return null;
        }

        round++;
    }
}
