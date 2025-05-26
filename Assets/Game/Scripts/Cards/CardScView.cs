using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScView : SerializedMonoBehaviour, IView
{
    //public TextMeshPro title;
    //public TextMeshPro description;
    public Dictionary<string,TextMeshPro> items=new Dictionary<string, TextMeshPro>();
    public CardModel cardModel;
    public void BindModel(IModel model)
    {
        cardModel = (CardModel)model;
        Refresh();
    }

    public IModel GetModel()
    {
        return cardModel;
    }

    public void Refresh()
    {
        cardModel.Refresh(this);
        //title.text = cardModel.cardName;
        //description.text = cardModel.cardDescription;
    }
}
