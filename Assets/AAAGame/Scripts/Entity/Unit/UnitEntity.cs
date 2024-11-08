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
/// 设置一个总manager
/// </summary>


public partial class UnitEntity : SampleEntity
{
    private Camp camp;
    private int mUnitId;
    private GameObject mPlayer;
    
    private Transform atkPosition;
    private SphereCollider atkRange;
    private float atkDistance = 2f;
    
    NavMeshAgent m_Agent;
    private int mHp;
    private int mAtk;
    
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
        atkPosition = GameObject.Find("AttackPosition").transform;
        atkRange = atkPosition.GetComponent<SphereCollider>();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);

        atkRange.enabled = false;
        atkPosition.gameObject.SetActive(false);
        
        switch (camp)
        {
            case Camp.Goblins:
                GoblinsOnShow();
                break;
            case Camp.Knights:
                KnightsOnShow();
                break;
        }
        
    }
    
    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        switch (camp)
        {
            case Camp.Goblins:
                GoblinsOnHide();
                break;
            case Camp.Knights:
                KnightsOnHide();
                break;
        }
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

    private PlayerEntity GetPlayer()
    {
        return GF.Entity.GetEntity<PlayerEntity>(LevelInfo.PlayerId);
    }
    
}
    