using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollideCount : MonoBehaviour
{

    [Tooltip("Targets to check collision with")]
    public List<GameObject> Targets;

    private Dictionary<GameObject, int> Counts;
    private Dictionary<GameObject, Collider> Colliders;

    public int collideCount { get; private set; }

    void Start()
    {
        Counts = new Dictionary<GameObject, int>();
        Colliders = new Dictionary<GameObject, Collider>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        List<ContactPoint> contacts = new List<ContactPoint>();
        collisionInfo.GetContacts(contacts);
        foreach (ContactPoint contact in contacts)
        {
            if (Targets.Contains(contact.thisCollider.gameObject))
            {
                Counts[contact.thisCollider.gameObject] = 1;
                Colliders[contact.thisCollider.gameObject] = collisionInfo.collider;
                sumCounts();
            }
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        foreach (KeyValuePair<GameObject,Collider> entry in Colliders)
        {
            if (entry.Value==collisionInfo.collider)
            {
                Counts[entry.Key] = 0;
                sumCounts();
            }
        }
        wipeColliders();
    }

    void sumCounts()
    {
        collideCount = 0;
        foreach(int value in Counts.Values)
        {
            collideCount+=value;
        }
        Debug.Log("Count " +collideCount);
    }

    void wipeColliders()
    {
        int countWiped = 0;
        foreach (KeyValuePair<GameObject,int> entry in Counts)
        {

            if (entry.Value == 0)
            {
                countWiped++;
                Colliders[entry.Key] = null;
            }  
        }
        Debug.Log("Wiped " + countWiped + " colliders");
    }

}