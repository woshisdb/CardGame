using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHpEffectObjData: TableEffectData
{

}

[CreateAssetMenu(fileName = "AddHpEffectObj", menuName = "TableEffectObj/AddHpEffectObj")]
public class AddHpEffectObj:TableEffectObj
{
    public GameObject hpEffect;
    public override void ShowEffect(TableEffectData effectData)
    {
        
    }
}