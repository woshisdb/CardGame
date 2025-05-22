using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardModel : IUISelector, IModel, ISendEvent
{
    public CardAsset cardAsset;
    public string cardName { get { return cardAsset.cardName; } }
    public string cardDescription { get { return cardAsset.cardDescription; } }
    public CardModel(CardAsset cardAsset)
    {
        this.cardAsset = cardAsset;
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
        if (!fromScene)
        {
            foreach (var x in cardAsset.cardEffects)
            {
                var eff = x;
                ret.Add(new ButtonBinder(() =>
                {
                    return "use";
                }, () =>
                {
                    this.SendEvent(new CardEffectEvent(eff.cardEffect, this));
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
