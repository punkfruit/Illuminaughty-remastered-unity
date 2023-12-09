using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundBoard : MonoBehaviour
{
    public int soundToPlay;
    public bool normal;

    public Image img;
    public Sprite unChecked, Checked;
    public Text input;

    public void ChangeSound()
    {
        int sound;
        int.TryParse(input.text, out sound);
        soundToPlay = sound;
    }

    public void PlaySound()
    {
        if (normal)
        {
            AudioManager.instance.PlaySFXnormal(soundToPlay);
        }
        else
        {
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }

    public void ChangeNormal()
    {
        normal = !normal;

        if (normal)
        {
            img.sprite = Checked;
        }
        else
        {
            img.sprite = unChecked;
        }
    }

    public void stopSound()
    {
        AudioManager.instance.StopSFX(soundToPlay);
    }
}
