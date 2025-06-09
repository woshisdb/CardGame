using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroProperty
{
    knowledge,
    brave,
    charm,
    agility,
    strength
}
public enum AnimalTagEnum
{
    Angry
}
public class HeroCardModel : CardModel,IAnimalCard
{
    public int knowledge;//知识
    public int brave;//勇气
    public int charm;//吸引力
    public int agility;//敏捷
    public int strength;//力量
    public Dictionary<AnimalTagEnum, int> tags;
    //////////////////////////////
    public int hp;
    public SlotView slot;
    // public List<AttackProcesser> attackProcessers=new List<AttackProcesser>();
    public int TagInf(AnimalTagEnum animalTag)
    {
        if (!tags.ContainsKey(animalTag))
        {
            return 0;
        }
        return tags[animalTag];
    }
    public void AddProperty(HeroProperty heroProperty,int val)
    {
        if(heroProperty==HeroProperty.knowledge)
        {
            knowledge += val;
        }
        else if(heroProperty==HeroProperty.brave)
        {
            brave += val;
        }
        if(heroProperty==HeroProperty.charm)
        {
            charm += val;
        }
        if (heroProperty==HeroProperty.agility)
        {
            agility += val;
        }
        if(heroProperty == HeroProperty.strength)
        {
            strength += val;
        }
    }

    public HeroCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as HeroCardAsset;
        this.hp = asset.hp;
        tags=new Dictionary<AnimalTagEnum, int>();
        foreach (var x in Enum.GetValues(typeof(AnimalTagEnum)))
        {
            tags[(AnimalTagEnum)x] = 0;
        }
    }

    public int GetHp()
    {
        return this.hp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
        TryRefresh();
    }

    public void ChangeHp(int hp)
    {
        this.hp = this.hp+hp;
        TryRefresh();
    }
    public SlotView GetSlot()
    {
        return slot;
    }
    public override void OnAddSlot(SlotView slotView)
    {
        slot = slotView;
    }
    public override void Refresh(CardScView cardScView)
    {
        base.Refresh(cardScView);
        cardScView.items["hp"].text = this.hp.ToString();
    }
    public void TryRefresh()
    {
        if (slot != null)
        {
            slot.Update();
        }
    }

    public CardEnum GetCardType()
    {
        return cardAsset.cardEnum;
    }
    /// <summary>
    /// 获得伤害
    /// </summary>
    /// <returns></returns>
    // public int ProcessAttack(int val)
    // {
    //     int ret = val;
    //     for(int i=0;i< attackProcessers.Count; i++)
    //     {
    //         ret = attackProcessers[i].func(ret);
    //     }
    //     return ret;
    // }
    // public void RegisterAttackProcess(AttackProcesser attackProcesser)
    // {
    //     this.attackProcessers.Add(attackProcesser);
    // }
    // public void RemoveRegisterAttackProcess(AttackProcesser attackProcesser)
    // {
    //     this.attackProcessers.Remove(attackProcesser);
    // }
}

[CreateAssetMenu(fileName = "newHeroCard", menuName = "Card/newHeroCard")]
public class HeroCardAsset : CardAsset
{
    public int hp;
    public HeroCardAsset() : base()
    {
        cardEnum = CardEnum.HeroCard;
    }
    public override CardModel CreateCardModel()
    {
        return new HeroCardModel(this);
    }
}
