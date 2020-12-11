using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlaySound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData data)
    {
        sound.Play(0);
    }

    public void OnPointerExit(PointerEventData data)
    {
        sound.Stop();
    }
}
