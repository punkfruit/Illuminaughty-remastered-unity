using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageCotroller : MonoBehaviour
{
    public bool multipleSegments;

    public GameObject navigator;
    public int currentSegment;
    public GameObject navPlus;
    public GameObject navMinus;
    public Text numb;
    public GameObject[] segments;

    void Start()
    {
        if (multipleSegments)
        {
            navigator.gameObject.SetActive(true);
            currentSegment = 0;
            SwitchSegment();
        }
        else
        {
            navigator.gameObject.SetActive(false);
        }
    }

   public void ChangeSegment(bool next)
    {
        if (next)
        {
            currentSegment++;
            if(currentSegment > segments.Length -1)
            {
                currentSegment = segments.Length -1;
            }
            SwitchSegment();
        }
        else
        {
            currentSegment--;
            if(currentSegment < 0)
            {
                currentSegment = 0;
            }
            SwitchSegment();
        }
    }

    void SwitchSegment()
    {
        for(int i = 0; i < segments.Length; i++)
        {
            segments[i].gameObject.SetActive(false);
        }
        segments[currentSegment].SetActive(true);

        if(currentSegment == segments.Length - 1)
        {
            navPlus.gameObject.SetActive(false);
        }
        else
        {
            navPlus.gameObject.SetActive(true);
        }

        if(currentSegment == 0)
        {
            navMinus.gameObject.SetActive(false);
        }
        else
        {
            navMinus.gameObject.SetActive(true);
        }

        numb.text = (currentSegment + 1).ToString();
    }

    private void OnEnable()
    {
        currentSegment = 0;
        SwitchSegment();
    }


}
