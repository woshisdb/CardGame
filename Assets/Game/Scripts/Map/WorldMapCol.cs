using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// �����ͼ��
/// </summary>
public class WorldMapModel:Singleton<WorldMapModel>,IRegisterEvent,ISendEvent,IModel
{
    /// <summary>
    /// һϵ�еĽڵ�
    /// </summary>
    [ShowInInspector]
    public List<List<Cell>> cells;
    /// <summary>
    /// �����ͼģ��
    /// </summary>
    private WorldMapModel()//����ģ��
    {
        var gameArch = GameArchitect.Instance;
        cells = GameArchitect.Instance.gameFrameWork.saveData.saveFile.cells;
    }

    public IView CreateView()
    {
        return null;
    }
}
/// <summary>
/// ģ�Ϳ�����
/// </summary>
public class WorldMapCol : MonoBehaviour,ISendEvent,IRegisterEvent,IView
{
    [ShowInInspector]
    public List<List<CellCol>> cells;
    public WorldMapModel model;
    public void BindModel(IModel model)
    {
        cells=new List<List<CellCol>>();
        this.model = (WorldMapModel)model;
        for(int i = 0; i < this.model.cells.Count; i++)
        {
            cells.Add(new List<CellCol>());
            for(int j = 0; j < this.model.cells[i].Count;j++)
            {
                var cellM = this.model.cells[i][j];
                var cell = (CellCol)cellM.CreateView();
                cell.transform.SetParent(this.transform);
                var ist = cell.GetComponent<CellCol>();
                ist.BindModel(this.model.cells[i][j]);
                cells[i].Add(cell);
            }
        }
        Refresh();
    }

    public IModel GetModel()
    {
        return this.model;
    }

    public void Refresh()
    {
        foreach(var cellx in cells)
        {
            foreach(var cell in cellx)
            {
                cell.Refresh();
            }
        }
    }
}
