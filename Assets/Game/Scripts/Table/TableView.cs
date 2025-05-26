using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class TableView : SerializedMonoBehaviour,IView,ISendEvent
{
    public Dictionary<TableEffectEnum, TableEffectObj> effects;
    public List<SlotView> slotViews { get { return tableModel.slots; } }
    public CardManager GetCardManager()
    {
        return GameObject.Find("CardManager").GetComponent<CardManager>();
    }
    public TableModel tableModel;
    [ShowInInspector,ReadOnly]
    public TableCircleEnum circleEnum { get {
            if(tableModel == null)
            {
                return TableCircleEnum.Pending;
            }
            return tableModel.tableCircle.circleEnum; 
    } }
    public void BindModel(IModel model)
    {
        tableModel = (TableModel)model;
        tableModel.view = this;
        Refresh();
    }

    public IModel GetModel()
    {
        return tableModel;
    }
    public void ClearSlots()
    {
        for (int i = 0; i < slotViews.Count; i++)
        {
            GameObject.DestroyImmediate(slotViews[i].gameObject);
        }
        slotViews.Clear();
    }
    public void Refresh()
    {
    }
    [Button]
    public void InitTableView()
    {
        tableModel = new TableModel();
        tableModel.view = this;

    }
    /// <summary>
    /// ���볡�����ʼ��
    /// </summary>
    public void StartGame(TableModelData tableData)
    {
        tableModel.Init(tableData);
        this.SendEvent(new ChangeEvent(TableCircleEnum.Pending));
        //AsyncQueue asyncQueue = new AsyncQueue();
        //asyncQueue.Add((done) =>
        //{
        //    GetCardManager().TestCards();
        //    done();
        //});
        //asyncQueue.Add((done) =>
        //{
        //    this.SendEvent(new ChangeEvent(TableCircleEnum.SelectCarding));
        //    done();
        //});
        //asyncQueue.Run();
    }

    public void ShowEffect(TableEffectData effectData)
    {
        
    }
    public Action endTurnAction;
    public void EndTurn()
    {
        if(endTurnAction != null)
            endTurnAction();
    }
    public SlotView GetSlot(string name)
    {
        foreach (var slotView in slotViews)
        {
            if (slotView.name == name)
            { return slotView;}
        }
        return null;
    }
}
