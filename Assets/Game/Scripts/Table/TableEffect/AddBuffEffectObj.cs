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
}

// [CreateAssetMenu(fileName = "AddBuffEffectObj", menuName = "TableEffectObj/AddBuffEffectObj")]
public class AddBuffEffectObj:TableEffectObj
{
    public GameObject effect;
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
}