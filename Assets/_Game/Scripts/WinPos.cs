using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(GameTag.Player.ToString())) {
            EventManager.EmitEvent(EventID.WinLevel);
        }
    }
}
