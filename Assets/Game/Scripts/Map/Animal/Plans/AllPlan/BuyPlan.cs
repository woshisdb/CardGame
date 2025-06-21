using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlan : PlanBase
{
    public string sleeper = "睡觉";
    public override bool CanRun()
    {
        return true;
    }

    public override void Run(Action done)
    {
        GameActionQueue actionQueue = new GameActionQueue();
        foreach (var x in GetNpcs(sleeper))
        {
            actionQueue.Add(e =>
            {
                var mapper = new Dictionary<string, INpc>();
                mapper.Add("老师", GetNpc(sleeper));
                var ret = new BuyDialogueStoryBuilder(new DialogueEnvir(mapper)).build();
                GameArchitect.Instance.dialogueManager.StartDialogue(ret, e);
            });
        }
        actionQueue.Run(done);
    }
    public BuyPlan() : base()
    {
        PlanEnum = PlanEnum.Sleep;
    }
}


public class BuyDialogueStoryBuilder : DialogueStoryBuilder
{
    public string seller = "店主";
    public string buyer = "卖家";
    public BuyDialogueStoryBuilder(DialogueEnvir envir) : base(envir)
    {
    }

    public override DialogueNode build()
    {
        DialogueNode root = new DialogueBuilder(this.envir)
            .Start("你好！", seller)
            .Next("要买什么书?", seller, null, e =>
            {
                //var obj = envir.GetNpc(buyer);
                GameArchitect.Instance.buySystem.BuyAction(envir.GetNpc(seller), envir.GetNpc(buyer),e);
            })
            .Build();

        // 用 root 开始对话系统
        return root;
    }
}