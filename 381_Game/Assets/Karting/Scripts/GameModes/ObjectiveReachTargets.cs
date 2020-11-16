﻿﻿using System.Collections;
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
    [Tooltip("GameObject that holds display message to trigger when an blockade is destroyed")]
    public GameObject displayObject = null;


    [Header("Destroy at Target Reached")]
    [Tooltip("Destroy Gameobject below when there at this many targets remaining")]
    public List<int> Remaining_Targets = new List<int>();

    [Tooltip("GameObject to Destroy")]
    public List<GameObject> GameObjects = new List<GameObject>();

    private Dictionary<int, GameObject> destroyBlocks = new Dictionary<int, GameObject>();


    public void MakeDictionary()
    {

        for (int i = 0; i < Remaining_Targets.Count && i < GameObjects.Count; i++)
        {
            destroyBlocks.Add(Remaining_Targets[i], GameObjects[i]);
        }
    }




    IEnumerator Start()
    {

        MakeDictionary();

        UnityEngine.Debug.Log(destroyBlocks);

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

        if (destroyBlocks.ContainsKey(targetRemaining))
        {
            
            Destroy(destroyBlocks[targetRemaining]);
            if (displayObject!=null)
                displayObject.GetComponent<DisplayOnEvent>().turnOn();
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