using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardModel : CardModel
{
    public int Power { get { return GetPower(); } }
    public AttackCardModel(CardAsset cardAsset) : base(cardAsset)
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

[CreateAssetMenu(fileName = "newAttackCard", menuName = "Card/newAttackCard")]
public class AttackCardAsset : CardAsset
{
    public AttackCardAsset() : base()
    {
        cardEnum = CardEnum.AttackCard;
    }
    public override CardModel CreateCardModel()
    {
        return new AttackCardModel(this);
    }
}
