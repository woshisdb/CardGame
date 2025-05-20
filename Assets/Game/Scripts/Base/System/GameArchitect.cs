using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameArchitect : Singleton<GameArchitect>
{
    public WorldMapModel worldMapModel;
    public WorldMapCol worldMapCol;
    //    public Market market;
    public GameFrameWork gameFrameWork;
    public UIManager uiManager;
    public int day;
    public List<Cell> cellList;
    public ResConfig resConfig;
    public GameObject tableRoot; 
    public CardManager cardManager;
    protected GameObject tableViewNow;
    //public TableModel GetTableModel()
    //{
    //    return (TableModel)tableViewNow.GetComponent<TableView>().GetModel();
    //}
    public SaveData saveSystem { get { return gameFrameWork.saveData; } }
    //    public TimeSystem timeSystem;
    //    public WorldView worldViewSystem;
    //    public GovernmentObj government { get{ return saveSystem.saveData.Government; } }
    private GameArchitect():base()
    {
        tableRoot = GameObject.Find("TableRoot");
        resConfig = (ResConfig)Resources.Load("newResConfig");
        gameFrameWork = GameObject.Find("GameFrameWork").GetComponent<GameFrameWork>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();
        gameFrameWork.InitFunc(()=> {
        });
    }
    public void SetTable(GameObject gameObject)
    {
        if(tableViewNow != null)
        {
            GameObject.Destroy(tableViewNow);
        }
        gameObject.transform.SetParent(tableRoot.transform);
        gameObject.transform.localPosition = Vector3.zero;
        tableViewNow = gameObject;
    }
    public void DestoryTable()
    {
        if (tableViewNow != null)
        {
            GameObject.Destroy(tableViewNow);
            tableViewNow = null;
        }
    }
    public TableModel GetTableModel()
    {
        return (TableModel)tableViewNow.GetComponent<TableView>().GetModel();
    }
}
