using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkilData : CardEffectData
{
    public string Name;

    public AttackSkilData(ICardEffect cardEffect) : base(cardEffect)
    {
    }
}

[CreateAssetMenu(fileName = "AttackSkil", menuName = "Effect/AttackSkil")]
public class AttackSkil : CardEffect, ISendEvent
{
    public override bool CanExe(CardEffectData effectData, TableModel table, CardModel card)
    {
        return true;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card)
    {
        return new SelectSlotData((slot) =>
        {
            return slot is OneCardSlotView;
        },
        (slot) =>
        {
            State.Next(new AddSlotCardData((OneCardSlotView)slot, card, () =>
            {
                State.Next(new RemoveHandCardData(card, () =>
                {
                    State.End();
                }));
            }));
        });
    }

    public override CardEffectData EffectData()
    {
        return new AttackSkilData(this);
    }
}