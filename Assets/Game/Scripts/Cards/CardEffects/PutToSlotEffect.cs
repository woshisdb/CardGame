using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutToSlotEffectData : CardEffectData
{
    public string Name;

    public PutToSlotEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }
}

[CreateAssetMenu(fileName = "PutToSlotEffect", menuName = "Effect/PutToSlotEffect")]
public class PutToSlotEffect : CardEffect, ISendEvent
{
    public PutToSlotEffect()
    {
        cardEffectEnum = CardEffectEnum.PutToSlotEffect;
    }

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
        return new PutToSlotEffectData(this);
    }
}
