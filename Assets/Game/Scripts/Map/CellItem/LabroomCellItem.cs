using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabroomCellItem : CellItem, ISendEvent
{
    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        string text = "实验室";
        ret.Add(new ButtonBinder(() => { return text; }, () => { Debug.Log(text); }));
        return ret;
    }
}
