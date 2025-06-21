using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemUI : MonoBehaviour
{
    public Button leftUI;
    public Button rightUI;
    public TextMeshProUGUI uiText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI priceText;
    int maxNum;
    Item Item;
    public void SetItem(int maxSum,Item item,Action left,Action right)
    {
        titleText.text = item.Name;
        Item = item;
        maxNum = maxSum;
        leftUI.onClick.RemoveAllListeners();
        leftUI.onClick.AddListener(()=> { left(); });
        rightUI.onClick.RemoveAllListeners();
        rightUI.onClick.AddListener(() => { right(); });
    }
}
