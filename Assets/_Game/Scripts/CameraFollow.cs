using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float speed;

    private void OnEnable() {
        EventManager.OnEventEmitted += OnEventEmitted;
    }

    private void OnDisable() {
        EventManager.OnEventEmitted -= OnEventEmitted;
    }

    private void OnEventEmitted(EventID eventID) {
        switch (eventID) {
            case EventID.Replay:
            case EventID.NextLevel:
                OnReset();
                break;
            case EventID.WinningLevel:
                RaceToWin();
                break;
        }
    }

    private void LateUpdate() {
        Vector3 pos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }

    public void RaceToWin() {
        offset += Vector3.right * 3.5f;
        transform.rotation = Quaternion.Euler(25, -20, 0);
    }

    public void OnReset() {
        offset.x = 0;
        transform.rotation = Quaternion.Euler(25, 0, 0);
    }
}
