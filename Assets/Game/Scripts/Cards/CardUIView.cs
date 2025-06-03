using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIView : SerializedMonoBehaviour,ISendEvent,IView
{
    public CardModel cardModel;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Dictionary<string, TextMeshProUGUI> items=new Dictionary<string, TextMeshProUGUI>();
    public Image image;

    public void BindModel(IModel model)
    {
        cardModel = (CardModel)model;
        Refresh();
    }

    public IModel GetModel()
    {
        return cardModel;
    }

    public void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (cardModel!=null)
        {
            cardModel.Refresh(this);
        }
        //title.SetText(cardModel.cardName);
        //description.SetText(cardModel.cardDescription);
    }

    public void XUpdate()
    {
        if(GameArchitect.Instance.GetTableModel().circleEnum == TableCircleEnum.SelectCarding)
        {
            this.SendEvent<SelectViewEvent>(new SelectViewEvent(cardModel));
        }
    }
}
