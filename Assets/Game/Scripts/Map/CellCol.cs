using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


/// <summary>
/// Õ¯∏Ò¿‡
/// </summary>
public class Cell : IUISelector, ISendEvent, IModel
{
    /// <summary>
    /// Œª÷√
    /// </summary>
    public Vector2Int pos;

    public Cell()
    {
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
    public void ToCellMap()
    {
        var table = GameArchitect.Instance.resConfig.FindTableObject(TableEnum.CombatTable);
        var tableV = GameObject.Instantiate(table);
        GameArchitect.Instance.SetTable(tableV);
        GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.cellUI);
        Camera.main.transform.position = new Vector3(477, 190, 11);
        //Debug.Log("gbbbofjff");
        GameArchitect.Instance.GetTableModel().view.StartGame();
    }
}


public class CellCol : MonoBehaviour,IRegisterEvent,IView
{
    [ShowInInspector]
    public Cell cell;

    public void BindModel(IModel model)
    {
        cell = (Cell)model;
        transform.position = new Vector3(cell.pos.x * 10, 0, cell.pos.y * 10);
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
