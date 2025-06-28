using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        EventCenter.Instance.AddEventListener(E_EventType.InputUp, MoveUp);
        EventCenter.Instance.AddEventListener(E_EventType.InputDown, MoveDown);
        EventCenter.Instance.AddEventListener(E_EventType.InputLeft, MoveLeft);
        EventCenter.Instance.AddEventListener(E_EventType.InputRight, MoveRight);
    }
    
    void MoveUp()
    {
    }
    void MoveDown()
    {
    }
    void MoveLeft()
    {
    }
    void MoveRight()
    {
    }
}
