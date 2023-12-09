using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public bool PcBuild;

   public void OpenChannel(string link)
    {
        if (PcBuild)
        {
            Application.OpenURL(link);
        }
    }
}
