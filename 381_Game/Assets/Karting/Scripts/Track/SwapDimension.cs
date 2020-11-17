using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDimension : MonoBehaviour { 

    public GameObject DimensionA;
    public Color AColor;
    public GameObject DimensionB;
    public Color BColor;
    private bool onA;
    private bool wasPressed;
    private bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        onA = true;
        RenderSettings.skybox.SetColor("_Tint", AColor);
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
                RenderSettings.skybox.SetColor("_Tint", AColor);
            else
                RenderSettings.skybox.SetColor("_Tint", BColor);

        }
    }
}
