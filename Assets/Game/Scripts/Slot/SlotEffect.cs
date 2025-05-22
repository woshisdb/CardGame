using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SlotEffectData
{
    public ISlotEffect slotEffect;
    public SlotEffectData(ISlotEffect slotEffect)
    {
        this.slotEffect = slotEffect;
    }
}

public interface ISlotEffect
{
    bool CanExe(SlotEffectData effectData, TableModel table, SlotView card);
    /// <summary>
    /// ����Ч��
    /// </summary>
    TableExeData Effect(SlotEffectData effectData, TableModel table, SlotView card);

    /// <summary>
    /// Ч������
    /// </summary>
    /// <returns></returns>
    SlotEffectData EffectData();
}


public abstract class SlotEffect : SerializedScriptableObject,ISlotEffect
{
    [ReadOnly]
    public SlotEffectEnum slotEffectEnum;
    public abstract bool CanExe(SlotEffectData effectData, TableModel table, SlotView card);
    public abstract TableExeData Effect(SlotEffectData effectData, TableModel table, SlotView card);

    public abstract SlotEffectData EffectData();
}
