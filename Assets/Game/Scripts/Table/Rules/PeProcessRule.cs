using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 预处理行为
/// </summary>
public class PeProcessRule
{
    public static void Process(GameRule gameRule)
    {
        var process = gameRule.GameRuleProcessor;
        process.Register<int>(ProcessType.Attack, (card,e) =>
        {
            if (card!=null && card.GetCardType() == CardEnum.HeroCard)
            {
                var hero = card as HeroCardModel;
                var angry=hero.TagInf(AnimalTagEnum.Angry);
                return e+angry;
            }
            else
            {
                return e;
            }
        });
        gameRule.HeroPreActions.Add(e =>
        {
            State.Next(new GetCardFromDeck(1,e,e));
        });
    }
}
