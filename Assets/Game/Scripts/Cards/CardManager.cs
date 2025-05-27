using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CardManager : MonoBehaviour,CardSetView
{
    public Transform hand;
    [ShowInInspector]
    public Dictionary<CardModel,CardUIView> cards;
    public void Awake()
    {
        cards = new Dictionary<CardModel, CardUIView>();
    }

    public void RemoveCards(List<CardModel> cards, Action done)
    {
        foreach (var card in cards)
        {
            RemoveCard(card,()=>{},()=>{});
        }
        done();
    }
    public void RemoveCard(CardModel card,Action onSucc,Action onFail)
    {
        GameObject.Destroy(cards[card].gameObject);
        cards.Remove(card);
        onSucc();
    }
    [Button]
    public void AddCard(CardModel card)
    {
        var obj = card.CreateView();
        ((CardUIView)obj).transform.SetParent(hand);
        cards.Add(card,(CardUIView)obj);
    }
    public void AddCard(CardModel card,Action onSucc,Action onFail)
    {
        var obj = card.CreateView();
        ((CardUIView)obj).transform.SetParent(hand);
        cards.Add(card, (CardUIView)obj);
        onSucc();
    }
    [Button]
    public void ClearCard()
    {
        foreach(var x in cards)
        {
            GameObject.Destroy(x.Value.gameObject);
        }
        cards.Clear();
    }
    [Button]
    public void AddCards(List<CardModel> cardModels)
    {
        //ClearCard();
        foreach(CardModel cardModel in cardModels)
        {
            AddCard(cardModel);
        }
    }
    [Button]
    public void TestCards()
    {
        AddCards(GameArchitect.Instance.saveSystem.saveFile.cards);
    }

    public void AddCardsAnim(List<CardModel> cards, Action done)
    {
        AddCards(cards);
        done();
    }

    public void RemoveCardsAnim(List<CardModel> cards, Action done)
    {
        RemoveCards(cards,done);
        done();
    }
}
