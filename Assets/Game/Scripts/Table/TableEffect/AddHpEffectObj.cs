using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHpEffectObjData: TableEffectData
{
    public IAnimalCard card;
    public int hp;

    public AddHpEffectObjData(IAnimalCard card, int hp)
    {
        this.card = card;
        this.hp = hp;
    }
}

[CreateAssetMenu(fileName = "AddHpEffectObj", menuName = "TableEffectObj/AddHpEffectObj")]
public class AddHpEffectObj:TableEffectObj
{
    public GameObject hpEffect;
    public override void ShowEffect(TableEffectData effectData)
    {
        var data = effectData as AddHpEffectObjData;
        data.card.GetSlot()
    }
}