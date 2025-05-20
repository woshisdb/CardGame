using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffEffectData:EffectData
{
    public string Name;

    public AddBuffEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }
}

[CreateAssetMenu(fileName = "AddBuffEffect", menuName = "Effect/AddBuffEffect")]
public class AddBuffEffect : CardEffect,ISendEvent
{
    public override bool CanExe(EffectData effectData, TableModel table, CardModel card)
    {
        return true;
    }

    public override TableExeData Effect(EffectData effectData, TableModel table, CardModel card)
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

    public override EffectData EffectData()
    {
        return new AddBuffEffectData(this);
    }
}
