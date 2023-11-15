using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager {
    public static event UnityAction<EventID> OnEventEmitted;
    public static void EmitEvent(EventID eventID) => OnEventEmitted?.Invoke(eventID);
    
}
