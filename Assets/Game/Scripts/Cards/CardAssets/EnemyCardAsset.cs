using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyCardModel : CardModel,IAnimalCard
{
    public int hp;
    public SlotView slot;

    public EnemyCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as EnemyCardAsset;
        this.hp = asset.hp;
    }

    public void GameTurn(Action done,TableModel tableModel, OneCardSlotView slot)
    {
        var asset = cardAsset as EnemyCardAsset;
        asset.ExeEnemyBehavior(done,this,slot,tableModel);
    }
    public override void Refresh(CardScView cardScView)
    {
        base.Refresh(cardScView);
        cardScView.items["hp"].text = this.hp.ToString();
    }
    public override void OnAddSlot(SlotView slotView)
    {
        slot = slotView;
    }

    public override List<UIItemBinder> GetUI(bool fromScene)
    {
        var ret = new List<UIItemBinder>();
        ret.Add(new KVItemBinder(() =>
        {
            return "Title";
        }, () =>
        {
            return cardAsset.cardName;
        }));
        ret.Add(new KVItemBinder(() =>
        {
            return "Description";
        }, () =>
        {
            return cardAsset.cardDescription;
        }));
        if (!fromScene)
        {
            foreach (var x in cardAsset.cardEffects)
            {
                var eff = x;
                ret.Add(new ButtonBinder(() =>
                {
                    return "use";
                }, () =>
                {
                    this.SendEvent(new CardEffectEvent(eff, this));
                }));
            }
            //foreach (var x in ((EnemyCardAsset)cardAsset).enemySkils)
            //{
            //    var eff = x;
            //    ret.Add(new ButtonBinder(() =>
            //    {
            //        return "www";
            //    }, () =>
            //    {
            //        this.SendEvent(new SlotEffectEvent(eff.slotEffect, slot));
            //    }));
            //}
        }
        return ret;
    }

    public int GetHp()
    {
        return this.hp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
        TryRefresh();
    }

    public void ChangeHp(int hp)
    {
        this.hp = this.hp + hp;
        TryRefresh();
    }
    public void TryRefresh()
    {
        if (slot != null)
        {
            slot.Update();
        }
    }
    public SlotView GetSlot()
    {
        return slot;
    }

    public CardEnum GetCardType()
    {
        return cardAsset.cardEnum;
    }
}

[CreateAssetMenu(fileName = "newEnemyCard", menuName = "Card/newEnemyCard")]
public class EnemyCardAsset : CardAsset
{
    public int hp;
    public List<SlotEffectData> enemySkils;
    public EnemyCardAsset():base()
    {
        cardEnum = CardEnum.EnemyCard;
        enemySkils = new List<SlotEffectData>();
    }
    [Button]
    public void AddEnemySkil(SlotEffect cardEffect)
    {
        enemySkils.Add(cardEffect.EffectData());
    }
    public override CardModel CreateCardModel()
    {
        return new EnemyCardModel(this);
    }
    public void ExeEnemyBehavior(Action done, EnemyCardModel enemyCard, OneCardSlotView slot, TableModel tableModel)
    {
        // 创建随机种子
        System.Random rand = new System.Random();

        // 创建技能索引列表
        List<int> indices = Enumerable.Range(0, enemySkils.Count).ToList();

        // 打乱顺序
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        // 尝试依次执行可用技能
        foreach (int index in indices)
        {
            SlotEffectData randomSkill = enemySkils[index];
            if (randomSkill.slotEffect.CanExe(randomSkill, tableModel, slot))
            {
                var exeData = randomSkill.slotEffect.Effect(randomSkill, tableModel, slot,done);
                State.Next(exeData);
                return;
            }
        }

        done();
    }

}
