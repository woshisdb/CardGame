using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DayDate
{
    public Action<int,WeekDayType,DayTimeType,Action>[] date;
    public Action<int,WeekDayType,Action> dayBegin;

    public DayDate(Action<int,WeekDayType,Action> passDay,Action<int,WeekDayType,DayTimeType,Action> morning,Action<int,WeekDayType,DayTimeType,Action> afternoon,Action<int,WeekDayType,DayTimeType,Action> evening)
    {
        this.dayBegin = passDay;
        date = new Action<int,WeekDayType,DayTimeType,Action>[]
        {
            morning,
            afternoon,
            evening
        };
    }
}

public class DayDates
{
    public List<DayDate> Dates;

    private void morning(int week, WeekDayType weekDay, DayTimeType dayTime, Action done)
    {
        if ((int)weekDay>=(int)WeekDayType.D1&&(int)weekDay<=(int)WeekDayType.D5&&dayTime==DayTimeType.morning)
        {
            
        }
        else
        {
            
        }
    }
    private void afternoon(int week, WeekDayType weekDay, DayTimeType dayTime, Action done)
    {
        
    }
    private void evening(int week, WeekDayType weekDay, DayTimeType dayTime, Action done)
    {
        
    }

    private void dayBegin(int week, WeekDayType day, Action done)
    {
        
    }

    private void GoToSchool()
    {
        
    }
    public DayDates()
    {
        Dates = new List<DayDate>()
        {
            new DayDate(dayBegin,morning,afternoon,evening),
        };
    }
}