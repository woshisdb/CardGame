using System;
using UnityEngine;

public class GameDateSystem : Singleton<GameDateSystem>
{

    public int TotalDaysInTerm = 90;
    public string[] WeekDays = { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };

    public int CurrentDay { get; private set; } = 1;
    public int ActionsPerDay = 3;
    public int RemainingActions { get; private set; }

    public event AsyncQueue OnDateChanged;
    public event Action OnActionChanged;

    /// <summary>执行一次行动</summary>
    public bool UseAction()
    {
        if (RemainingActions > 0)
        {
            RemainingActions--;
            OnActionChanged?.Invoke();

            if (RemainingActions == 0)
            {
                AdvanceDay();
            }
            return true;
        }
        else
        {
            Debug.LogWarning("行动次数已用完");
            return false;
        }
    }

    /// <summary>推进一天</summary>
    public void AdvanceDay()
    {
        if (CurrentDay < TotalDaysInTerm)
        {
            CurrentDay++;
            RemainingActions = ActionsPerDay;
            OnDateChanged?.Invoke();
            OnActionChanged?.Invoke();
        }
        else
        {
            Debug.Log("学期结束");
        }
    }

    public int GetCurrentWeek() => (CurrentDay - 1) / 7 + 1;
    public string GetWeekDay() => WeekDays[(CurrentDay - 1) % 7];
    public string GetDateDisplay() => $"第{GetCurrentWeek()}周 {GetWeekDay()}（第{CurrentDay}天）";
    public float GetProgress() => (float)CurrentDay / TotalDaysInTerm;
}