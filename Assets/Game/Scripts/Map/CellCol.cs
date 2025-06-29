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
    public List<INpc> npcs;
    public ICellBindTableModel tableData;
    /// <summary>
    /// 市场
    /// </summary>
    public Market market;

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
    [Button]
    public void AddNpc()
    {
        var obj = new Npc();
        npcs.Add(obj);
        obj.cell = this;
        obj.shop.cell = this;
    }
    public INpc FindNpcByName(string name)
    {
        return npcs.Find(e => { return e.Name == name; });
    }
}


public class CellCol : MonoBehaviour,IRegisterEvent,IView
{
    [ShowInInspector]
    public Cell cell;
    public void BindModel(IModel model)
    {
        cell = (Cell)model;
        cell.CellItems = new List<CellItem>(GetComponentsInChildren<CellItem>());
        transform.localPosition = new Vector3(cell.pos.x * 10, 0, cell.pos.y * 10);
        cell.OnBind(this);
        var bookStore = (BookStoreCellItem)(cell.CellItems.Find(e =>
        {
            return e is BookStoreCellItem;
        }));
        bookStore.owner = cell.FindNpcByName("bookOwner");
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
