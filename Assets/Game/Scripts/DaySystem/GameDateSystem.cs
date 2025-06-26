using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public enum WeekDayType
{
    D1,
    D2, 
    D3, 
    D4, 
    D5, 
    D6, 
    D7
}

public enum DayTimeType
{
    sunup,
    morning,
    afternoon,
    evening
}

public class GameDateSystem:SerializedMonoBehaviour
{
    public TextMeshProUGUI day;
    public GameObject passDayAnim;
    public int TotalDaysInTerm = 90;
    public string[] WeekDays = { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };

    public int CurrentDay { get; private set; } = 1;
    public int ActionsPerDay = 3;
    public int RemainingActions { get; private set; }

    public GameActionQueue OnDateChanged;
    public GameActionQueue OnActionChanged;

    public GameDateSystem()
    {
        OnDateChanged = new GameActionQueue();
        OnActionChanged = new GameActionQueue();
        OnDateChanged.Add(PassDayAnim);
    }

    public void PassDayAnim(Action done)
    {
        done?.Invoke();
    }
    public void Init(SaveData saveData)
    {
        RemainingActions = saveData.saveFile.RemainingActions;
        CurrentDay = saveData.saveFile.CurrentDay;
        SetDay();
    }

    /// <summary>执行一次行动</summary>
    public bool UseAction()
    {
        if (RemainingActions > 0)
        {
            RemainingActions--;
            SetDay();
            OnActionChanged.Run(() =>
            {
                if (RemainingActions == 0)
                {
                    AdvanceDay();
                }
                else
                {
                    UseAction();
                }
            });
            return true;
        }
        else
        {
            Debug.LogWarning("行动次数已用完");
            return false;
        }
    }
    public void StartDatePass()
    {
        UseAction();
    }
    public void SetDay()
    {
        day.text = GetDateDisplay()+":"+GetProgressDisplay();
    }

    /// <summary>推进一天</summary>
    public void AdvanceDay()
    {
        if (CurrentDay < TotalDaysInTerm)
        {
            CurrentDay++;
            RemainingActions = ActionsPerDay;
            SetDay();
            OnDateChanged.Run(() =>
            {
                UseAction();
            });
        }
        else
        {
            Debug.Log("学期结束");
        }
    }

    public int GetCurrentWeek() => (CurrentDay - 1) / 7 + 1;
    public string GetWeekDay() => WeekDays[(CurrentDay - 1) % 7];
    public int GetWeekDayInt() => ((CurrentDay - 1) % 7)+1;

    public WeekDayType GetWeekDayType()
    {
        return (WeekDayType)((CurrentDay - 1) % 7);
    }
    public string GetDateDisplay() => $"第{GetCurrentWeek()}周|{GetWeekDay()}|第{CurrentDay}天|";
    public string GetProgressDisplay()
    {
        if (RemainingActions==3)
        {
            return "晨";
        }
        else if (RemainingActions==2)
        {
            return "早";
        }
        else if(RemainingActions==1)
        {
            return "中";
        }
        else
        {
            return "晚";
        }
    }
    public DayTimeType GetProgressDisplayInt()
    {
        if (RemainingActions==3)
        {
            return DayTimeType.sunup;
        }
        else if (RemainingActions==2)
        {
            return DayTimeType.morning;
        }
        else if(RemainingActions==1)
        {
            return DayTimeType.afternoon;
        }
        else
        {
            return DayTimeType.evening;
        }
    }

    public bool IsWeekDay(WeekDayType weekDay)
    {
        // 当前是第几天 % 7 得到的是 0~6，WeekDayType 的值也正好是从 D1（0）到 D7（6）
        return (int)weekDay == (CurrentDay - 1) % 7;
    }
    public float GetProgress() => (float)CurrentDay / TotalDaysInTerm;
}