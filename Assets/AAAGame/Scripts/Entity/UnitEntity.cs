using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// To Do
/// 读表创建
/// AI      Navi    插件  !!!之后换成ROV
/// 行为树   BehaviorTree  
/// HP      接口   IHitable
/// Atk     接口   IAtkable
/// </summary>


public class UnitEntity : SampleEntity
{
    private Camp camp;
    private int mPlayerId;
    private GameObject mPlayer;
    
    
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
        //mPlayer = (GameObject)GetPlayer();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        var targetPosition = GetPlayerTransform().position;

        m_Agent.destination = targetPosition;


    }


    private Transform GetPlayerTransform()
    {
        return  GF.Entity.GetEntity<PlayerEntity>(LevelPlayer.PlayerId).transform;
    }
    
    
    
}
    