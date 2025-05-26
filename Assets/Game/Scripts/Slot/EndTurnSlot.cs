using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurnSlot : SlotView
{

    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTouch(PointerEventData eventData)
    {
        tableView.EndTurn();
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("TOUCH");
    //    view.EndTurn();
    //}
}
