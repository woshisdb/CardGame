using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCellItem : CellItem, ISendEvent
{
    public override List<UIItemBinder> GetUI()
    {
        var ret = new List<UIItemBinder>();
        string text = "自习";
        ret.Add(new ButtonBinder(() => { return text; }, () => {
            Hero.AddProperty(HeroProperty.knowledge, 3);
            Debug.Log(text);
        }));
        return ret;
    }
}
