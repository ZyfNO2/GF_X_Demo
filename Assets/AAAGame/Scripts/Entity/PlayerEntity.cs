using UnityEngine;

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
    private Transform firePoint;
    private float fireInterval = 0.4f;
    private float lastFireTime;
    private bool mCtrlable;
    public bool Ctrlable
    {
        get => mCtrlable;
        set
        {
            mCtrlable = value;
            //摇杆先禁用
            //if (!IsAIPlayer) GF.StaticUI.JoystickEnable = mCtrlable;
        }
    }
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        characterCtrl = GetComponent<CharacterController>();
        firePoint = transform.Find("FirePoint");
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (!Ctrlable) return;
        isGrounded = characterCtrl.isGrounded;

        Move();//移动
        //Fire();
        Jump();//跳跃
    }
    private void Fire()
    {
        if (Time.time - lastFireTime > fireInterval)
        {
            lastFireTime = Time.time;
            var fireParms = EntityParams.Create(firePoint.position, firePoint.eulerAngles);
            fireParms.Set<VarFloat>(BulletEntity.LIFE_TIME, 3f);
            GF.Entity.ShowEntity<BulletEntity>("Bullet", Const.EntityGroup.Effect, fireParms);
        }
    }
    
    public float horizontalinput;//水平参数
    public float Verticalinput;//垂直参数
    float speed=10.0f;//声明一个参数，没有规定    
    
    private void Move()
    {
        // float movePower = GF.StaticUI.Joystick.GetDistance();
        // joystickForward.Set(GF.StaticUI.Joystick.GetHorizontalAxis(), 0, GF.StaticUI.Joystick.GetVerticalAxis());
        // if (movePower > 0.001f)
        // {
        //     characterCtrl.transform.forward = Vector3.Slerp(characterCtrl.transform.forward, joystickForward, Time.deltaTime * rotationSpeed);
        // }
        //
        // if (isGrounded)
        // {
        //     if (playerVelocity.y < 0) playerVelocity.y = 0;
        //     moveStep = characterCtrl.transform.forward * moveSpeed * movePower;
        // }
        //
        // characterCtrl.Move(moveStep * Time.deltaTime);
        
        horizontalinput = Input.GetAxis("Horizontal");
        //AD方向控制
        Verticalinput = Input.GetAxis("Vertical");
        //WS方向控制
        
        if (horizontalinput == 0 && Verticalinput == 0)
        {
            return;
        }
        characterCtrl.transform.forward = new Vector3(horizontalinput, 0, Verticalinput);

        moveStep = characterCtrl.transform.forward * moveSpeed;
        
        characterCtrl.Move(moveStep * Time.deltaTime);
        
    }

    private void Jump()
    {
        if (isGrounded && (Input.GetMouseButtonDown(0) && !GF.UI.IsPointerOverUIObject(Input.mousePosition) || Input.GetButtonDown("Jump")))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * Physics.gravity.y);
        }
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterCtrl.Move(playerVelocity * Time.deltaTime);
    }
}
