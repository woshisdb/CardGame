using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public struct RefreshViewEvent : IEvent
{
}


public class InspectorUI : MonoBehaviour,IRegisterEvent
{
    public IUISelector nowObj;
    public ListUI listUI;
    public void Start()
    {
        this.Register<RefreshViewEvent>(e =>
        {
            ShowUI(nowObj);
        });
    }
    public void ShowUI(IUISelector obj)
    {
        nowObj=obj;
        listUI.ClearUis();
        if(obj==null)
        {
            return;
        }
        var uis=obj.GetUI();
        foreach (var item in uis)
        {
            GenItems(item, listUI);
        }
    }
    public static void GenItems(UIItemBinder item, ListUI listUI)
    {
        if (item.GetType() == typeof(KVItemBinder))
        {
            GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.kvItemUI);
            var comp = go.GetComponent<KVItemUI>();
            comp.BindObj(item);
            listUI.Add(go);
        }
        else if (item.GetType() == typeof(ButtonBinder))
        {
            GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.buttonUI);
            var comp = go.GetComponent<ButtonUI>();
            comp.BindObj(item);
            listUI.Add(go);
        }
        else
        {
            GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.tableItemUI);
            var comp = go.GetComponent<TableItemUI>();
            comp.BindObj((TableItemBinder)item);
            listUI.Add(go);
        }
    }
}