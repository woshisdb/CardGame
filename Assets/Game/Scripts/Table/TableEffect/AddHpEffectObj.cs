using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public struct AddHpEffectObjData: TableEffectData
{
    public IAnimalCard card;
    public int hp;
    public Action done;

    public AddHpEffectObjData(IAnimalCard card, int hp, Action done)
    {
        this.card = card;
        this.hp = hp;
        this.done = done;
    }

    public TableEffectEnum GetEffectEnum()
    {
        return TableEffectEnum.AddHpEffectObj;
    }
}

[CreateAssetMenu(fileName = "AddHpEffectObj", menuName = "TableEffectObj/AddHpEffectObj")]
public class AddHpEffectObj:TableEffectObj
{
    public GameObject hpEffect;
    public override void ShowEffect(TableEffectData effectData)
    {
        var data = (AddHpEffectObjData)effectData;
        var slot = data.card.GetSlot().gameObject;
        var hpEff = GameObject.Instantiate(hpEffect);
        hpEff.gameObject.transform.parent = slot.transform;
        hpEff.gameObject.transform.DOLocalMoveX(1, 1).OnComplete(() =>
        {
            GameObject.Destroy(hpEff);
            data.card.ChangeHp(data.hp);
            data.done();
        });
    }
}