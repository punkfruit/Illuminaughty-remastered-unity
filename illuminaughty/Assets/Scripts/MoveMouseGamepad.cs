using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MoveMouseGamepad : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    public string[] names;

    public Vector2 mouseGPpos;
    public SimulateClick clik;

    private void Start()
    {
        names = Input.GetJoystickNames();
        if (names[0] != "")
        {
            mouseGPpos.x = Screen.width / 2;
            mouseGPpos.y = Screen.height / 2;
        }
    }
    private void Update()
    {
        names = Input.GetJoystickNames();

        if(names[0] != "")
        {
            mouseGPpos.x += Input.GetAxisRaw("whoreizontal");
            mouseGPpos.y += Input.GetAxisRaw("vertickle");

            //mouseGPpos.x = (Input.mousePosition.x);
            // mouseGPpos.y = (Input.mousePosition.y);

            //mouseGPpos.x = (Input.GetAxisRaw("Horizontal"));
            //mouseGPpos.y = (Input.GetAxisRaw("Vertical"));



            int xPos = (int)mouseGPpos.x, yPos = (int)mouseGPpos.y;
            SetCursorPos(xPos, yPos);//Call this when you want to set the mouse position




            if (Input.GetButtonDown("Fire1"))
            {
                clik.ClickAt(-292, -116);
            }
        }
        

        
    }
}
