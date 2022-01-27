using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class TextBoxActivation is used on a game object to activate textbox event on collision with player.
/// </summary>

public class TextBoxActivation : MonoBehaviour
{
    private TextBoxManager manager;
    [SerializeField] float speed;
    public bool destroyWhenActivated;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TextBoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3((-1) * speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Activator colliosion: " + collision.name);
        if(collision.tag.Equals("Player"))
        {
            if(manager.textFileIndex != 0)
            {
                manager.ReloadScript();
            }
            manager.currentLine = 0;
            manager.endAtLine = manager.textLines.Count - 1;
            manager.EnableTextBox();

            if (destroyWhenActivated)
            {
                Destroy(this);
            }
        }
    }


}
