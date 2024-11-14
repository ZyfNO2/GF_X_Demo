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
    
    List<int> loadEntityTaskList;
    
    RaycastHit m_HitInfo = new RaycastHit();
    
    
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


        Attack();
        
        Move();
        
        Jump();

        Build();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                ray.direction = new Vector3(ray.direction.x, ray.direction.y ,ray.direction.z); // 将射线方向设置为垂直向下
                RaycastHit hitInfo;
                
                LayerMask layerMask = 1 << 6;
                
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity,layerMask))
                {
                    // 使用碰撞点的位置来创建特效实体参数
                    var effectEntityParams = EntityParams.Create(hitInfo.point, this.transform.eulerAngles,
                        this.transform.localScale);
                    Log.Info(hitInfo.point);
            
                    GF.Entity.ShowEffect("Sphere", effectEntityParams, 1f, 0);
                }
                else
                {
                    Log.Info("没有检测到地面");
                }
            }
            
        }
     
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
          
            loadEntityTaskList.Remove(eArgs.Entity.Id);
            
        }
    }
    
    
    
    
    
}
