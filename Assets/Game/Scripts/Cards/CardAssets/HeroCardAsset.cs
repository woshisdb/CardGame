using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newHeroCard", menuName = "SaveData/newHeroCard")]
public class HeroCardAsset : CardAsset
{
    public int hp;
    public HeroCardAsset() : base()
    {
        cardEnum = CardEnum.HeroCard;
    }
}
