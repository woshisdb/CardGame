using Sirenix.OdinInspector;

public enum TableEffectEnum
{
    AddHpEffectObj,
    Counter,//反击
    Shield,//护盾
    Regeneration,
    Attack,
}

public struct TableEffectDataEvent:IEvent
{
    public TableEffectData tableEffectData;
    public TableEffectDataEvent(TableEffectData tableEffectData)
    {
        this.tableEffectData = tableEffectData;
    }
}

public interface TableEffectData
{
    TableEffectEnum GetEffectEnum();
}

public abstract class TableEffectObj: SerializedScriptableObject
{
    public TableModel TableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }

    public abstract void ShowEffect(TableEffectData effectData);
}