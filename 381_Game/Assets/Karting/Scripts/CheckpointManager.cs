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

    [Tooltip("GameObject that holds display message to trigger when an blockade is destroyed")]
    public GameObject displayObject = null;

    public AudioSource onPassCheckpoint;

    void Start()
    {
        lastCheckPoint = GameObject.Find(targetName);
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.name.Contains(targetName) && collider.gameObject!=lastCheckPoint)
        {
            
            lastCheckPoint = collider.gameObject;
            if (onPassCheckpoint != null) onPassCheckpoint.Play(0);
            if (displayObject != null)
                displayObject.GetComponent<DisplayOnEvent>().turnOn();
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
        rb.angularVelocity = new Vector3(0, 0, 0);
    }
}