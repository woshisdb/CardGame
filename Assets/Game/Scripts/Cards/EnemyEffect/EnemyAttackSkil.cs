using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkilData : SlotEffectData
{
    public string Name;

    public AttackSkilData(ISlotEffect slotEffect) : base(slotEffect)
    {
    }
}

[CreateAssetMenu(fileName = "EnemyAttackSkil", menuName = "Effect/EnemyAttackSkil")]
public class EnemyAttackSkil : SlotEffect, ISendEvent
{
    public override bool CanExe(SlotEffectData effectData, TableModel table, SlotView slot)
    {
        return true;
    }

    public override TableExeData Effect(SlotEffectData effectData, TableModel table, SlotView card)
    {
        var heroSlot = table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView;
        var hero = heroSlot.cardModel as HeroCardModel;
        return new EndData();
    }

    public override SlotEffectData EffectData()
    {
        return new AttackSkilData(this);
    }
}