using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollisionHandler : MonoBehaviour
{
    [SerializeField] float DelayTime = 2f;
    [SerializeField] ParticleSystem explosionVFX;

    

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Player_Control>().enabled = false;
        Invoke("ProcessRestart", DelayTime);

        explosionVFX.Play(); ;              
    }

    void ProcessRestart()
    {       
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }  

}
