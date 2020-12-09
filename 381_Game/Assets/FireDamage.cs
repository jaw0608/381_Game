using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public CheckpointManager cm;
    public ParticleSystem par; 
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        par = GetComponent<ParticleSystem>();
        cm = player.GetComponent<CheckpointManager>();
    }

    void OnParticleCollision(GameObject obj)
    {
        if(obj.tag == "Player")
        {
            Debug.Log(obj.tag + " has touched fire");
            cm.Respawn();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
