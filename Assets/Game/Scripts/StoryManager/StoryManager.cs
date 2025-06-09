using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public class StoryManager:SerializedMonoBehaviour
{
    public void Init()
    {
        GameArchitect.Instance.gameDateSystem.OnActionChanged.Add(e =>
        {
            var time = GetTime();
            ProcessAction(time.Item1,time.Item2,time.Item3);
        });
        GameArchitect.Instance.gameDateSystem.OnDateChanged.Add(e =>
        {
            var time = GetTime();
            ProcessDay(time.Item1,time.Item2);
        });
    }
    public (int, WeekDayType, DayTimeType) GetTime()
    {
        return (GameArchitect.Instance.gameDateSystem.GetCurrentWeek(),
            GameArchitect.Instance.gameDateSystem.GetWeekDayType(),
            GameArchitect.Instance.gameDateSystem.GetProgressDisplayInt()
            );
    }
    /// <summary>
    /// 当天开始的行为
    /// </summary>
    /// <param name="week"></param>
    /// <param name="day"></param>
    public void ProcessDay(int week,WeekDayType day)
    {
        
    }
    /// <summary>
    /// 一个行为结束后的检测
    /// </summary>
    /// <param name="week"></param>
    /// <param name="day"></param>
    /// <param name="dayTime"></param>
    public void ProcessAction(int week,WeekDayType day,DayTimeType dayTime)
    {
        
    }
    
}