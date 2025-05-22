using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardModel : CardModel,IAnimalCard
{
    public int hp;
    public SlotView slot;
    public HeroCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as HeroCardAsset;
        this.hp = asset.hp;
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
    }

    public int GetHp()
    {
        return this.hp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }

    public void ChangeHp(int hp)
    {
        this.hp = this.hp+hp;
    }
    public SlotView GetSlot()
    {
        return slot;
    }
    public override void OnAddSlot(SlotView slotView)
    {
        slot = slotView;
    }
}

[CreateAssetMenu(fileName = "newHeroCard", menuName = "SaveData/newHeroCard")]
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
