using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Class Credits manages the credits roll of the Credits Scene. It reads the contents from a txt file.
/// </summary>

public class Credits : MonoBehaviour
{
    public TMP_Text headlineText;
    public TMP_Text infoText;
    public TextAsset headlineTextFile;
    public TextAsset infoTextFile;
    public List<string> headlineTextLines = new List<string>();
    public List<string> infoTextLines = new List<string>();
    [SerializeField] float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        headlineText.text = "";
        infoText.text = "";
        
        //Read the text file for headlines.
        if (headlineTextFile != null)
        {
            headlineTextLines = headlineTextFile.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None).ToList();
            foreach(string line in headlineTextLines)
            {
                headlineText.text += line + "\n";
            }
        }

        //Read the text file for info content.
        if (infoTextFile != null)
        {
            infoTextLines = infoTextFile.text.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None).ToList();
            foreach (string line in infoTextLines)
            {
                infoText.text += line + "\n";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, movementSpeed * Time.deltaTime, 0);
    }
}
