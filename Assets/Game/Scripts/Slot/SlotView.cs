using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SlotView : SerializedMonoBehaviour,IUISelector
{
    [ReadOnly]
    public string slotName;
    public TableView tableView;
    //public void Refresh()
    //{
    //}
    //public void AddCard(CardModel cardModel,Action done)
    //{
    //    this.cardModel = cardModel;
    //    var cardScView = cardModel.CreateCardScView();
    //    this.scView = cardScView;
    //    cardScView.transform.parent = transform;
    //    cardScView.transform.localPosition = Vector3.zero;
    //    done();
    //}

    public abstract List<UIItemBinder> GetUI();
    //{
    //    if(cardModel!=null)
    //    {
    //        return cardModel.GetUI(true);
    //    }
    //    else
    //    {
    //        var list = new List<UIItemBinder>();
    //        return list;
    //    }
    //}
}
