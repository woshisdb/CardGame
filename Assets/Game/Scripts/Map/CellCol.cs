using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// ������
/// </summary>
public class Cell : IUISelector, ISendEvent, IModel
{
    public List<CellItem> CellItems;
    public ICellBindTableModel tableData;

    public static Dictionary<TableModelDataEnum, Func<ICellBindTableModel>> tableModelDataModelDic =
        new Dictionary<TableModelDataEnum, Func<ICellBindTableModel>>()
        {
            { TableModelDataEnum.NormalTableModelDataEnum , () => { return new TableModelData();}},
            {TableModelDataEnum.RandomTableModelDataEnum, () => { return new RandomTableModelData();}}
        };
    /// <summary>
    /// λ��
    /// </summary>
    public Vector2Int pos;

    public Cell()
    {
    }
    [Button]
    public void CreateDataByEnum(TableModelDataEnum type)
    {
        tableData = tableModelDataModelDic[type]();
    }
    public IView CreateView()
    {
        GameObject inst = GameArchitect.Instance.resConfig.cell;
        var cell = GameObject.Instantiate(inst);
        var ist = cell.GetComponent<CellCol>();
        return ist;
    }

    public List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        ret.Add(new KVItemBinder(() =>
        {
            return "Position";
        },
        () =>
        {
            return "(" + pos.x + "," + pos.y + ")";
        }));
        ret.Add(new ButtonBinder(() =>
        {
            return "Enter";
        },
        () =>
        {
            ToCellMap();
        }));
        return ret;
    }
    public virtual void ToCellMap()
    {
        var table = GameArchitect.Instance.resConfig.FindTableObject(TableEnum.CombatTable);
        var tableV = GameObject.Instantiate(table);
        GameArchitect.Instance.SetTable(tableV);
        GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.cellUI);
        Camera.main.transform.position = new Vector3(477, 190, 11);
        GameArchitect.Instance.GetTableModel().view.StartGame(tableData);
    }

    public virtual void OnBind(CellCol cellView)
    {
        
    }
}


public class CellCol : MonoBehaviour,IRegisterEvent,IView
{
    [ShowInInspector]
    public Cell cell;

    public void BindModel(IModel model)
    {
        cell = (Cell)model;
        transform.localPosition = new Vector3(cell.pos.x * 10, 0, cell.pos.y * 10);
        cell.OnBind(this);
        Refresh();
    }

    public IModel GetModel()
    {
        return cell;
    }

    public void Refresh()
    {
    }
}
