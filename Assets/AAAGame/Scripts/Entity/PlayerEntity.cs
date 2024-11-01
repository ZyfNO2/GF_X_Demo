using System.Collections.Generic;
//using AAAGame.Scripts.Entity;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerEntity : SampleEntity
{
    public virtual bool IsAIPlayer { get => false; }
    private Vector3 joystickForward;
    private float moveSpeed = 10f;
    private float rotationSpeed = 10f;
    CharacterController characterCtrl;
    private Vector3 playerVelocity;
    private float jumpHeight = 3f;
    private bool isGrounded;
    private Vector3 moveStep;
    //private Transform firePoint;
    private float fireInterval = 0.4f;
    private float lastFireTime;
    private bool mCtrlable;
    
    List<int> loadEntityTaskList;
    
    public bool Ctrlable
    {
        get => mCtrlable;
        set
        {
            mCtrlable = value;
            if (!IsAIPlayer) GF.StaticUI.JoystickEnable = mCtrlable;
        }
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        characterCtrl = GetComponent<CharacterController>();
        loadEntityTaskList = new List<int>();
        //firePoint = transform.Find("FirePoint");
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

    private void Move()
    {
        //float movePower = GF.StaticUI.Joystick.GetDistance();
        // joystickForward.Set(GF.StaticUI.Joystick.GetHorizontalAxis(), 0, GF.StaticUI.Joystick.GetVerticalAxis());
        // if (movePower > 0.001f)
        // {
        //     characterCtrl.transform.forward = Vector3.Slerp(characterCtrl.transform.forward, joystickForward, Time.deltaTime * rotationSpeed);
        // }

        // if (isGrounded)
        // {
        //     if (playerVelocity.y < 0) playerVelocity.y = 0;
        //     moveStep = characterCtrl.transform.forward * moveSpeed * movePower;
        // }

        //characterCtrl.Move(moveStep * Time.deltaTime);


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
        // if (isGrounded && (Input.GetMouseButtonDown(0) && !GF.UI.IsPointerOverUIObject(Input.mousePosition) || Input.GetButtonDown("Jump")))
        // {
        //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * Physics.gravity.y);
        // }
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterCtrl.Move(playerVelocity * Time.deltaTime);
    }

    private void Build()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Log.Info("<<<<<<<<<<<<<<<" + "InBuild");
            var buildingEntity = EntityParams.Create(this.transform.position, this.transform.eulerAngles,
                this.transform.localScale );
            
            var mBuildingEntityId = GF.Entity.ShowEntity<BuildingEntity>("BuildingTest", Const.EntityGroup.Buliding, buildingEntity);
            
            loadEntityTaskList.Add(mBuildingEntityId);
        }
        
        
    }
    
    
    
    
}
