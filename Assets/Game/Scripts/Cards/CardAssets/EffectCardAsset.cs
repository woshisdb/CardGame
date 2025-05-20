using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEffectCard", menuName = "SaveData/newEffectCard")]
public class EffectCardAsset : CardAsset
{
    public EffectCardAsset() : base()
    {
        cardEnum = CardEnum.EffectCard;
    }
}