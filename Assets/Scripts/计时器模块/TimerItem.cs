using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerItem
{
    //唯一ID
    public int keyID;
    //计时结束后的委托回调
    public UnityAction overCallBack;
    //间隔一定时间去执行的委托回调
    public UnityAction callBack;

    /// <summary>
    /// 毫秒，表示计时器的总计时时间
    /// </summary>
    public int allTime;
    /// <summary>
    /// 记录一开始计时的总时间
    /// </summary>
    public int maxAllTime;

    /// <summary>
    /// 间隔执行回调的毫秒
    /// </summary>
    public int intervalTime;
    /// <summary>
    /// 记录一开始的间隔时间
    /// </summary>
    public int maxIntervalTime;

    //是否在进行计时
    public bool isRunning;

    /// <summary>
    /// 初始化计时器数据
    /// </summary>
    /// <param name="keyID"></param>
    /// <param name="allTime"></param>
    /// <param name="overCallBack"></param>
    /// <param name="intervalTime"></param>
    /// <param name="callBack"></param>
    public void InitInfo(int keyID,int allTime,UnityAction overCallBack,int intervalTime=0,UnityAction callBack = null)
    {
        this.keyID = keyID;
        this.maxAllTime =  this.allTime = allTime;
        this.overCallBack = overCallBack;
        this.maxIntervalTime = this.intervalTime = intervalTime;
        this.callBack = callBack;
        this.isRunning = true;
    }
    //重置计时器
    public void ResetTimer()
    {
        this.allTime = this.maxAllTime;
        this.intervalTime = this.maxIntervalTime;
        this.isRunning = true;
    }
    public void ResetInfo()
    {
        overCallBack = null;
        callBack = null;
    }
}