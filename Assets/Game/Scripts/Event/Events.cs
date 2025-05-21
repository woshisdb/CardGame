using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SelectViewEvent : IEvent
{
    public IUISelector view;
    public SelectViewEvent(IUISelector view)
    {
        this.view = view;
    }
}

public struct ChangeEvent:IEvent
{
    public TableCircleEnum circleEnum;
    public ChangeEvent(TableCircleEnum tableCircle)
    {
        this.circleEnum = tableCircle;
    }
}

public struct SelectSlotEvent:IEvent
{
    /// <summary>
    /// SlotModel
    /// </summary>
    public SlotView slot;
    public SelectSlotEvent(SlotView slotModel)
    {
        this.slot = slotModel;
    }
}

public struct CardEffectEvent:IEvent
{
    public ICardEffect effect;
    public CardModel cardModel;
    public CardEffectEvent(ICardEffect effect,CardModel cardModel)
    {
        this.effect = effect;
        this.cardModel = cardModel;
    }
}

public struct SlotEffectEvent : IEvent
{
    public ISlotEffect effect;
    public SlotView slotView;
    public SlotEffectEvent(ISlotEffect effect, SlotView slotView)
    {
        this.effect = effect;
        this.slotView = slotView;
    }
}