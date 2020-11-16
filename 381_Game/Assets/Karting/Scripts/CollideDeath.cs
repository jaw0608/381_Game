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


public class CollideDeath : MonoBehaviour
{

    [Tooltip("Targert to check collision with")]
    public string targetName;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name.Contains(targetName))
        {
            this.gameObject.GetComponent<CheckpointManager>().Respawn();
        }
    }
}