//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AddAngryBuffObjData : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public struct AddAngryBuffObjData : TableEffectData, IAddBuffEffectData
{
    public Action done;
    public IAnimalCard card;
    public int angry;
    public AddAngryBuffObjData(Action done, IAnimalCard card, int angry)
    {
        this.done = done;
        this.card = card;
        this.angry = angry;
    }

    public TableEffectEnum GetEffectEnum()
    {
        return TableEffectEnum.Angry;
    }

    public IAnimalCard getCard()
    {
        return card;
    }

    public Action Done()
    {
        return done;
    }

    public int GetAngry()
    {
        return angry;
    }

    public int GetEffectTime()
    {
        return 0;
    }
}

[CreateAssetMenu(fileName = "AddAngryBuffEffectObj", menuName = "TableEffectObj/AddAngryBuffEffectObj")]
public class AddAngryBuffEffectObj : AddBuffEffectObj
{
    public override void AddBuff(IAnimalCard card, IAddBuffEffectData addBuffObjData)
    {
        var data = (AddAngryBuffObjData)addBuffObjData;
        var hero = card as HeroCardModel;
        hero.tags[AnimalTagEnum.Angry] -= data.GetAngry();
        //TableModel.gameRule.GameRuleProcessor.Register(ProcessType.Attack, data.func);
        //var icon = (data.getCard().GetSlot() as OneCardSlotView).AddEffectIcon(effectIcon);
        //int passTime = data.GetEffectTime();
        //Action<Action> gameAction = null;
        //gameAction = e =>
        //{
        //    passTime--;
        //    if (passTime <= 0)
        //    {
        //        TableModel.gameRule.GameRuleProcessor.Remove(ProcessType.Attack, data.func);
        //        GameObject.Destroy(icon);
        //    }
        //};
        //this.AddGameRuleListen(ActionTimePointType.Bef, data.card, data.GetEffectTime(), gameAction);
    }
}