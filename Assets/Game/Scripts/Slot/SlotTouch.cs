using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotTouch : MonoBehaviour,ISendEvent, IPointerClickHandler
{
    public SlotView slotView;
    public TableView tableView { get { return slotView.tableView; } }
    public void OnPointerClick(PointerEventData eventData)
    {
        slotView.OnTouch(eventData);
        //if (slotView.tableView.tableModel!= null && slotView.tableView.tableModel.circleEnum == TableCircleEnum.SelectSloting)
        //{
        //    this.SendEvent<SelectSlotEvent>(new SelectSlotEvent(slotView));
        //}
        //else
        //{
        //    this.SendEvent<SelectViewEvent>(new SelectViewEvent(slotView));
        //}
    }
}
