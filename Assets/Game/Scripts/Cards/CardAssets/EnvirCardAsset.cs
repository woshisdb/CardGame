using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnvirCard", menuName = "SaveData/newEnvirCard")]
public class EnvirCardAsset : CardAsset
{
    public EnvirCardAsset() : base()
    {
        cardEnum = CardEnum.EnvirCard;
    }
}
