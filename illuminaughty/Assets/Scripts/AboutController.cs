using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutController : MonoBehaviour
{
    public GameObject[] pages;
    public int currentPage;
    public string titlePage;


    private void Start()
    {
        SwitchPage(0);
    }
    public void SwitchPage(int page)
    {
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].gameObject.SetActive(false);
        }
        currentPage = page;
        pages[page].SetActive(true);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(titlePage);
    }

}
