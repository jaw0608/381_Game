using System.Collections;
using UnityEngine;

public class DisplayOnEvent : MonoBehaviour
{
    [Tooltip("The text that will be displayed")]
    [TextArea]
    public string message;
    [Tooltip("Prefab for the message")]
    public PoolObjectDef messagePrefab;


    float m_InitTime = float.NegativeInfinity;

    bool displayNow = false;
    DisplayMessageManager m_DisplayMessageManager;

    private NotificationToast notification;

    void OnEnable()
    {
        m_InitTime = Time.time;
        if (m_DisplayMessageManager == null)
            m_DisplayMessageManager = FindObjectOfType<DisplayMessageManager>();

        DebugUtility.HandleErrorIfNullFindObject<DisplayMessageManager, DisplayMessage>(m_DisplayMessageManager, this);

    }

    //call to trigger event
    public void turnOn()
    {
        displayNow = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (displayNow == false) return;
        displayNow = false;
        Display();

    }

    public void Display()
    {
        notification = messagePrefab.getObject(true, m_DisplayMessageManager.DisplayMessageRect.transform).GetComponent<NotificationToast>();

        notification.Initialize(message);

        m_DisplayMessageManager.DisplayMessageRect.UpdateTable(notification.gameObject);

        StartCoroutine(messagePrefab.ReturnWithDelay(notification.gameObject, notification.TotalRunTime));

    }
}
