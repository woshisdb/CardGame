using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextSlot : SlotView
{
    public TextMeshPro text;
    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }
    public void SetText(string val)
    {
        text.text = val;
    }
}
