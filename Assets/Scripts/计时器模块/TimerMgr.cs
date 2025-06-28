using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TimerMgr : BaseManager<TimerMgr>
{
    private int TIMER_KEY = 0;
    private Dictionary<int, TimerItem> timerDic = new Dictionary<int, TimerItem>();
    private List<TimerItem> delList = new List<TimerItem>();
    private Coroutine timer;

    private TimerMgr() { Start(); }

    //开启计时器管理器
    public void Start()
    {
        timer = MonoMgr.Instance.StartCoroutine(StartTiming());
    }
    
    IEnumerator StartTiming()
    {
        while (true)
        {
            //100毫秒进行一次计时
            yield return new WaitForSeconds(0.1f);
            //遍历所有的计时器
            foreach (TimerItem item in timerDic.Values)
            {
                if (!item.isRunning)
                {
                    continue;
                }
                //判断计时器是否有间隔时间的要求
                if (item.callBack != null)
                {
                    item.intervalTime -= (int)(0.1f * 1000);
                    if (item.intervalTime <= 0)
                    {
                        //执行间隔时间回调
                        item.callBack.Invoke();
                        item.intervalTime = item.maxIntervalTime;
                    }
                }
                item.allTime -= (int)(0.1f * 1000);
                if (item.allTime <= 0)
                {
                    item.overCallBack?.Invoke();
                    delList.Add(item);
                }
            }

            for(int i = 0; i < delList.Count; i++)
            {
                timerDic.Remove(delList[i].keyID);
            }
            delList.Clear();
        }
    }
    
    public int AddTimer(int allTime,UnityAction overCallBack,int intervalTime = 0,UnityAction callBack = null)
    {
        //构建唯一ID
        int keyID = ++TIMER_KEY;
        TimerItem timerItem = new TimerItem();
        timerItem.InitInfo(keyID, allTime, overCallBack, intervalTime, callBack);
        timerDic.Add(keyID, timerItem);
        return keyID;
    }

    public void RemoveTimer(int keyID)
    {
        if (timerDic.ContainsKey(keyID))
        {
            timerDic.Remove(keyID);
        }
    }
}
