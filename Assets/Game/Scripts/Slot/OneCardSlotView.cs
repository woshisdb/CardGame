using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class OneCardSlotView : SlotView, IUISelector,ISendEvent
{
    public CardModel cardModel;
    public CardScView scView;
    public override void Update()
    {
        if(!IsEmpty())
        scView.Refresh();
    }
    public CardModel GetCardModel()
    {
        return cardModel;
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
    public void RemoveCard(CardModel cardModel,Action onSucc,Action onFail)
    {
        this.cardModel = null;
        cardModel.OnRemoveSlot(this);
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
    public bool IsEmpty()
    {
        if(cardModel==null)
        {
            return true;
        }
        return false;
    }
    public override void OnTouch(PointerEventData eventData)
    {
        if (tableView.tableModel != null && tableView.tableModel.circleEnum == TableCircleEnum.SelectSloting)
        {
            this.SendEvent<SelectSlotEvent>(new SelectSlotEvent(this));
        }
        else
        {
            this.SendEvent<SelectViewEvent>(new SelectViewEvent(this));
        }
    }
}
