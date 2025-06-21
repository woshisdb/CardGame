using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class GameFrameWork : MonoBehaviour,IRegisterEvent
{
    [ShowInInspector,ReadOnly]
    GameArchitect gameArchitect;
    public SaveData saveData;
    public void Start()
    {
        GameArchitect.mInstance = null;
        gameArchitect = GameArchitect.Instance; 
        gameArchitect.worldMapModel = WorldMapModel.Instance;
        gameArchitect.worldMapCol = GameObject.Find("worldMapCol").GetComponent<WorldMapCol>();
        gameArchitect.worldMapCol.BindModel(gameArchitect.worldMapModel);
        gameArchitect.cellList = new List<Cell>();
        foreach (var scenex in gameArchitect.worldMapModel.cells)
        {
            foreach (var cell in scenex)//Ã¿¸öCELL
            {
                gameArchitect.cellList.Add(cell);
            }
        }
        this.Register<SelectViewEvent>(e =>
        {
            GameArchitect.Instance.uiManager.inspector.ShowUI(e.view);
        });
    }
    [Button]
    public void StartGame()
    {
        GameArchitect.Instance.gameDateSystem.Start();
    }
    public void InitFunc(Action func)
    {
        StartCoroutine(DelayCoroutine(0, func));
    }
    private IEnumerator DelayCoroutine(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
    [Button]
    public void UpdateFrameWork()
    {
        Debug.Log(">>>");
        gameArchitect.day++;
    }
}
