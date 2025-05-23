using Sirenix.OdinInspector;

public enum TableEffectEnum
{
    AddHpEffectObj,
}

public class TableEffectData
{
    
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