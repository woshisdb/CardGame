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

    public override CardEffectData Clone()
    {
        var ret = new PutToSlotEffectData(cardEffect);
        ret.Name = Name;
        return ret;
    }

    public override int GetPower()
    {
        return 0;
    }
}

[CreateAssetMenu(fileName = "PutToSlotEffect", menuName = "CardEffect/PutToSlotEffect")]
public class PutToSlotEffect : CardEffect, ISendEvent
{
    public PutToSlotEffect()
    {
        cardEffectEnum = CardEffectEnum.PutToSlot;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done)
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
                    State.End(done);
                }));
            }));
        });
    }

    public override CardEffectData EffectData()
    {
        return new PutToSlotEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as PutToSlotEffectData;
        return "放置到卡牌上";
    }
}
