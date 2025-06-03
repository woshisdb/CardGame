using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCardModel : CardModel
{
    public int Power { get { return GetPower(); } }
    public EffectCardModel(CardAsset cardAsset) : base(cardAsset)
    {
    }
    public override void Refresh(CardUIView cardUIView)
    {
        base.Refresh(cardUIView);
        cardUIView.items["power"].text = Power.ToString();
    }
    public virtual int GetPower()
    {
        return cardEffectData.GetPower();
    }

    public override string GetCardDescription()
    {
        return cardEffectData.cardEffect.GetEffectStr(cardEffectData);
    }
}

[CreateAssetMenu(fileName = "newEffectCard", menuName = "Card/newEffectCard")]
public class EffectCardAsset : CardAsset
{
    public EffectCardAsset() : base()
    {
        cardEnum = CardEnum.EffectCard;
    }
    public override CardModel CreateCardModel()
    {
        return new EffectCardModel(this);
    }
}