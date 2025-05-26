using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardModel : CardModel,IAnimalCard
{
    public int knowledge;
    public int brave;
    public int charm;
    public int agility;
    public int strength;
    //////////////////////////////
    public int hp;
    public SlotView slot;
    public HeroCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as HeroCardAsset;
        this.hp = asset.hp;
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
