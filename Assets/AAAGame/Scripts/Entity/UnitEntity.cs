using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
/// </summary>


public partial class UnitEntity : SampleEntity
{
    private Camp camp;
    private int mUnitId;
    private GameObject mPlayer;
    private SphereCollider attackRange;
    private float atkDistance = 2f;
    
    
    NavMeshAgent m_Agent;
    
    
    
    public void SetCamp(Camp camp)
    {
        this.camp = camp;
    }

    public Camp GetCamp()
    {
        return this.camp;
    }


    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        m_Agent = GetComponent<NavMeshAgent>();
        attackRange = GameObject.Find("AttackRange").GetComponent<SphereCollider>();
        
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        
        Move();

        Attack();
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
    }


    private void Attack()
    {
        switch (camp)
        {
            case Camp.Goblins:
                GoblinsAttack();
                break;
            case Camp.Knights:
                KnightsAttack();
                break;
        }
    }

    private void Move()
    {
        var targetPosition = GetPlayerTransform().position;

        m_Agent.destination = targetPosition;
    }


    private Transform GetPlayerTransform()
    {
        return  GF.Entity.GetEntity<PlayerEntity>(LevelInfo.PlayerId).transform;
    }
    
    
    
}
    