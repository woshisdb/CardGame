using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookStoreCellItem : CellItem, ISendEvent
{
    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        string text = "读书";
        ret.Add(new ButtonBinder(() => { return text; }, () => {
            Hero.AddProperty(AnimalProperty.knowledge,3);
            Debug.Log(text);
        }));
        return ret;
    }
}
