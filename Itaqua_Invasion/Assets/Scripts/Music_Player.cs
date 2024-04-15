using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Music_Player : MonoBehaviour
{

    void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<Music_Player>().Length;

        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    
    
}
