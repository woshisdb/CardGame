using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPlant : MonoBehaviour, IPointerClickHandler,ISendEvent
{
    public CellCol cellCol;
    //public SceneView SceneView;
    public void OnPointerClick(PointerEventData eventData)
    {
        this.SendEvent<SelectViewEvent>(new SelectViewEvent(cellCol.cell));
    }
}
