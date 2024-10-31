using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
public class MyMenuProcedure : ProcedureBase
{
    private int myMenuUIFormId;
    int levelEntityId;
    LevelEntity lvEntity;
    IFsm<IProcedureManager> procedure;
    MyMenuUIForm myMenuUIForm;
    
    
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        Log.Info("<<<<<<<<<<<<" + "Success In MyMenuProcedure");
        
        base.OnEnter(procedureOwner);
        procedure = procedureOwner;
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);//订阅Entity打开事件, Entity显示成功时触发
        GF.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);//订阅UI打开事件, UI打开成功时触发
        
        //TO DO  生成UI
        //其实我觉得不应该直接就EnterGame的，之后再改吧
        ShowLevel();

    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        if (Input.GetKey(KeyCode.Space) && GF.UI.GetTopUIFormId() == myMenuUIFormId)
        {
            EnterGame();
        }
    }
    
    
    
    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        //TO DO 关闭Menu：
        
        GF.UI.CloseUIForm(myMenuUIFormId);
        
        GF.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GF.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        base.OnLeave(procedureOwner, isShutdown);
    }
    
    public void EnterGame()
    {
        //this.procedure.SetData<VarInt32>("LevelEntityId", levelEntityId);
        Log.Info("<<<<<<<<<<<<" + "Success Enter MyGameProcedurce");
        this.procedure.SetData<VarInt32>("LevelEntityId", levelEntityId);
        ChangeState<MyGameProcedure>(procedure);
    }
    
    
    public void ShowLevel()
    {
        lvEntity = null;
        myMenuUIForm = null;
        if (GF.Base.IsGamePaused)
        {
            GF.Base.ResumeGame();
        }
        GF.UI.CloseAllLoadingUIForms();
        GF.UI.CloseAllLoadedUIForms();
        GF.Entity.HideAllLoadingEntities();
        GF.Entity.HideAllLoadedEntities();

        //异步打开主菜单UI
        var uiParms = UIParams.Create();//用于给UI传递各种参数
        uiParms.OpenCallback = uiLogic =>
        {
            myMenuUIForm = uiLogic as MyMenuUIForm;
        };
        myMenuUIFormId = GF.UI.OpenUIForm(UIViews.MyMenuUIForm, uiParms);

        //动态创建关卡
        var lvTb = GF.DataTable.GetDataTable<LevelTable>();
        var playerMd = GF.DataModel.GetOrCreate<PlayerDataModel>();
        var lvRow = lvTb.GetDataRow(playerMd.LevelId);

        var lvParams = EntityParams.Create(Vector3.zero, Vector3.zero, Vector3.one);
        lvParams.Set(LevelEntity.P_LevelData, lvRow);
        lvParams.Set(LevelEntity.P_LevelReadyCallback, (GameFrameworkAction)OnLevelAllReady);
        levelEntityId = GF.Entity.ShowEntity<LevelEntity>(lvRow.LvPfbName, Const.EntityGroup.Level, lvParams);
    }
    
    
    
    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
        var args = e as OpenUIFormSuccessEventArgs;

    }
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var args = e as ShowEntitySuccessEventArgs;
        if (args.Entity.Id == levelEntityId)
        {
            lvEntity = args.Entity.Logic as LevelEntity;
            CameraFollower.Instance.SetFollowTarget(lvEntity.transform);
        }
    }
    private void OnLevelAllReady()
    {
        procedure.SetData<VarUnityObject>("LevelEntity", lvEntity);
        GF.BuiltinView.HideLoadingProgress();
    }
    
    
}
