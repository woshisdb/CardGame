using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class PureLogicDialogue
{
    public Action Done;
    public DialogueNode currentNode;
    public PureLogicDialogue(DialogueNode tree, Action done)
    {
        this.currentNode = tree;
        this.Done=done;
    }
    public void Process()
    {
        //currentNode.Process();
        if (currentNode.HasChoices)
        {
            var ret = currentNode.GetNpc(currentNode.speakerName).GetChoice(currentNode);
            currentNode = ret.nextNode;
        }
        else if (currentNode.nextNode != null)
        {
            currentNode.Process(() => {
                currentNode = currentNode.nextNode;
                Process();
            });
        }
        else//结束
        {
            Done();
        }
    }
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speakerNameText;
    public Image portraitImage;
    public GameObject choicesPanel;
    public Button choiceButtonPrefab;
    public Button continueButton;
    private DialogueNode currentNode;
    public Action Done;

    public void StartDialogue(DialogueNode tree,Action done)
    {
        bool isPureLogic = true;
        foreach (var player in tree.DialogueEnvir.players)
        {
            if(player.Value.IsPlayer())
            {
                isPureLogic = false;
            }
        }
        if (isPureLogic)
        {
            new PureLogicDialogue(tree,done).Process();
        }
        else//显示动画
        {
            GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.DialogueUI);
            currentNode = tree;
            this.Done = done;
            DisplayCurrentNode();
        }
    }

    void DisplayCurrentNode()
    {
        speakerText.text = currentNode.speakerText;
        speakerNameText.text = currentNode.speakerName;
        portraitImage.sprite = currentNode.portrait;
        //currentNode.Process();
        foreach (Transform child in choicesPanel.transform)
            Destroy(child.gameObject);

        if (currentNode.HasChoices)
        {
            if (!currentNode.GetNpc().IsPlayer())
            {
                var ret = currentNode.GetNpc().GetChoice(currentNode);
                speakerText.text += "(" + ret.text + ")";
                //OnChoiceSelected(ret);
                continueButton.gameObject.SetActive(true);
                continueButton.onClick.RemoveAllListeners();
                continueButton.onClick.AddListener(() =>
                {
                    currentNode = ret.nextNode;
                    DisplayCurrentNode(); // 自动跳转
                });
            }
            else//是玩家
            {
                continueButton.gameObject.SetActive(false);
                foreach (var choice in currentNode.choices)
                {
                    if (!choice.IsAvailable(choice.playerEffect)) continue;
                    var btn = Instantiate(choiceButtonPrefab, choicesPanel.transform);
                    btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => OnChoiceSelected(choice));
                }
            }
        }
        else if (currentNode.nextNode != null)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                var cur = currentNode;
                currentNode.Process(() =>
                {
                    currentNode = currentNode.nextNode;
                    DisplayCurrentNode(); // 自动跳转
                });
            });
        }
        else
        {
            EndDialogue();
        }
    }

    void OnChoiceSelected(DialogueChoice choice)
    {
        if (choice != null)
        {
            currentNode = currentNode.nextNode;
        }
        currentNode = choice.nextNode;
        DisplayCurrentNode();
    }

    void EndDialogue()
    {
        Done?.Invoke();
        speakerText.text = "对话结束";
        foreach (Transform child in choicesPanel.transform)
            Destroy(child.gameObject);
        GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.cellUI);
    }

}