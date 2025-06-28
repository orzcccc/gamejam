using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 事件类型 枚举
/// </summary>
public enum E_EventType
{
    /// <summary>
    /// 场景切换时进度条变化--float
    /// </summary>
    E_SceneLoadChange,

    /// <summary>
    /// 输入系统触发技能1 行为
    /// </summary>
    E_Input_Skill1,
    /// <summary>
    /// 输入系统触发技能2 行为
    /// </summary>
    E_Input_Skill2,
    /// <summary>
    /// 输入系统触发技能3 行为
    /// </summary>
    E_Input_Skill3,

    /// <summary>
    /// 水平/垂直热键
    /// </summary>
    E_Input_Horizontal,
    E_Input_Vertical,
}
