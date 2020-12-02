﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDimension : MonoBehaviour { 

    public GameObject DimensionA;
    public Color AColor;
    public GameObject DimensionB;
    public Color BColor;
    public GameObject Blockades;
    private bool onA;
    private bool wasPressed;
    private bool isPressed;

    public GameObject CameraObject;
    public GameObject GroundPlane;

    private Renderer GroundRenderer;
    private Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        Cam = CameraObject.GetComponent<Camera>();
        GroundRenderer = GroundPlane.GetComponent<Renderer>();
        onA = true;
        Cam.backgroundColor = AColor;
        GroundRenderer.material.color = AColor;
        changeChildren(DimensionA, BColor);
        changeChildren(DimensionB, AColor);
        wasPressed = false;
        isPressed = false;
        DimensionB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        wasPressed = Input.GetKeyUp(KeyCode.Return);
        isPressed = Input.GetKeyDown(KeyCode.Return);

        //pressed but not still being held down
        if (isPressed==true && wasPressed == false)
        {
            //make inactive and scaled to zero
            onA = !onA;
            DimensionA.SetActive(onA);
            DimensionB.SetActive(!onA);
            if (onA)
            {
                Cam.backgroundColor = AColor;
                GroundRenderer.material.color = AColor;
            }
                
            else
            {
                Cam.backgroundColor = BColor;
                GroundRenderer.material.color = BColor;
            }

        }
    }

    private void changeChildren(GameObject obj, Color color)
    {
        foreach (Transform child in obj.transform)
        {
            changeChildren(child.gameObject, color);
        }
        Renderer r = obj.GetComponent<Renderer>();
        if (r != null)
        {
            r.material.color = color;
        }
    }

}