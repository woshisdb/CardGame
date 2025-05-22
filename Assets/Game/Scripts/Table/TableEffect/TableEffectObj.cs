using Sirenix.OdinInspector;

public enum TableEffectType
{
    
}

public class TableEffectData
{
    
}

public abstract class TableEffectObj:SerializedMonoBehaviour
{
    public abstract void ShowEffect(TableEffectData effectData);
}