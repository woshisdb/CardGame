using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState:ISendEvent
{
    public GameRule GameRule { get { return GameArchitect.Instance.GetTableModel().gameRule; } }
    public TableModel TableModel { get { return GameArchitect.Instance.GetTableModel(); } }

    public void Run(Action action=null)
    {
        GameRule.RunTemp = action;
    }
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
        done();
    }

    public override void Pre(Action done)
    {
        done();
    }

    public override void Process(Action done)
    {
        this.SendEvent(new ChangeEvent(TableCircleEnum.SelectCarding));
        done();
    }
}

/// <summary>
/// 用户游戏状态
/// </summary>
public class EnemyGameState : GameState
{
    public override bool IsEnd()
    {
        return false ;
    }

    public override GameState Next()
    {
        return null;
    }

    public override void Post(Action done)
    {
        done();
    }

    public override void Pre(Action done)
    {
        done();
    }

    public override void Process(Action done)
    {
        Debug.Log("ssss");
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

public class GameRule:ISendEvent,IRegisterEvent
{
    public TableModel tableModel;
    public GameState gameState;//当前状态
    public Action RunTemp=null;
    public GameRule(TableModel tableModel)
    {
        this.tableModel = tableModel;
    }
    public void Init()
    {
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
                    }
                });
            }
            else
            {
                done();
            }
        });
        async.Run();
    }
    public void Run()
    {
        if(RunTemp==null)
        {
            RunNormal();
        }
        else
        {
            RunTemp();
        }
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
