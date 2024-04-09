using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionHandler : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + " collided with " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " trigger with " + other.gameObject.name);
    }
}
