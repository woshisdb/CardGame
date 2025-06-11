// RelationshipManager.cs
using System.Collections.Generic;

// NpcRelationshipType.cs
[System.Flags]
/// <summary>
/// 表示 NPC 与 NPC 之间可能存在的关系类型。
/// 支持按位组合（Flags），一个关系可以同时拥有多个类型，例如既是朋友又是盟友。
/// </summary>
public enum NpcRelationshipType
{
    /// <summary>
    /// 无关系。默认值。
    /// </summary>
    None = 0,

    /// <summary>
    /// 朋友关系，代表相互友好、互相信赖。
    /// </summary>
    Friend = 1 << 0,  // 1

    /// <summary>
    /// 敌人关系，存在仇恨或敌对立场。
    /// </summary>
    Enemy = 1 << 1,   // 2

    /// <summary>
    /// 同盟关系，共同目标或结成联盟，例如势力合作。
    /// </summary>
    Ally = 1 << 2,    // 4

    /// <summary>
    /// 主人关系，被 To 方视为其上级或主君。
    /// </summary>
    Master = 1 << 3,  // 8

    /// <summary>
    /// 仆人关系，From 方为 To 方效命、服务。
    /// </summary>
    Servant = 1 << 4, // 16

    /// <summary>
    /// 兄弟姐妹关系，表示为同一父母所生。
    /// </summary>
    Sibling = 1 << 5, // 32

    /// <summary>
    /// 父母关系。From 是 To 的父亲或母亲。
    /// </summary>
    Parent = 1 << 6,  // 64

    /// <summary>
    /// 配偶关系，如夫妻、结发妻子、正室等。
    /// </summary>
    Spouse = 1 << 7,  // 128

    /// <summary>
    /// 竞争对手关系，存在某种较劲或争夺，如官职、势力地位。
    /// </summary>
    Rival = 1 << 8    // 256
}

public class RelationshipManager
{
    private Dictionary<(INpc, INpc), NpcRelationship> relationships = new Dictionary<(INpc, INpc), NpcRelationship>();

    public NpcRelationship GetRelationship(INpc from, INpc to)
    {
        var key = (from, to);
        return relationships.ContainsKey(key) ? relationships[key] : null;
    }

    public void SetRelationship(INpc from, INpc to, NpcRelationshipType type, int intimacy = 0)
    {
        var key = (from, to);
        if (!relationships.ContainsKey(key))
        {
            relationships[key] = new NpcRelationship(from, to, type, intimacy);
        }
        else
        {
            relationships[key].AddRelationshipType(type);
            relationships[key].ChangeIntimacy(intimacy);
        }
    }

    public void ChangeIntimacy(INpc from, INpc to, int amount)
    {
        var rel = GetRelationship(from, to);
        if (rel != null)
            rel.ChangeIntimacy(amount);
    }

    public bool AreFriends(INpc a, INpc b)
    {
        var rel = GetRelationship(a, b);
        return rel != null && rel.HasRelationshipType(NpcRelationshipType.Friend);
    }

    public bool AreEnemies(INpc a, INpc b)
    {
        var rel = GetRelationship(a, b);
        return rel != null && rel.HasRelationshipType(NpcRelationshipType.Enemy);
    }
}
