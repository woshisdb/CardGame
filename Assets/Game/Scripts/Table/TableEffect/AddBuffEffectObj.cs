using System;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public interface IAddBuffEffectData
{
    IAnimalCard getCard();
    Action Done();
    /// <summary>
    /// -1Ϊ����,����Ϊʱ��
    /// </summary>
    /// <returns></returns>
    int GetEffectTime();
}

// [CreateAssetMenu(fileName = "AddBuffEffectObj", menuName = "TableEffectObj/AddBuffEffectObj")]
public class AddBuffEffectObj:TableEffectObj
{
    public GameObject effect;
    public GameObject effectIcon;
    public override void ShowEffect(TableEffectData effectData)
    {
        var data = effectData as IAddBuffEffectData;
        var slot = data.getCard().GetSlot().gameObject;
        var eff = GameObject.Instantiate(effect);
        eff.gameObject.transform.parent = slot.transform;
        eff.transform.transform.localPosition = Vector3.zero;
        eff.gameObject.transform.DOLocalMoveX(1, 1).OnComplete(() =>
        {
            GameObject.Destroy(eff);
            AddBuff(data.getCard(),data);
            data.Done().Invoke();
        });
    }

    public virtual void AddBuff(IAnimalCard card,IAddBuffEffectData addBuffObjData)
    {
        
    }
    // public void SetEffectPassTime<T>(ActionTimePointType gameAction, int time,LinkAction linkAction, IAddBuffEffectData data)
    // {
    //     var icon = GameObject.Instantiate(effectIcon);
    //     icon.gameObject.transform.parent = (data.getCard().GetSlot() as OneCardSlotView).contentView ;
    //     icon.transform.localScale= Vector3.one;
    //     int passTime = time;
    //     GameAction act = null;
    //     if (linkAction.actionType == ActionTimePointType.After)
    //     {
    //         act = new GameAction(e =>
    //         {
    //             passTime--;
    //             if (passTime <= 0)
    //             {
    //                 TableModel.RemoveAfterActionFromTable<T>(linkAction);
    //                 GameObject.Destroy(icon);
    //             }
    //         });
    //     }
    //     else
    //     {
    //         act = new GameAction(e =>
    //         {
    //             passTime--;
    //             if (passTime <= 0)
    //             {
    //                 TableModel.RemoveBeforActionFromTable<T>(linkAction);
    //                 GameObject.Destroy(icon);
    //             }
    //         });
    //     }
    //
    //     if (time != -1)
    //     {
    //         AddGameRuleListen(gameAction,data.getCard(),time,act);
    //     }
    // }
    public void AddGameRuleListen(ActionTimePointType gameActionType, IAnimalCard card,int time,Action<Action> act)
    {
        if (time != -1)
        {
            var passTime = time;
            var action = act;
            act = e =>
            {
                action(e);
                passTime--;
                if(passTime <= 0)
                {
                    if (card.GetCardType() == CardEnum.HeroCard)
                    {
                        if (gameActionType == ActionTimePointType.Bef)
                        {
                            TableModel.gameRule.HeroPreActions.Remove(act);
                        }
                        else
                        {
                            TableModel.gameRule.HeroPostActions.Remove(act);
                        }
                    }
                    else
                    {
                        if (gameActionType == ActionTimePointType.Bef)
                        {
                            TableModel.gameRule.EnemyPreActions.Remove(act);
                        }
                        else
                        {
                            TableModel.gameRule.EnemyPostActions.Remove(act);
                        }
                    }
                }
            };
            if (card.GetCardType() == CardEnum.HeroCard)
            {
                if (gameActionType == ActionTimePointType.Bef)
                {
                    TableModel.gameRule.HeroPreActions.Add(act);
                }
                else
                {
                    TableModel.gameRule.HeroPostActions.Add(act);
                }
            }
            else
            {
                if (gameActionType == ActionTimePointType.Bef)
                {
                    TableModel.gameRule.EnemyPreActions.Add(act);
                }
                else
                {
                    TableModel.gameRule.EnemyPostActions.Add(act);
                }
            }
        }
    }
}

