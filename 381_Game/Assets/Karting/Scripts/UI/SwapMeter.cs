using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapMeter : MonoBehaviour
{
    public int MeterMax;
    public int CurrentMeter;
    public bool isEmpty { get; private set; }

    public UnityEngine.UI.Slider SwapMeterBar;

    [Header("Danger Warning")]
    public Color DangerColor;
    public int DangerAmount = 1;


    private Color StartColor;
    private Image BackgroundImage;

    private bool isChangingColor = false;

    // Start is called before the first frame update
    void Start()
    {
        SwapMeterBar.value = Percent();
        isEmpty = false;
        BackgroundImage = SwapMeterBar.transform.GetChild(0).gameObject.GetComponent<Image>();
        StartColor = BackgroundImage.color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Decrement()
    {
        CurrentMeter -= 1;
        UpdateBar();
        if (Percent() <= 0)
        {
            CurrentMeter = 0;
            isEmpty = true;
        }
    }

    void onLast()
    {
        
        int val = CurrentMeter - DangerAmount;
        if ((float)val / (float)MeterMax <= 0)
        {
            if (isChangingColor == false)
            {
                InvokeRepeating("ChangeColor", 0.0f, 0.5f);
                isChangingColor = true;
            }
            
        }
        else
        {
            CancelInvoke("ChangeColor"); //cancel any current Invoke Repeats
            isChangingColor = false;
            BackgroundImage.color = StartColor;
        }
    }

    void ChangeColor()
    {
        if (BackgroundImage.color == StartColor) BackgroundImage.color = DangerColor;
        else BackgroundImage.color = StartColor;
    }

    public void AddTo(int val)
    {
        if (CurrentMeter + val < MeterMax) CurrentMeter += val;
        else CurrentMeter = MeterMax;
        UpdateBar();
    }

    float Percent()
    {
        return (float)CurrentMeter / (float)MeterMax;
    } 

    void UpdateBar()
    {
        SwapMeterBar.value = Percent();
        onLast();
    }

}
