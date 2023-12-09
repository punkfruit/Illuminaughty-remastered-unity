using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreenRes : MonoBehaviour
{

    public bool fullscren;
    public int ScrnWidth, ScrnHeight;

    public Sprite check, uncheck;
    public Image checkBox;
    // Start is called before the first frame update
    void Start()
    {
        fullscren = Screen.fullScreen;
        ScrnWidth = Screen.width;
        ScrnHeight = Screen.height;

        if (fullscren)
            checkBox.sprite = check;
        else
            checkBox.sprite = uncheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickResWidth(int width)
    {
        ScrnWidth = width;
        //ScrnHeight = height;
    }

    public void PickResHeight(int height)
    {
        ScrnHeight = height;
    }

    public void Apply()
    {
        Screen.SetResolution(ScrnWidth, ScrnHeight, fullscren);
        Screen.fullScreen = fullscren;
    }

    public void FullCheck()
    {
        if (fullscren)
        {
            fullscren = false;
            checkBox.sprite = uncheck;
        }
        else
        {
            fullscren = true;
            checkBox.sprite = check;
        }
    }
}
