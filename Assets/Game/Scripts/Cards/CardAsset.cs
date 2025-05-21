using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



[CreateAssetMenu(fileName = "newCard", menuName = "SaveData/newCard")]

public class CardAsset : SerializedScriptableObject
{
    public string cardName;
    public string cardDescription;
    [ReadOnly]
    public CardEnum cardEnum;
    public List<CardEffectData> cardEffects=new List<CardEffectData>();
    [Button]
    public void AddEffect(CardEffect cardEffect)
    {
        cardEffects.Add(cardEffect.EffectData());
    }
    public virtual CardModel CreateCardModel()
    {
        return new CardModel(this);
    }
}
