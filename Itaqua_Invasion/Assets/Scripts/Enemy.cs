using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject DeathVFX;
    [SerializeField] GameObject HitVFX;
    GameObject ScoreTag;

    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int life = 2;

    ScoreBoard scoreBoard;


    // Start is called before the first frame update

    void Start()
    {
        
        scoreBoard = FindObjectOfType<ScoreBoard>();
        ScoreTag = GameObject.FindGameObjectWithTag("ParentSpawn");
        AddRigidBody();

    }

    void AddRigidBody()
    {
        Rigidbody addRB = gameObject.AddComponent<Rigidbody>();
        addRB.useGravity = false;
    }

    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemie();
    }
    void ProcessHit()
    {
        GameObject vfx = Instantiate(HitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = ScoreTag.transform;

        scoreBoard.IncreaseScore(scorePerHit);
        life -= 1;
    }
    void KillEnemie()
    {
        
        if (life < 1)
        {
            GameObject vfx = Instantiate(DeathVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = ScoreTag.transform;

            Destroy(gameObject);
        }
        
    }
   
}
