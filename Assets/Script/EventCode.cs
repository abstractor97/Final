using UnityEngine;
using System.Collections;

public class EventCode 
{
    /// <summary>
    /// 开始事件，需要延后初始化的组件订阅
    /// </summary>
    public static readonly string APP_START_GAME = "APP_START_GAME";
    /// <summary>
    /// 存档事件，当这个事件发出后,存档开始
    /// </summary>
    public static readonly string APP_SAVE_GAME = "APP_SAVE_GAME";
    /// <summary>
    /// 存档完成事件，当这个事件发出后，存档结束
    /// </summary>
    public static readonly string APP_SAVEOVER_GAME = "APP_SAVEOVER_GAME";


}
