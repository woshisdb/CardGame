using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyCardModel : CardModel
{
    public int hp;
    public SlotView slot;

    public EnemyCardModel(CardAsset cardAsset) : base(cardAsset)
    {
        var asset = cardAsset as EnemyCardAsset;
        this.hp = asset.hp;
    }

    public void GameTurn(Action done)
    {
        done();
    }

    public override void OnAddSlot(SlotView slotView)
    {
        slot = slotView;
    }

    public void AddHp(int hp)
    {
        this.hp += hp;
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
                    this.SendEvent(new CardEffectEvent(eff.cardEffect, this));
                }));
            }
            foreach (var x in ((EnemyCardAsset)cardAsset).enemySkils)
            {
                var eff = x;
                ret.Add(new ButtonBinder(() =>
                {
                    return "www";
                }, () =>
                {
                    this.SendEvent(new SlotEffectEvent(eff.slotEffect, slot));
                }));
            }
        }
        return ret;
    }
}

[CreateAssetMenu(fileName = "newEnemyCard", menuName = "SaveData/newEnemyCard")]
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
}
