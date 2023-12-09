using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public int soundToPlay;
    public GameObject cubePrefab;
    

    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(soundToPlay);
    }

    public void SpawnTheCube()
    {
        Instantiate(cubePrefab, transform.position, transform.rotation);
    }

    public void SelfDef()
    {
        Destroy(gameObject);
    }
}
