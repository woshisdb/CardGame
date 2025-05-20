using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyCard", menuName = "SaveData/newEnemyCard")]
public class EnemyCardAsset : CardAsset
{
    public int hp;
    public List<CardEffectData> enemySkils;
    public EnemyCardAsset():base()
    {
        cardEnum = CardEnum.EnemyCard;
        enemySkils = new List<CardEffectData>();
    }
    [Button]
    public void AddEnemySkil(CardEffect cardEffect)
    {
        enemySkils.Add(cardEffect.EffectData());
    }
}
