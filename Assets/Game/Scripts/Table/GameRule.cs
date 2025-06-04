using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAction
{
    public Action<Action> action;
    public GameAction(Action<Action> action)
    {
        this.action = action;
    }
}
public abstract class GameState:ISendEvent
{
    public GameRule GameRule { get { return GameArchitect.Instance.GetTableModel().gameRule; } }
    public TableModel TableModel { get { return GameArchitect.Instance.GetTableModel(); } }
    
    /// <summary>
    /// 是否初始化
    /// </summary>
    public bool isInit;
    /// <summary>
    /// 前驱处理
    /// </summary>
    public abstract void Pre(Action done);
    public abstract bool IsEnd();
    //处理
    public abstract void Process(Action done);
    //后处理
    public abstract void Post(Action done);
    public abstract GameState Next();
}
/// <summary>
/// 用户游戏状态
/// </summary>
public class UserGameState : GameState
{
    public override bool IsEnd()
    {
        return true;
    }

    public override GameState Next()
    {
        return new EnemyGameState();
    }

    public override void Post(Action done)
    {
        AsyncQueue asyncQueue = new AsyncQueue();
        foreach (GameAction action in TableModel.gameRule.HeroPostActions)
        {
            asyncQueue.Add(e =>
            {
                action.action(e);
            });
        }
        asyncQueue.Add(e =>
        {
            done();
            e();
        });
        asyncQueue.Run();
    }

    public override void Pre(Action done)
    {
        (TableModel.FindSlotByName("gameStage") as TextSlot).SetText("玩家回合");
        AsyncQueue asyncQueue = new AsyncQueue();
        foreach (GameAction action in TableModel.gameRule.HeroPreActions)
        {
            asyncQueue.Add(e =>
            {
                action.action(e);
            });
        }
        asyncQueue.Add(e =>
        {
            done();
            e();
        });
        asyncQueue.Run();
    }

    public override void Process(Action done)
    {
        Debug.Log("UserGameState");
        AsyncQueue async = new AsyncQueue();
        async.Add(e =>
        {
            State.Next(new GetCardFromDeck(1,e,e));
        });
        async.Add(e =>
        {
            TableModel.view.endTurnAction = e;
            //要收到e这个行为
            this.SendEvent(new ChangeEvent(TableCircleEnum.SelectCarding));
        });
        async.Add(e =>
        {
            done();
        });
        async.Run();
    }
}

/// <summary>
/// 用户游戏状态
/// </summary>
public class EnemyGameState : GameState
{
    public override bool IsEnd()
    {
        return true;
    }

    public override GameState Next()
    {
        return new UserGameState();
    }

    public override void Post(Action done)
    {
        AsyncQueue asyncQueue = new AsyncQueue();
        foreach (GameAction action in TableModel.gameRule.EnemyPostActions)
        {
            asyncQueue.Add(e =>
            {
                action.action(e);
            });
        }
        asyncQueue.Add(e =>
        {
            done();
            e();
        });
        asyncQueue.Run();
    }

    public override void Pre(Action done)
    {
        (TableModel.FindSlotByName("gameStage") as TextSlot).SetText("敌方回合");
        AsyncQueue asyncQueue = new AsyncQueue();
        foreach (GameAction action in TableModel.gameRule.EnemyPreActions)
        {
            asyncQueue.Add(e =>
            {
                action.action(e);
            });
        }
        asyncQueue.Add(e =>
        {
            done();
            e();
        });
        asyncQueue.Run();
    }

    public override void Process(Action done)
    {
        Debug.Log("EnemyGameState");
        var hero = TableModel.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView;
        if(hero.IsEmpty())
        {
            done();
        }
        var enemys = TableModel.FindSlotByTags(SlotTag.EnemySlot);
        AsyncQueue asyncQueue = new AsyncQueue();
        for(int i =0;i<enemys.Count;i++)
        {
            if(enemys[i] is OneCardSlotView)
            {
                var enemySlot = enemys[i] as OneCardSlotView;
                if(!enemySlot.IsEmpty())
                {
                    var enemyCard = enemySlot.GetCardModel() as EnemyCardModel;
                    asyncQueue.Add(e =>
                    {
                        enemyCard.GameTurn(e,TableModel,enemySlot);
                    });
                }    
            }
        }
        asyncQueue.Add(e =>
        {
            done();
            e();
        });
        asyncQueue.Run();
    }
}

public enum ActionTimePointType
{
    After,
    Bef,
}



public class GameRule:ISendEvent,IRegisterEvent
{
    public List<GameAction> HeroPreActions = new List<GameAction>();
    public List<GameAction> EnemyPreActions = new List<GameAction>();
    public List<GameAction> HeroPostActions = new List<GameAction>();
    public List<GameAction> EnemyPostActions = new List<GameAction>();
    public int power=10;//能量
    public TableModel tableModel;
    public GameState gameState;//当前状态

    public void ChangePower(int val)
    {
        power += val;
        var powerSlot = tableModel.FindSlotByName("power") as TextSlot;
        powerSlot.SetText(power.ToString());
    }
    public GameRule(TableModel tableModel)
    {
        this.tableModel = tableModel;
    }
    public void Init()
    {
        var powerSlot = tableModel.FindSlotByName("power") as TextSlot;
        powerSlot.SetText(power.ToString());
        AsyncQueue asyncQueue = new AsyncQueue();
        asyncQueue.Add((done) =>
        {
            GameArchitect.Instance.cardManager.ClearCard();
            GameArchitect.Instance.cardManager.TestCards();
            done();
        });
        gameState = new UserGameState();
        asyncQueue.Run();
    }
    public void RunNormal()
    {
        if(this != GameArchitect.Instance.GetTableModel().gameRule)
        {
            return;
        }
        AsyncQueue async = new AsyncQueue();
        if (!gameState.isInit)
        {
            async.Add(done =>
            {
                gameState.isInit = true;
                gameState.Pre(done);
            });
        }
        async.Add(done =>
        {
            gameState.Process(done);
        });
        async.Add(done =>
        {
            if (gameState.IsEnd())
            {
                gameState.Post(() => {
                    var next = gameState.Next();
                    gameState = next;
                    if (next == null)//游戏结束
                    {
                        EndGame(done);
                    }
                    else
                    {
                        done();
                        Run();
                    }
                });
            }
            else
            {
                Run();
                done();
            }
        });
        async.Run();
    }
    public void Run()
    {
        RunNormal();
    }

    public int GetPower()
    {
        return power;
    }
    public void EndGame(Action done)
    {
        GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.mapUI);
        Camera.main.transform.position = new Vector3(0, 10, 0);
        tableModel.Destory();
        GameArchitect.Instance.DestoryTable();
        done();
    }
}
