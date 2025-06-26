
using System;
using System.Collections.Generic;
using UnityEngine;

public enum WorkType
{
    //********************战斗类
    Warrior,//战士
    Navy,//海军
    AirForce,//空军
    //********************魔法类
    Magicer,
    //********************机械师
    Machiner,
    //********************工匠类
    Blacksmith,//铁匠
    Alchemist,//炼金术士
    Chef,//厨师
    Merchant,//商人
    Architect,//建筑师
    Artisan,//木匠
    Doctor,//医生
    Researcher,//学者
    Teacher,//老师
    //********************政治类
    Lord,//城主,
    Marquis,//侯爵,
    Duke,//公爵,
    King,//国王:最高立法、司法权，制定国家法律,统帅全国军队，决定战争与和平
    //*******************
    //军事部
    //内政部
    //财政部
    //外交部
}

public enum AlchemistWorkType
{
    Apprentice = 1,
    Expert,
    Master,
    Grandmaster,
}

public enum ChefWorkType
{
    Apprentice = 1,
    JuniorChef,
    MasterChef,
    GrandmasterChef,
    Gourmet
}

public enum MerchantWorkType
{
    JuniorMerchant = 1,
    IntermediateMerchant,
    SeniorMerchant,
    Tycoon,
    EmpireBuilder
}

public enum ArchitectWorkType
{
    Apprentice = 1,
    JuniorArchitect,
    SeniorArchitect,
    MasterArchitect,
}

public enum ArtisanWorkType
{
    Apprentice = 1,
    JuniorArtisan,
    MasterArtisan,
    GrandmasterArtisan,
    LegendaryArtisan
}

public enum DoctorWorkType
{
    Intern = 1,
    GeneralDoctor,
    SeniorDoctor,
    MasterDoctor,
    LegendaryDoctor
}

public enum ResearcherWorkType
{
    JuniorResearcher = 1,
    Researcher,
    SeniorResearcher,
    MasterResearcher,
    VisionaryResearcher
}

public enum TeacherWorkType
{
    ApprenticeTeacher = 1,
    JuniorTeacher,
    SeniorTeacher,
    MasterTeacher,
    LegendaryTeacher
}



/// <summary>
/// 幻想风格用：每种职业开放一组专属玩法、任务、成长逻辑
/// </summary>
public abstract class Work
{
    public int Id { get; private set; }
    public string WorkName { get; protected set; }       // 职业名称
    public string Description { get; protected set; }    // 描述
    
    public bool IsUnlocked { get; private set; } = false;
    public Enum WorkSubType { get; private set; }

    protected Work(int id, string name, string description)
    {
        Id = id;
        WorkName = name;
        Description = description;
    }
    public void SetWorkSubType(Enum workSubType)
    {
        WorkSubType = workSubType;
    }
}
