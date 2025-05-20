using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAttackCard", menuName = "SaveData/newAttackCard")]
public class AttackCardAsset : CardAsset
{
    public AttackCardAsset() : base()
    {
        cardEnum = CardEnum.AttackCard;
    }
}
