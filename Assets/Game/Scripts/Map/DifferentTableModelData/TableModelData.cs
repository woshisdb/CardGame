using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public static class CellBindTableModelExtensions
{
    public static HeroCardModel Hero(this ICellBindTableModel obj)
    {
        return GameArchitect.Instance.heroCardModel;
    }
}

public interface ICellBindTableModel
{
    void TableInit(TableModel tableModel);
    TableModelDataEnum GetEnum();

    void GameRule(TableModel table);
}

public class TableModelData:ICellBindTableModel
{
    public EnemyCardModel enemy;
    [Button]
    public void AddCard(CardAsset asset,CardEnum cardEnum)
    {
        if (cardEnum == CardEnum.EnemyCard)
        {
            enemy = asset.CreateCardModel() as EnemyCardModel;
        }
    }
    public void TableInit(TableModel tableModel)
    {
        var heroSlot = tableModel.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView;
        heroSlot.AddCard(this.Hero(),()=>{},()=>{});
        var enemySlot = tableModel.FindSlotByTag(SlotTag.EnemySlot) as OneCardSlotView;
        enemySlot.AddCard(enemy,()=>{},()=>{});
    }
    
    
    public TableModelDataEnum GetEnum()
    {
        return TableModelDataEnum.NormalTableModelDataEnum;
    }

    public void GameRule(TableModel table)
    {
        table.gameRule = new GameRule(table);
    }
}