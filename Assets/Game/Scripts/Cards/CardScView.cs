using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScView : MonoBehaviour, IView
{
    public TextMeshPro title;
    public TextMeshPro description;
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
        title.text = cardModel.cardName;
        description.text = cardModel.cardDescription;
    }
}
