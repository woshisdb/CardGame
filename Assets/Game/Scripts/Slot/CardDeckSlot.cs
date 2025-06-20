﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public interface CardSetView
{
    void AddCardsAnim(List<CardModel> cards,Action done);
    void RemoveCardsAnim(List<CardModel> cards,Action done);
}

public class CardDeckModel
{
    public List<CardModel> cards= new List<CardModel>();

    public void AddCards(List<CardModel> newcards)
    {
        cards.AddRange(newcards);
    }

    public void RemoveCards(List<CardModel> newcards)
    {
        cards.RemoveAll(card => { return newcards.Contains(card);});
    }
    [Button]
    public void AddCard(CardAsset asset)
    {
        cards.Add(asset.CreateCardModel());
    }
}

public class CardDeckSlot : SlotView,ISendEvent,CardSetView
{
    public TextMeshPro text;
    public CardDeckModel cardDeckModel;
    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        foreach(var card in cardDeckModel.cards)
        {
            ret.Add(new KVItemBinder(() =>
            {
                return card.cardName;
            }, () =>
            {
                return "---------------";
            }));
        }
        return ret;
    }

    public override void OnTouch(PointerEventData eventData)
    {
        this.SendEvent(new SelectViewEvent(this));
    }

    public void AddCardsAnim(List<CardModel> cards, Action done)
    {
        cardDeckModel.AddCards(cards);
        done();
    }

    public void RemoveCardsAnim(List<CardModel> cards, Action done)
    {
        cardDeckModel.RemoveCards(cards);
        done();
    }

    public List<CardModel> GetCardsAnim(int num, Action done)
    {
        if (CanGetNum(num))
        {
            var ret = cardDeckModel.cards.GetRange(0,num);
            RemoveCardsAnim(ret,done);
            return ret;
        }
        else
        {
            return null;
        }
    }
    
    public bool CanGetNum(int num)
    {
        if (num<=cardDeckModel.cards.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        text.text = "卡组\n("+cardDeckModel.cards.Count.ToString()+")";
    }
}
