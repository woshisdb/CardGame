using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardModel : IUISelector, IModel, ISendEvent
{
    public CardAsset cardAsset;
    public CardEffectData cardEffectData;

    public TableModel TableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }

    public string cardName { get { return GetCardName(); } }
    public string cardDescription { get { return GetCardDescription(); } }

    public virtual string GetCardName()
    {
        return cardAsset.cardName;
    }

    public virtual string GetCardDescription()
    {
        return cardAsset.cardDescription;
    }

    public CardModel(CardAsset cardAsset)
    {
        this.cardAsset = cardAsset;
        cardEffectData = cardAsset.cardEffect.Clone();
    }

    public IView CreateView()
    {
        var cardUI = GameArchitect.Instance.resConfig.cardUITemplate;
        var obj = GameObject.Instantiate(cardUI[cardAsset.cardEnum].gameObject);
        obj.GetComponent<CardUIView>().BindModel(this);
        return obj.GetComponent<CardUIView>();
    }

    public CardScView CreateCardScView()
    {
        var cardUI = GameArchitect.Instance.resConfig.cardSCTemplate;
        var obj = GameObject.Instantiate(cardUI[cardAsset.cardEnum].gameObject);
        var cardScView = obj.GetComponent<CardScView>();
        cardScView.BindModel(this);
        return cardScView;
    }

    public virtual void Refresh(CardScView cardScView)
    {
        cardScView.items["title"].text = this.cardName;
        cardScView.items["description"].text = this.cardDescription;
    }
    public virtual void Refresh(CardUIView cardUIView)
    {
        cardUIView.items["title"].text = this.cardName;
        cardUIView.items["description"].text = this.cardDescription;
    }

    public virtual void OnAddSlot(SlotView slotView)
    {
        
    }
    public virtual void OnRemoveSlot(SlotView slotView)
    {

    }
    public virtual List<UIItemBinder> GetUI(bool fromScene)
    {
        var ret = new List<UIItemBinder>();
        ret.Add(new KVItemBinder(() =>
        {
            return "Title";
        }, () =>
        {
            return cardAsset.cardName;
        }));
        ret.Add(new KVItemBinder(() =>
        {
            return "Description";
        }, () =>
        {
            return cardAsset.cardDescription;
        }));
        if (!fromScene && cardAsset.cardEffect!=null)
        {
            if (cardAsset.cardEffect.cardEffect.CanExe(cardAsset.cardEffect,TableModel,this))
            {
                ret.Add(new ButtonBinder(() =>
                {
                    return "use";
                }, () =>
                {
                    this.SendEvent(new CardEffectEvent(cardAsset.cardEffect, this));
                }));
            }
        }
        return ret;
    }
    public List<UIItemBinder> GetUI()
    {
        return GetUI(false);
    }
}
