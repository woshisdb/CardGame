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
                var mapper = new Dictionary<string, INpc>();
                mapper.Add("老师",GetNpc(sleeper));
                var ret = new SleepDialogueStoryBuilder(new DialogueEnvir(mapper)).build();
                GameArchitect.Instance.dialogueManager.StartDialogue(ret, e);
            });
        }
        actionQueue.Run(done);
    }
    public SleepPlan():base()
    {
        PlanEnum = PlanEnum.Sleep;
    }
}


public class SleepDialogueStoryBuilder : DialogueStoryBuilder
{
    public string speaker = "老师";
    public SleepDialogueStoryBuilder(DialogueEnvir envir) : base(envir)
    {
    }

    public override DialogueNode build()
    {
        var afterYes = new DialogueNode(this.envir)
            .SetText("太棒了，我们马上开始！")
            .SetSpeaker(speaker);
        var afterNo = new DialogueNode(this.envir)
            .SetText("没关系，我会等你准备好。")
            .SetSpeaker(speaker);
        DialogueNode root = new DialogueBuilder(this.envir)
            .Start("你好！", speaker)
            .Next("欢迎来到新学期", speaker)
            .Next("你准备好了吗？", speaker)
            .Choice(
                ("是的！（勇气3）", afterYes, (e) => { return 0.5f; }, null, null),
                ("我还没准备好", afterNo, (e) => { return 0.3f; }, null, null)
            )
            .Build();

        // 用 root 开始对话系统
        return root;
    }
}