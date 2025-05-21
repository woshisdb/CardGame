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
        return new SelectSlotData((slot) =>
        {
            return slot is OneCardSlotView && ((OneCardSlotView)slot).ExistTag(SlotTag.AnimalSlot);
        },
        (slot) =>
        {
            State.End();
        });
    }

    public override SlotEffectData EffectData()
    {
        return new AttackSkilData(this);
    }
}