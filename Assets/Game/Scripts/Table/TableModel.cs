using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



public class LinkAction
{
    public ActionTimePointType actionType;
    public Action<TableExeData,Action> done;
    public bool isHero;
    public GameObject icon;
    public int passTime;
    public Type Type;
    public Action<Action> act;

    public LinkAction(Action<TableExeData,Action> done, ActionTimePointType actionType,Type type)
    {
        this.done = done;
        this.actionType = actionType;
        this.Type = type;
    }

    public LinkAction SetAnimalState(bool isHero)
    {
        this.isHero = isHero;
        return this;
    }
    public LinkAction SetPassTime(int passTime)
    {
        this.passTime = passTime;
        if (actionType == ActionTimePointType.After)
        {
            act = e =>
            {
                this.passTime--;
                if (this.passTime <= 0)
                {
                    TableModel.nowTableModel.RemoveAfterActionFromTable(Type,this);
                    if (icon!=null)
                    {
                        GameObject.Destroy(icon);
                    }
                }
                e.Invoke();
            };
        }
        else
        {
            act = e =>
            {
                passTime--;
                if (passTime <= 0)
                {
                    TableModel.nowTableModel.RemoveAfterActionFromTable(Type,this);
                    if (icon!=null)
                    {
                        GameObject.Destroy(icon);
                    }
                }
                e.Invoke();
            };
        }
        return this;
    }

    public LinkAction SetEndCheck(ActionTimePointType actionType)
    {
        if (isHero)
        {
            if (actionType == ActionTimePointType.Bef)
            {
                TableModel.nowTableModel.gameRule.HeroPreActions.Remove(act);
            }
            else
            {
                TableModel.nowTableModel.gameRule.HeroPostActions.Remove(act);
            }
        }
        else
        {
            if (actionType == ActionTimePointType.Bef)
            {
                TableModel.nowTableModel.gameRule.EnemyPreActions.Remove(act);
            }
            else
            {
                TableModel.nowTableModel.gameRule.EnemyPostActions.Remove(act);
            }
        }
        return this;
    }

    public LinkAction SetIconShow(GameObject iconTemplate,OneCardSlotView slot)
    {
        icon = slot.AddEffectIcon(iconTemplate);
        icon = GameObject.Instantiate(iconTemplate);
        // icon.gameObject.transform.parent = slot.contentView ;
        // icon.transform.localScale= Vector3.one;
        return this;
    }
}

public class TableModel:IModel,IRegisterEvent,ISendEvent
{
    public static TableModel nowTableModel
    {
        get
        {
            return GameArchitect.Instance.GetTableModel();
        }
    }

    public List<SlotView> slots;
    public TableView view;
    public TableCircle tableCircle;
    public SlotSel slotSel;
    public GameRule gameRule;
    public Dictionary<Type, List<LinkAction>> actionBeforLinks;
    public Dictionary<Type, List<LinkAction>> actionAfterLinks;

    public LinkAction AddBeforActionToTable<T>(Action<TableExeData,Action> action)
    {
        var linkAction = new LinkAction(action,ActionTimePointType.Bef,typeof(T));
        if (!actionBeforLinks.ContainsKey(typeof(T)))
        {
            actionBeforLinks[typeof(T)]=new List<LinkAction>();
        }
        actionBeforLinks[typeof(T)].Add(linkAction);
        return linkAction;
    }
    public LinkAction AddAfterActionToTable<T>(Action<TableExeData,Action> action)
    {
        var linkAction = new LinkAction(action,ActionTimePointType.After,typeof(T));
        if (!actionAfterLinks.ContainsKey(typeof(T)))
        {
            actionAfterLinks[typeof(T)]=new List<LinkAction>();
        }
        actionAfterLinks[typeof(T)].Add(linkAction);
        return linkAction;
    }
    
    public int GetPower()
    {
        return gameRule.GetPower();
    }

    public void RemoveBeforActionFromTable<T>(LinkAction action)
    {
        actionBeforLinks[typeof(T)].Remove(action);
    }

    public void RemoveAfterActionFromTable<T>(LinkAction action)
    {
        actionAfterLinks[typeof(T)].Remove(action);
    }
    public void RemoveBeforActionFromTable(Type T,LinkAction action)
    {
        actionBeforLinks[T].Remove(action);
    }

    public void RemoveAfterActionFromTable(Type T,LinkAction action)
    {
        actionAfterLinks[T].Remove(action);
    }
    
    public void ExeActionBef(TableExeData data,Action done)
    {
        var seq = new AsyncQueue();
        if (actionBeforLinks.ContainsKey(data.GetType())&& data.needLink)
        {
            foreach (LinkAction action in actionBeforLinks[data.GetType()])
            {
                seq.Add(e=>
                {
                    action.done(data,e);
                });
            }
            seq.Add((e) =>
            {
                done();
                e();
            });
            seq.Run();
        }
        else
        {
            done();
        }
    }
    
    public void ExeActionAfter(TableExeData data,Action done)
    {
        var seq = new AsyncQueue();
        if (actionAfterLinks.ContainsKey(data.GetType())&&data.needLink)
        {
            foreach (LinkAction action in actionAfterLinks[data.GetType()])
            {
                seq.Add(e=>
                {
                    action.done(data,e);
                });
            }
            seq.Add((e) =>
            {
                done();
                e();
            });
            seq.Run();
        }
        else
        {
            done();
        }
    }
    
    public CardManager cardManager { get
        {
            return GameArchitect.Instance.cardManager;
        } }
    public TableCircleEnum circleEnum { get { return tableCircle.circleEnum; } }

    public class SlotSel
    {
        public Func<SlotView, bool> cond;
        public Action<SlotView> succ;
        public SlotSel(Func<SlotView, bool> cond, Action<SlotView> succ)
        {
            this.cond = cond;
            this.succ = succ;
        }
    }
    public void TableEffectEvent(TableEffectDataEvent effectData)
    {
        var effs = view.effects;
        effs[effectData.tableEffectData.GetEffectEnum()].ShowEffect(effectData.tableEffectData);
    }
    public void ChangeEvent(ChangeEvent e)
    {
        tableCircle.SetCircle(e.circleEnum);
        if (e.circleEnum == TableCircleEnum.Pending)
        {
            if(gameRule != null)
            gameRule.Run();
        }
    }
    public virtual void CardEffectEvent(CardEffectEvent e)
    {
        if (e.effect.cardEffect.CanExe(e.effect, this, e.cardModel))
        {
            State.Next(e.effect.cardEffect.Effect(e.effect, this, e.cardModel,null));
        }
    }
    public void SlotEffectEvent(SlotEffectEvent e)
    {
        if (e.effect.CanExe(e.effect.EffectData(), this, e.slotView))
        {
            State.Next(e.effect.Effect(e.effect.EffectData(), this, e.slotView,null));
        }
    }
    public void SelectSlotEvent(SelectSlotEvent e)
    {
        if (TableCircleEnum.SelectSloting == circleEnum)
        {
            if (slotSel.cond(e.slot))
            {
                this.SendEvent<ChangeEvent>(new ChangeEvent(TableCircleEnum.SelectCarding));
                slotSel.succ(e.slot);
                slotSel = null;
            }
        }
    }
    
    
    public TableModel()
    {
        tableCircle = new TableCircle();
        slots = new List<SlotView>();
        gameRule = new GameRule(this);
    }
    public void Init(ICellBindTableModel tableData)
    {
        tableData.TableInit(this);
        tableData.GameRule(this);
        (this.FindSlotByName("cardDeckSlot") as CardDeckSlot).cardDeckModel = GameArchitect.Instance.saveSystem.saveFile.cardDeckModel;
        //Debug.Log(gameRule);
        //Debug.Log(this);
        this.Register<ChangeEvent>(ChangeEvent);
        this.Register<CardEffectEvent>(CardEffectEvent);
        this.Register<SlotEffectEvent>(SlotEffectEvent);
        this.Register<SelectSlotEvent>(SelectSlotEvent);
        this.Register<TableEffectDataEvent>(TableEffectEvent);
        gameRule.Init();
    }
    public void Destory()
    {
        this.Unregister<ChangeEvent>(ChangeEvent);
        this.Unregister<CardEffectEvent>(CardEffectEvent);
        this.Unregister<SelectSlotEvent>(SelectSlotEvent);
        this.Unregister<SlotEffectEvent>(SlotEffectEvent);
        this.Unregister<TableEffectDataEvent>(TableEffectEvent);
    }
    public void StartSelectSlot(Func<SlotView,bool> cond,Action<SlotView> succ)
    {
        slotSel = new SlotSel(cond, succ);
        this.SendEvent(new ChangeEvent(TableCircleEnum.SelectSloting));
    }
    public SlotView FindSlotByTag(SlotTag slotTag,Func<SlotView,bool> func=null)
    {
        for(int i=0;i< slots.Count;i++)
        {
            if(slots[i].ExistTag(slotTag))
            {
                if(func!=null&& func(slots[i])||func==null)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }
    public SlotView FindSlotByName(string name, Func<SlotView, bool> func = null)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name == name)
            {
                if (func != null && func(slots[i]) || func == null)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }
    public List<SlotView> FindSlotByTags(SlotTag slotTag, Func<SlotView, bool> func = null)
    {
        List<SlotView> ret = new List<SlotView>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].ExistTag(slotTag))
            {
                if (func != null && func(slots[i])||func==null)
                {
                    ret.Add(slots[i]);
                }
            }
        }
        return ret;
    }

    [Button]
    public void AddSlot(SlotView slotView)
    {
        var gameObj = GameObject.Instantiate(slotView.gameObject);
        gameObj.transform.SetParent(view.transform);
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.GetComponent<SlotView>().tableView = view;
        slots.Add(slotView);
    }
    public IView CreateView()
    {
        return view;
    }

}
