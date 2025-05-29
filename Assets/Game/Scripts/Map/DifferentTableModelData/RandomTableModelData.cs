using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomTableModelData:ICellBindTableModel
{
    public List<EnemyCardAsset> enemys;
    public void TableInit(TableModel tableModel)
    {
        var heroSlot = tableModel.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView;
        heroSlot.AddCard(this.Hero(),()=>{},()=>{});
        int index = UnityEngine.Random.Range(0, enemys.Count);
        var enemy = enemys[index];
        var enemySlot = tableModel.FindSlotByTag(SlotTag.EnemySlot) as OneCardSlotView;
        enemySlot.AddCard(enemy.CreateCardModel(), () => { }, () => { });
    }
    public TableModelDataEnum GetEnum()
    {
        return TableModelDataEnum.RandomTableModelDataEnum;
    }

    public void GameRule(TableModel table)
    {
        table.gameRule = new GameRule(table);
    }
}