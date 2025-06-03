using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CellItem : MonoBehaviour, IPointerClickHandler, ISendEvent, IUISelector
{
    public HeroCardModel Hero { get { return GameArchitect.Instance.heroCardModel; } }
    public abstract List<UIItemBinder> GetUI();

    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        this.SendEvent<SelectViewEvent>(new SelectViewEvent(this));
        //Debug.Log(1111);
        //this.SendEvent<SelectViewEvent>(new SelectViewEvent());
    }
}
