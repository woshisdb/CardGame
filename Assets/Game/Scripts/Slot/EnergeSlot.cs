using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnergeSlot : SlotView
{
    public TextMeshPro text;
    public override List<UIItemBinder> GetUI()
    {
        throw new System.NotImplementedException();
    }
    public void SetEnerge(int val)
    {
        text.text = val.ToString();
    }
    public void Update()
    {
        if(tableView!=null&&tableView.tableModel!=null&& tableView.tableModel.gameRule!=null)
        SetEnerge(tableView.tableModel.gameRule.power);
    }
    public void ChangeEnerge(int val)
    {
        if (tableView != null && tableView.tableModel != null && tableView.tableModel.gameRule != null)
        {
            tableView.tableModel.gameRule.ChangePower(val);
        }
    }
}
