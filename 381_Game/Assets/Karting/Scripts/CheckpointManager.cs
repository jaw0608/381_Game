﻿using UnityEngine;
using System.Collections;

/*References:
 * Teleport: https://gamedev.stackexchange.com/questions/118063/how-do-i-teleport-an-object-in-unity
 * Rotation: https://answers.unity.com/questions/373528/how-do-i-make-one-transform-rotation-the-same-as-a.html
 * Stopping Velocity: https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html
 * 
 * 
 * 
 * 
 */


public class CheckpointManager : MonoBehaviour
{

    [Tooltip("Base name of checkpoint targets")]
    public string targetName;

    private GameObject lastCheckPoint;

    private Rigidbody rb;

    void Start()
    {
        lastCheckPoint = GameObject.Find(targetName);
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.name.Contains(targetName))
        {
            
            lastCheckPoint = collider.gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        Debug.Log("Respawned to" + lastCheckPoint.name);
        //teleport and face same orientation as object. set velocity to 0.
        this.gameObject.transform.position = lastCheckPoint.transform.position;
        this.gameObject.transform.rotation = lastCheckPoint.transform.rotation;
        rb.velocity = new Vector3(0, 0, 0);
    }
}