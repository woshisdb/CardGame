using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBuilder
{
    private DialogueNode startNode;
    private DialogueNode currentNode;
    private List<DialogueNode> createdNodes = new List<DialogueNode>();

    public DialogueBuilder Start(string text, string speaker = "", Sprite portrait = null, Action<Action> process =null)
    {
        currentNode = CreateNode(text, speaker, portrait, process);
        startNode = currentNode;
        return this;
    }

    public DialogueBuilder Next(string text, string speaker = "", Sprite portrait = null,Action<Action> process=null)
    {
        var next = CreateNode(text, speaker, portrait, process);
        currentNode.nextNode = next;
        currentNode = next;
        return this;
    }

    public DialogueBuilder Choice(params (string text, DialogueNode node,Func<INpc,float> aiplan,Func<PlayerEffect,bool> canContinue,PlayerEffect effect)[] choices)
    {
        foreach (var choice in choices)
        {
            var decision = (choice.canContinue == null)? e => { return true;}:choice.canContinue;
            currentNode.choices.Add(new DialogueChoice(choice.text, choice.node,choice.aiplan,choice.canContinue,choice.effect));
        }

        return this;
    }

    public DialogueNode Build()
    {
        return startNode;
    }

    private DialogueNode CreateNode(string text, string speaker, Sprite portrait,Action<Action> process)
    {
        var node = new DialogueNode(envir);
        node.process = process;
        node.speakerText = text;
        node.speakerName = speaker;
        node.portrait = portrait;
        node.choices = new List<DialogueChoice>();
        createdNodes.Add(node);
        return node;
    }
    DialogueEnvir envir;
    public DialogueBuilder(DialogueEnvir dialogueEnvir)
    {
        envir = dialogueEnvir;
    }
    public List<DialogueNode> GetAllNodes() => createdNodes;
}