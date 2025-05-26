using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class SlotView : SerializedMonoBehaviour,IUISelector
{
    public HashSet<SlotTag> slotTags=new HashSet<SlotTag>();
    [ReadOnly]
    public string slotName { get
        {
            return transform.name;
        } }
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
    public bool ExistTag(SlotTag tag)
    {
        if(slotTags!=null)
        {
            return slotTags.Contains(tag);
        }
        else
        {
            return false;
        }
    }
    public virtual void Update()
    {

    }
    public virtual void OnTouch(PointerEventData eventData)
    {

    }

}
