using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableCircleEnum
{
    Pending,//�ȴ��׶�.�ȴ�����
    SelectSloting,//����ѡ��Slot
    SelectCarding,//����ѡ����
}


/// <summary>
/// Table��ѭ��
/// </summary>
public class TableCircle
{
    public TableCircleEnum circleEnum;
    public void SetCircle(TableCircleEnum tableCircleEnum)
    {
        this.circleEnum = tableCircleEnum;
    }
}
