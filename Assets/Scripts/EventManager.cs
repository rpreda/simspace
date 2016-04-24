using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public enum EventType { CLICK, NOT_ENOUGH_RESOURCES };

    public delegate void ClickAction();
    public static event ClickAction OnClicked;
    public delegate void ResourceAction(Resource.ResourceType resType);
    public static event ResourceAction OnNotEnoughResources;

    public static void TriggerEvent(EventType evType)
    {
        switch (evType)
        {
            case EventType.CLICK:
                if (OnClicked != null)
                {
                    OnClicked();
                }
            break;
//             case EventType.NOT_ENOUGH_RESOURCES:
//                 if (OnNotEnoughResources != null)
//                 {
//                     OnNotEnoughResources();
//                 }
// 
//             break;
            default:
                //bla
            break;
        }
    }

    public static void TriggerResourceEvent(EventType evType, Resource.ResourceType resType)
    {
        switch (evType)
        {
            case EventType.NOT_ENOUGH_RESOURCES:
                if (OnNotEnoughResources != null)
                {
                    OnNotEnoughResources(resType);
                }

                break;
            default:
                //bla
                break;
        }
    }
}
