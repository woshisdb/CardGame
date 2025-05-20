using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public enum UIEnum
{
    cellUI,
    mapUI
}

public class UIManager : MonoBehaviour
{
    public InspectorUI inspector;
    public GameObject kvItemUI;
    public GameObject tableItemUI;
    public GameObject buttonUI;
    public GameObject cellUI;
    public GameObject mapUI;
    public void ToSceneUI(UIEnum uienum)
    {
        if(uienum == UIEnum.cellUI)
        {
            cellUI.SetActive(true);
            mapUI.SetActive(false);
        }
        else
        {
            cellUI.SetActive(false);
            mapUI.SetActive(true);
        }
    }
}
