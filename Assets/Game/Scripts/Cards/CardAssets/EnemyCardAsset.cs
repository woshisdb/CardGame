using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyCard", menuName = "SaveData/newEnemyCard")]
public class EnemyCardAsset : CardAsset
{
    /// <summary>
    /// 生命值
    /// </summary>
    public int hp;
    public EnemyCardAsset():base()
    {
        cardEnum = CardEnum.EnemyCard;
    }
}
