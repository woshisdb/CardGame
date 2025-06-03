using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
//[CreateAssetMenu(fileName = "newCardEffect", menuName = "SaveData/newCardEffect")]
public abstract class CardEffect : SerializedScriptableObject, ICardEffect
{
    [ReadOnly]
    public CardEffectEnum cardEffectEnum;
    public abstract bool CanExe(CardEffectData effectData, TableModel table, CardModel card);
    public abstract TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done);

    public abstract CardEffectData EffectData();

    public TableExeData Cost(int power,TableExeData tableExeData)
    {
        return new ChangePowerData(() => { State.Next(tableExeData); },power);
    }
    public abstract string GetEffectStr(CardEffectData data);
}
