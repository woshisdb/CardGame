//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AngryTagEffect : MonoBehaviour
//{
//}



using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AngryTagEffectData : CardEffectData
{
    [ShowInInspector, SerializeField]
    protected int angry;
    [ShowInInspector, SerializeField]
    protected int power;
    public AngryTagEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
        var data = cardEffect as AngryTagEffect;
        angry = data.angry;
        power = data.power;
    }

    public override CardEffectData Clone()
    {
        var ret = new AngryTagEffectData(cardEffect);
        ret.angry = angry;
        ret.power = power;
        return ret;
    }

    public override int GetPower()
    {
        return gameRuleProcessor.Process(ProcessType.Power, tableModel.gameRule.owner, power);//power;
    }
    public int GetAngry()
    {
        return gameRuleProcessor.Process(ProcessType.Angry, tableModel.gameRule.owner, angry);
    }
}

[CreateAssetMenu(fileName = "AngryTagEffect", menuName = "CardEffect/AngryTagEffect")]
public class AngryTagEffect : CardEffect, ISendEvent
{
    public int power;
    public int angry;
    public AngryTagEffect()
    {
        cardEffectEnum = CardEffectEnum.AngryTag;
    }
    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AngryTagEffectData;
        Action angryEff = () =>
        {
            State.Next(new AddAngryBuffToAnimal(GameArchitect.Instance.heroCardModel,eff.GetAngry(), done));
        };
        return Cost(eff.GetPower(), new RemoveHandCardData(card, angryEff, () => { }));
    }
    public override CardEffectData EffectData()
    {
        return new AngryTagEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AngryTagEffectData;
        if (cd.GetAngry() < 0)
            return "减少" + Math.Abs(cd.GetAngry()) + "点愤怒";
        else if (cd.GetAngry() > 0)
        {
            return "增加" + Math.Abs(cd.GetAngry()) + "点愤怒";
        }
        else
        {
            return "什么都不做";
        }
    }
}