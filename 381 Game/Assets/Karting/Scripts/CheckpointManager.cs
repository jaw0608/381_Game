using UnityEngine;
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

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name.Contains(targetName))
        {
            lastCheckPoint = collisionInfo.collider.gameObject;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {

    }

    void OnCollisionExit(Collision collisionInfo)
    {

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
        //teleport and face same orientation as object. set velocity to 0.
        this.gameObject.transform.position = lastCheckPoint.transform.position;
        this.gameObject.transform.rotation = lastCheckPoint.transform.rotation;
        rb.velocity = new Vector3(0, 0, 0);
    }
}