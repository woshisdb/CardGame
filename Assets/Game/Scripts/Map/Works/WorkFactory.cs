using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkFactory
{
    public static Work CreateWork(WorkType workType, Enum workSubType)
    {
        Work work;
        // 判断大类别 WorkType 并根据小类别创建对应的 Work 类实例
        switch (workType)
        {
            case WorkType.Alchemist:
                work = CreateAlchemistWork((AlchemistWorkType)workSubType);
                break;
            case WorkType.Chef:
                work = CreateChefWork((ChefWorkType)workSubType);
                break;
            case WorkType.Merchant:
                work = CreateMerchantWork((MerchantWorkType)workSubType);
                break;
            case WorkType.Architect:
                work = CreateArchitectWork((ArchitectWorkType)workSubType);
                break;
            case WorkType.Artisan:
                work = CreateArtisanWork((ArtisanWorkType)workSubType);
                break;
            case WorkType.Doctor:
                work = CreateDoctorWork((DoctorWorkType)workSubType);
                break;
            case WorkType.Researcher:
                work = CreateResearcherWork((ResearcherWorkType)workSubType);
                break;
            case WorkType.Teacher:
                work = CreateTeacherWork((TeacherWorkType)workSubType);
                break;
            default:
                throw new ArgumentException("Unknown WorkType");
        }
        work.SetWorkSubType(workSubType);
        return work;
    }

    // 为不同职业创建具体的 Work 类实例
    private static Work CreateAlchemistWork(AlchemistWorkType workSubType)
    {
        switch (workSubType)
        {
            case AlchemistWorkType.Apprentice: return new AlchemistWork();
            case AlchemistWorkType.Expert: return new Alchemist2Work();
            case AlchemistWorkType.Master: return new AlchemistMasterWork();
            case AlchemistWorkType.Grandmaster: return new AlchemistGrandmasterWork();
            default: throw new ArgumentException("Unknown AlchemistWorkType");
        }
    }

    private static Work CreateChefWork(ChefWorkType workSubType)
    {
        switch (workSubType)
        {
            case ChefWorkType.Apprentice: return new ChefWork();
            case ChefWorkType.JuniorChef: return new Chef2Work();
            case ChefWorkType.MasterChef: return new ChefMasterWork();
            case ChefWorkType.GrandmasterChef: return new ChefGrandmasterWork();
            case ChefWorkType.Gourmet: return new ChefGourmetWork();
            default: throw new ArgumentException("Unknown ChefWorkType");
        }
    }

    private static Work CreateMerchantWork(MerchantWorkType workSubType)
    {
        switch (workSubType)
        {
            case MerchantWorkType.JuniorMerchant: return new MerchantWork();
            case MerchantWorkType.IntermediateMerchant: return new Merchant2Work();
            case MerchantWorkType.SeniorMerchant: return new MerchantMasterWork();
            case MerchantWorkType.Tycoon: return new MerchantGrandmasterWork();
            case MerchantWorkType.EmpireBuilder: return new MerchantTycoonWork();
            default: throw new ArgumentException("Unknown MerchantWorkType");
        }
    }

    private static Work CreateArchitectWork(ArchitectWorkType workSubType)
    {
        switch (workSubType)
        {
            case ArchitectWorkType.Apprentice: return new ArchitectWork();
            case ArchitectWorkType.JuniorArchitect: return new Architect2Work();
            case ArchitectWorkType.SeniorArchitect: return new ArchitectMasterWork();
            case ArchitectWorkType.MasterArchitect: return new ArchitectGrandmasterWork();
            default: throw new ArgumentException("Unknown ArchitectWorkType");
        }
    }

    private static Work CreateArtisanWork(ArtisanWorkType workSubType)
    {
        switch (workSubType)
        {
            case ArtisanWorkType.Apprentice: return new ArtisanWork();
            case ArtisanWorkType.JuniorArtisan: return new Artisan2Work();
            case ArtisanWorkType.MasterArtisan: return new ArtisanMasterWork();
            case ArtisanWorkType.GrandmasterArtisan: return new ArtisanGrandmasterWork();
            case ArtisanWorkType.LegendaryArtisan: return new ArtisanLegendWork();
            default: throw new ArgumentException("Unknown ArtisanWorkType");
        }
    }

    private static Work CreateDoctorWork(DoctorWorkType workSubType)
    {
        switch (workSubType)
        {
            case DoctorWorkType.Intern: return new DoctorWork();
            case DoctorWorkType.GeneralDoctor: return new Doctor2Work();
            case DoctorWorkType.SeniorDoctor: return new DoctorMasterWork();
            case DoctorWorkType.MasterDoctor: return new DoctorGrandmasterWork();
            case DoctorWorkType.LegendaryDoctor: return new DoctorLegendWork();
            default: throw new ArgumentException("Unknown DoctorWorkType");
        }
    }

    private static Work CreateResearcherWork(ResearcherWorkType workSubType)
    {
        switch (workSubType)
        {
            case ResearcherWorkType.JuniorResearcher: return new ResearcherWork();
            case ResearcherWorkType.Researcher: return new Researcher2Work();
            case ResearcherWorkType.SeniorResearcher: return new ResearcherMasterWork();
            case ResearcherWorkType.MasterResearcher: return new ResearcherGrandmasterWork();
            case ResearcherWorkType.VisionaryResearcher: return new ResearcherVisionaryWork();
            default: throw new ArgumentException("Unknown ResearcherWorkType");
        }
    }

    private static Work CreateTeacherWork(TeacherWorkType workSubType)
    {
        switch (workSubType)
        {
            case TeacherWorkType.ApprenticeTeacher: return new TeacherWork();
            case TeacherWorkType.JuniorTeacher: return new Teacher2Work();
            case TeacherWorkType.SeniorTeacher: return new TeacherMasterWork();
            case TeacherWorkType.MasterTeacher: return new TeacherGrandmasterWork();
            case TeacherWorkType.LegendaryTeacher: return new TeacherLegendWork();
            default: throw new ArgumentException("Unknown TeacherWorkType");
        }
    }
}

