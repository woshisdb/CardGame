using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    public string text;
    public DialogueNode nextNode;
    public Func<PlayerEffect, bool> condition;
    public PlayerEffect playerEffect;
    public DialogueChoice(string text, DialogueNode nextNode, Func<PlayerEffect, bool> condition = null,PlayerEffect playerEffect = null)
    {
        this.text = text;
        this.nextNode = nextNode;
        this.condition = condition == null ? (e) => { return true; } :condition;
        this.playerEffect = playerEffect;
    }

    public bool IsAvailable(PlayerEffect stats)
    {
        return condition(stats);
    }
}

public class DialogueNode : ScriptableObject
{
    [Header("文本内容")]
    [TextArea]
    public string speakerText;
    public string speakerName;
    public Sprite portrait;
    public Action process;

    [Header("对话选项（可为空）")]
    public List<DialogueChoice> choices = new List<DialogueChoice>();

    [Header("没有选项时自动跳转的下一个节点")]
    public DialogueNode nextNode;

    public bool HasChoices => choices != null && choices.Count > 0;

    // 当前链式构建用字段（不序列化）
    private string currentSpeaker = null;

    // ----------- 链式构建 API -----------

    public static DialogueNode Line(string text, string speaker = null)
    {
        return new DialogueNode().SetText(text).SetSpeaker(speaker);
    }

    public DialogueNode SetSpeaker(string speaker)
    {
        this.speakerName = speaker;
        this.currentSpeaker = speaker;
        return this;
    }

    public DialogueNode SetText(string text)
    {
        this.speakerText = text;
        return this;
    }

    public void Process()
    {
        process?.Invoke();
    }
    public DialogueNode SetProcess(Action process)
    {
        this.process = process;
        return this;
    }
    
    public DialogueNode SetPortrait(Sprite sprite)
    {
        this.portrait = sprite;
        return this;
    }
    public DialogueNode SetPortrait(DialoguePortraitEnum spriteEnum)
    {
        this.portrait = GameArchitect.Instance.resConfig.DialoguePortraits[spriteEnum];
        return this;
    }

    public DialogueNode Next(string text, string speaker = null)
    {
        var next = new DialogueNode().SetText(text);
        next.SetSpeaker(speaker ?? this.currentSpeaker);
        this.nextNode = next;
        return next;
    }

    public DialogueNode Choice(params (string text, DialogueNode node)[] simpleChoices)
    {
        foreach (var (text, node) in simpleChoices)
        {
            this.choices.Add(new DialogueChoice(text, node));
        }
        return this;
    }

    public DialogueNode Choice(params (string text, DialogueNode node, Func<PlayerEffect, bool> condition)[] fullChoices)
    {
        foreach (var (text, node, condition) in fullChoices)
        {
            this.choices.Add(new DialogueChoice(text, node, condition));
        }
        return this;
    }
}