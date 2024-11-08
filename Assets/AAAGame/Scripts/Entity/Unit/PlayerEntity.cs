using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
//using AAAGame.Scripts.Entity;
using UnityEngine;
using UnityGameFramework.Runtime;

public partial class PlayerEntity : SampleEntity
{
    public virtual bool IsAIPlayer { get => false; }

    private float moveSpeed = 10f;
    
    CharacterController characterCtrl;
    private Vector3 playerVelocity;
    
    private bool isGrounded;
    private Vector3 moveStep;
 
    //!!! 先pubilc 测试
    public List<UnitEntity> enemyList;
    
    private int Hp;
    
    
    private bool mCtrlable;
    
    List<int> loadEntityTaskList;

    public Camp camp = Camp.Knights;
    
    
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        characterCtrl = GetComponent<CharacterController>();
        loadEntityTaskList = new List<int>();
        
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        
        
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        //if (!Ctrlable) return;
        isGrounded = characterCtrl.isGrounded;

        Move();
        
        Jump();

        Build();
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GF.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
    }


    private void Move()
    {
            
        float movePower = 1f;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        
        if (isGrounded )
        {
            if (playerVelocity.y < 0) playerVelocity.y = 0;
            if(h==0 && v==0) return;
            characterCtrl.transform.forward = new Vector3(h, 0, v).normalized;
            moveStep = characterCtrl.transform.forward * (moveSpeed * movePower);
            //moveStep = characterCtrl.transform.forward * moveSpeed * movePower;
        }
        
        characterCtrl.Move(moveStep * Time.deltaTime);
        

    }

    private void Jump()
    {
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterCtrl.Move(playerVelocity * Time.deltaTime);
    }

    private void Build()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBuilding();
        }
    }

    private void SpawnBuilding()
    {
        //Log.Info("<<<<<<<<<<<<<<<" + "InBuild");
        var buildingEntityParams = EntityParams.Create(this.transform.position, this.transform.eulerAngles,
            this.transform.localScale );
        
        //!!!硬编码，之后读表改     
        var mBuildingEntityId = GF.Entity.ShowEntity<BuildingEntity>("BuildingTest", Const.EntityGroup.Building, buildingEntityParams);
            
        loadEntityTaskList.Add(mBuildingEntityId);
    }
    
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var eArgs = e as ShowEntitySuccessEventArgs;
        if (loadEntityTaskList.Contains(eArgs.Entity.Id))
        {
            var buildingEntity = GF.Entity.GetEntity<BuildingEntity>(eArgs.Entity.Id);
            buildingEntity.SetCamp(Camp.Knights);
            
            Log.Info("<<<<<<<<<<<<<<<" + buildingEntity.GetCamp());
            loadEntityTaskList.Remove(eArgs.Entity.Id);
            
        }
    }
    
    
    
    
    
}
