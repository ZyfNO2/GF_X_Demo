using GameFramework;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
public class MyMenuProcedure : ProcedureBase
{
    IFsm<IProcedureManager> procedure;
    
    
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        Log.Info("<<<<<<<<<<<<" + "Success In MyMenuProcedure");
        
        base.OnEnter(procedureOwner);
        procedure = procedureOwner;
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);//订阅Entity打开事件, Entity显示成功时触发
        GF.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);//订阅UI打开事件, UI打开成功时触发
        
        //TO DO  生成UI
        //var uiParms = UIParams.Create();//用于给UI传递各种参数
        
        GF.UI.OpenUIForm(UIViews.MyMenuUIForm);
        GFBuiltin.BuiltinView.HideLoadingProgress();
        Log.Info("<<<<<<<<<<<<" + "Success Open MyMenuUIForm");

    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
    }
    
    
    
    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        //TO DO 关闭Menu：
        
        GF.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GF.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        base.OnLeave(procedureOwner, isShutdown);
    }
    
    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
        var args = e as OpenUIFormSuccessEventArgs;

    }
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var args = e as ShowEntitySuccessEventArgs;
        // if (args.Entity.Id == levelEntityId)
        // {
        //     lvEntity = args.Entity.Logic as LevelEntity;
        //     CameraFollower.Instance.SetFollowTarget(lvEntity.transform);
        // }
    }
    
    
}
