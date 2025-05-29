using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCardModel : CardModel
{
    public int power;
    public AttackCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        this.power = ((AttackCardAsset)cardAsset).power;
    }
    public override void Refresh(CardUIView cardUIView)
    {
        base.Refresh(cardUIView);
        cardUIView.items["power"].text = this.power.ToString();
    }
}

[CreateAssetMenu(fileName = "newAttackCard", menuName = "Card/newAttackCard")]
public class AttackCardAsset : CardAsset
{
    public int power;
    public AttackCardAsset() : base()
    {
        cardEnum = CardEnum.AttackCard;
    }
    public override CardModel CreateCardModel()
    {
        return new AttackCardModel(this);
    }
}
