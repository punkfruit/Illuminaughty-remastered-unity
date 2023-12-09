using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour
{
    public static MouseCursor instance;
    public bool on = true;
    public SpriteRenderer cursor;
    public Color onn, offf;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (on)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;

            if (Cursor.visible == true)
            {
                Cursor.visible = false;
            }

            cursor.color = onn;
        }
        else
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }

            cursor.color = offf;
        }
        
    }
}
