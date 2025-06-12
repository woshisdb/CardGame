using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI speakerNameText;
    public Image portraitImage;
    public GameObject choicesPanel;
    public Button choiceButtonPrefab;
    public Button continueButton;
    private DialogueNode currentNode;

    public void StartDialogue(DialogueNode tree)
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

        }
        else//显示动画
        {
            GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.DialogueUI);
            currentNode = tree;
            DisplayCurrentNode();
        }
    }

    void DisplayCurrentNode()
    {
        speakerText.text = currentNode.speakerText;
        speakerNameText.text = currentNode.speakerName;
        portraitImage.sprite = currentNode.portrait;
        currentNode.Process();
        foreach (Transform child in choicesPanel.transform)
            Destroy(child.gameObject);

        if (currentNode.HasChoices)
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
        else if (currentNode.nextNode != null)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                currentNode = currentNode.nextNode;
                DisplayCurrentNode(); // 自动跳转
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
        speakerText.text = "对话结束";
        foreach (Transform child in choicesPanel.transform)
            Destroy(child.gameObject);
        GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.cellUI);
    }
    //void StartGame()
    //{
    //    //DialogueEnvir dialogueEnvir;
    //    //var afterYes = new DialogueNode()
    //    //    .SetText("太棒了，我们马上开始！")
    //    //    .SetSpeaker("老师");
    //    //var afterNo = new DialogueNode()
    //    //    .SetText("没关系，我会等你准备好。")
    //    //    .SetSpeaker("老师");
    //    //DialogueNode root = new DialogueBuilder()
    //    //    .Start("你好！", "老师")
    //    //    .Next("欢迎来到新学期", "老师")
    //    //    .Next("你准备好了吗？", "老师")
    //    //    .Choice(
    //    //        ("是的！（勇气3）", afterYes,null,null),
    //    //        ("我还没准备好", afterNo,null,null)
    //    //    )
    //    //    .Build();

    //    //// 用 root 开始对话系统
    //    //StartDialogue(root);
    //}

}