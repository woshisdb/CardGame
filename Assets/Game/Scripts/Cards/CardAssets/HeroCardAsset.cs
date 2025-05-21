using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardModel : CardModel
{
    public int hp;

    public HeroCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as HeroCardAsset;
        this.hp = asset.hp;
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
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
