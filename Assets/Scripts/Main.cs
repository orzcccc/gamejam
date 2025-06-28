    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Main : MonoBehaviour
{
    private AudioSource sour;
    private float v;
    private int TimerID;
    private void Start()
    {

        //ABResMgr.Instance.LoadResAsync<GameObject>("model", "Sphere", (obj) => { Instantiateobj); });
        // UWQResMgr.Instance.LoadRes<string>("file://" + Application.streamingAssetsPath + "/text.txt", (str) => { Debug.Log(str); }, () => { Debug.Log("加载失败"); });

        /* ABMgr.Instance.LoadResAsync<GameObject>("model", "Sphere", (obj) =>
         {
             GameObject go = Instantiate(obj);
             go.name = "go";
         });
         ABMgr.Instance.LoadResAsync<GameObject>("model", "Sphere", (obj) =>
         {
             GameObject go = Instantiate(obj);
             go.name = "go";
         }, true);
         ABMgr.Instance.LoadResAsync<GameObject>("model", "Sphere", (obj) => {
             GameObject go = Instantiate(obj);
             go.name = "go";
         });*/
        // ABMgr.Instance.LoadRes<GameObject>("model", "Sphere");
        /*
        ResourcesMgr.Instance.LoadAsync<GameObject>("Test/Cube", Test);
        Debug.Log(ResourcesMgr.Instance.GetRefCount<GameObject>("Test/Cube"));

        ResourcesMgr.Instance.LoadAsync<GameObject>("Test/Cube", Test);
        Debug.Log(ResourcesMgr.Instance.GetRefCount<GameObject>("Test/Cube"));

        ResourcesMgr.Instance.UnloadAsset<GameObject>("Test/Cube",true, Test);

        Instantiate(ResourcesMgr.Instance.Load<GameObject>("Test/Cube"));
        Debug.Log(ResourcesMgr.Instance.GetRefCount<GameObject>("Test/Cube"));

        Instantiate(EditorResMgr.Instance.LoadEditorRes<GameObject>("Cylinder"));
        */
    }

    private void Update()
    {
        
        //计时器
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TimerID = TimerMgr.Instance.AddTimer(5000, () => { print("5s结束"); }
            , 1000, () => { print("1s"); });
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TimerMgr.Instance.RemoveTimer(TimerID);
        }
    }
    public void OnGUI()
    {
    }
}
