using Sirenix.OdinInspector;

public enum TableEffectType
{
    AddHpEffectObj,
}

public class TableEffectData
{
    
}

public abstract class TableEffectObj: SerializedScriptableObject
{
    public abstract void ShowEffect(TableEffectData effectData);
}