﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

public class ObjectiveReachTargets : Objective
{

    [Tooltip("Choose whether you need to collect all pickups or only a minimum amount")]
    public bool mustCollectAllPickups = true;
    
    [Tooltip("If MustCollectAllPickups is false, this is the amount of pickups required")]
    public int pickupsToCompleteObjective = 5;
    
    [Header("Notification")]
    [Tooltip("Start sending notification about remaining pickups when this amount of pickups is left")]
    public int notificationPickupsRemainingThreshold = 1;

    [Header("Display Message")]
    [Tooltip("GameObject that holds display message to trigger when a new path is opened")]
    public GameObject displayObject = null;


    [Header("Open Paths after this many objectives are picked up")]
    [Tooltip("Open Path below when this many objectives picked up")]
    public List<int> PickedUp_Objectives = new List<int>();

    [Tooltip("Path gameobject to open")]
    public List<GameObject> GameObjects = new List<GameObject>();

    [Header("SwapMeter")]
    [Tooltip("Object that contains the SwapMeter script")]
    public GameObject SwapMeterObject;
    [Tooltip("Value to add when meter when an object is picked up")]
    public int AddOnCollect;

    private Dictionary<int, List<GameObject>> paths = new Dictionary<int, List<GameObject>>();


    public void MakeDictionary()
    {

        for (int i = 0; i < PickedUp_Objectives.Count && i < GameObjects.Count; i++)
        {
            if (paths.ContainsKey(PickedUp_Objectives[i]) == false)
            {
                paths.Add(PickedUp_Objectives[i], new List<GameObject>());
            }
            paths[PickedUp_Objectives[i]].Add(GameObjects[i]);
            GameObjects[i].SetActive(false);
        }
    }




    IEnumerator Start()
    {

        MakeDictionary();

        TimeManager.OnSetTime(totalTimeInSecs, isTimed, gameMode);
        
        yield return new WaitForEndOfFrame();

        title = "Collect " +
                (mustCollectAllPickups ? "all the" : pickupsToCompleteObjective.ToString()) + " " +
                targetName + "s";
        
        if (mustCollectAllPickups)
            pickupsToCompleteObjective = NumberOfPickupsTotal;
        
        Register();
    }

    protected override void ReachCheckpoint(int remaining)
    {

        if (isCompleted)
            return;
        
        if (mustCollectAllPickups) 
            pickupsToCompleteObjective = NumberOfPickupsTotal;

        m_PickupTotal = NumberOfPickupsTotal - remaining;
        int targetRemaining = mustCollectAllPickups ? remaining : pickupsToCompleteObjective - m_PickupTotal;
        SwapMeterObject.GetComponent<SwapMeter>().AddTo(AddOnCollect);
        if (paths.ContainsKey(m_PickupTotal))
        {

            for (int i = 0; i < paths[m_PickupTotal].Count; i++)
            {
                paths[m_PickupTotal][i].SetActive(true);
            }
            paths.Remove(m_PickupTotal);
                
            if (displayObject!=null)
                displayObject.GetComponent<DisplayOnEvent>().turnOn();
        }

        foreach (int key in paths.Keys)
        {
            for (int i=0; i<paths[key].Count; i++)
            {
                paths[key][i].SetActive(false);
            }
        }

        // update the objective text according to how many enemies remain to kill
        if (targetRemaining == 0)
        {
            CompleteObjective(string.Empty, GetUpdatedCounterAmount(),
                "Objective complete: " + title);
        }
        else if (targetRemaining == 1)
        {
            string notificationText = notificationPickupsRemainingThreshold >= targetRemaining
                ? "One " + targetName + " left"
                : string.Empty;
            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
        else if (targetRemaining > 1)
        {
            // create a notification text if needed, if it stays empty, the notification will not be created
            string notificationText = notificationPickupsRemainingThreshold >= targetRemaining
                ? targetRemaining + " " + targetName + "s to collect left"
                : string.Empty;

            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }

    }

    public override string GetUpdatedCounterAmount()
    {
        return m_PickupTotal + " / " + pickupsToCompleteObjective;
    }
    
}
