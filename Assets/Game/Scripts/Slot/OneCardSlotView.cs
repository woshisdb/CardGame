using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class OneCardSlotView : SlotView, IUISelector
{
    public CardModel cardModel;
    public CardScView scView;
    public void Refresh()
    {
    }
    public void AddCard(CardModel cardModel,Action onSucc,Action onFail)
    {
        this.cardModel = cardModel;
        cardModel.OnAddSlot(this);
        var cardScView = cardModel.CreateCardScView();
        this.scView = cardScView;
        cardScView.transform.parent = transform;
        cardScView.transform.localPosition = Vector3.zero;
        onSucc();
    }

    public override List<UIItemBinder> GetUI()
    {
        if(cardModel!=null)
        {
            return cardModel.GetUI(true);
        }
        else
        {
            var list = new List<UIItemBinder>();
            return list;
        }
    }
}
