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
    /// -1为永久,其他为时间
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
    public void SetEffectPassTime(int time,LinkAction linkAction, IAddBuffEffectData data)
    {
        var icon = GameObject.Instantiate(effectIcon);
        icon.gameObject.transform.parent = data.getCard().GetSlot().gameObject.transform;
        if(time!=-1)
        if (data.getCard().GetCardType()== CardEnum.HeroCard)
        {
            int passTime = time;
            TableModel.gameRule.HeroPreActions.Add(new GameAction(e =>
            {
                passTime--;
                if (passTime <= 0)
                {
                    TableModel.RemoveAfterActionFromTable<TableChangeHpData>(linkAction);
                    GameObject.Destroy(icon);
                }
            }));
        }
        else
        {
            int passTime = time;
            TableModel.gameRule.EnemyPreActions.Add(new GameAction(e =>
            {
                passTime--;
                if (passTime <= 0)
                {
                    TableModel.RemoveAfterActionFromTable<TableChangeHpData>(linkAction);
                    GameObject.Destroy(icon);
                }
            }));
        }
    }
}