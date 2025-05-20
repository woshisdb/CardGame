using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
//[CreateAssetMenu(fileName = "newCardEffect", menuName = "SaveData/newCardEffect")]
public abstract class CardEffect : SerializedScriptableObject, ICardEffect
{
    public abstract bool CanExe(EffectData effectData, TableModel table, CardModel card);
    public abstract TableExeData Effect(EffectData effectData, TableModel table, CardModel card);

    public abstract EffectData EffectData();
}
