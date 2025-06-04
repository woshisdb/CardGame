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
    [ShowInInspector,SerializeField]
    protected CardEffectData cardEffect;
    [Button]
    public void AddEffect(CardEffect cardEffect)
    {
        this.cardEffect=cardEffect.EffectData();
    }
    public virtual CardModel CreateCardModel()
    {
        return new CardModel(this);
    }
    public CardEffectData CloneCardEffectData()
    {
        return cardEffect.Clone();
    }
    public bool CanExe(CardEffectData effectData, TableModel table, CardModel card)
    {
        return cardEffect.cardEffect.CanExe(effectData, table, card);
    }
}
