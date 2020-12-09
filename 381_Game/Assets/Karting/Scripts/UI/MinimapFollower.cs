using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollower : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public float DistanceAbove;
    [Tooltip("Switch to this distance when the key below is pressed")]
    public float SwitchDistance = 0;
    [Tooltip("Key to trigger switch")]
    public string SwitchKey = null;
    private bool Down = false;

    public bool changeCamera = false;
    public float GroundLevel = -3;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = ObjectToFollow.transform.position;
        pos.y += DistanceAbove;
        transform.position = pos;
        if (SwitchKey.Length == 0) SwitchKey = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchKey!=null && Input.GetKeyDown(SwitchKey))
        {
            Down = !Down;
        }
        Vector3 pos = ObjectToFollow.transform.position;
        if (Down)
        {
            pos.y += SwitchDistance;
        }
        else
        {
            pos.y += DistanceAbove;
        }
        
        transform.position = pos;

        //update far plane
        if (GetComponent<Camera>()!=null && changeCamera)
        {
            GetComponent<Camera>().farClipPlane = pos.y - GroundLevel;
        }

    }
}
