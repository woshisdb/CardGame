using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPlan : PlanBase
{
    public string sleeper = "睡觉";
    public override bool CanRun()
    {
        return true;
    }

    public override void Run(Action done)
    {
        GameActionQueue actionQueue=new GameActionQueue();
        foreach(var x in GetNpcs(sleeper))
        {
            actionQueue.Add(e =>
            {
                GameArchitect.Instance.dialogueManager.StartDialogue(null,null);
                e();
            });
        }
        actionQueue.Run(done);
    }
}
