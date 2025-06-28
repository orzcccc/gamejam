using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;
    public Button StartBtn;

    [HideInInspector]
    public List<MoveType> moveList = new List<MoveType>();
    private void Start()
    {
        Up.onClick.AddListener(OnClickBtnUp);
        Down.onClick.AddListener(OnClickBtnDown);
        Left.onClick.AddListener(OnClickBtnLeft);
        Right.onClick.AddListener(OnClickBtnRight);
        StartBtn.onClick.AddListener(OnClickBtnStart);
    }

    void OnClickBtnUp()
    {
        moveList.Add(MoveType.up);
    }
    
    void OnClickBtnDown()
    {
        moveList.Add(MoveType.down);
    }
    
    void OnClickBtnLeft()
    {
        moveList.Add(MoveType.left);
    }
    
    void OnClickBtnRight()
    {
        moveList.Add(MoveType.right);
    }
    
    void OnClickBtnStart()
    {
    }
}
