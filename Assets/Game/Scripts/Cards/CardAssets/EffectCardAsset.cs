using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCardModel : CardModel
{
    public int power;
    public EffectCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        this.power = ((EffectCardAsset)cardAsset).power;
    }
    public override void Refresh(CardUIView cardUIView)
    {
        base.Refresh(cardUIView);
        cardUIView.items["power"].text = this.power.ToString();
    }
}

[CreateAssetMenu(fileName = "newEffectCard", menuName = "SaveData/newEffectCard")]
public class EffectCardAsset : CardAsset
{
    public int power;
    public EffectCardAsset() : base()
    {
        cardEnum = CardEnum.EffectCard;
    }
    public override CardModel CreateCardModel()
    {
        return new EffectCardModel(this);
    }
}