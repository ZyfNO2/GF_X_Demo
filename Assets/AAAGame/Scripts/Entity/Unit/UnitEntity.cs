using UnityEngine;
using UnityEngine.AI;
using UnityGameFramework.Runtime;

/// <summary>
/// To Do
/// 读表创建
/// AI      Navi    插件  !!!之后换成ROV
/// 行为树   BehaviorTree
/// 状态机？
/// HP      接口   IHitable
/// Atk     接口   IAtkable
/// 设置一个总manager
/// </summary>


public class UnitEntity : SampleEntity
{
    
    private GameObject mPlayer;
    
    NavMeshAgent m_Agent;
    private int mHp;
    private int mAtk;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_Agent = GetComponent<NavMeshAgent>();
   
        
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        LevelManager.Instance.enemyList.Add(this.Id);
        Log.Info(LevelManager.Instance.enemyList.Count);
    }
    
    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);

    }

    

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        
        Move();

        Attack();
        
        
    }

    


    // ReSharper disable Unity.PerformanceAnalysis
    private void Attack()
    {

    }

    private void Move()
    {
        var gameProcedure = GF.Procedure.CurrentProcedure as MyGameProcedure;

        var PlayerId = LevelManager.Instance.playerId;
        
        var targetPosition = GF.Entity.GetEntity<PlayerEntity>(PlayerId).transform.position;

        m_Agent.destination = targetPosition;
    }
    
   
    
}
    