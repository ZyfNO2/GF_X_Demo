using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.Event;


public class MyGameProcedure : ProcedureBase
{
    public GameUIForm GameUI { get; private set; }
    public LevelEntity Level { get; private set; }
    private IFsm<IProcedureManager> procedure;

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);
        this.procedure = procedureOwner;

        if (GF.Base.IsGamePaused)
        {
            GF.Base.ResumeGame();
        }
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GF.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        GF.Event.Subscribe(CloseUIFormCompleteEventArgs.EventId, OnCloseUIForm);
        
    }
    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

    }
    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        if (GF.Base.IsGamePaused)
        {
            GF.Base.ResumeGame();
        }
        GF.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        GF.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
        GF.Event.Unsubscribe(CloseUIFormCompleteEventArgs.EventId, OnCloseUIForm);
        base.OnLeave(procedureOwner, isShutdown);
    }
    
    private void CheckGamePause()
    {
        if (GameUI == null)
        {
            return;
        }
        if (GF.UI.GetTopUIFormId() != GameUI.UIForm.SerialId)
        {
            if (!GF.Base.IsGamePaused)
            {
                GF.Base.PauseGame();
            }
        }
        else
        {
            if (GF.Base.IsGamePaused)
            {
                GF.Base.ResumeGame();
            }
        }
    }
    private void OnCloseUIForm(object sender, GameEventArgs e)
    {
        CheckGamePause();
    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
        var args = e as OpenUIFormSuccessEventArgs;
        if (args.UIForm.Logic.GetType() == typeof(GameUIForm))
        {
            GameUI = args.UIForm.Logic as GameUIForm;
            Level?.StartGame();
        }
        CheckGamePause();
    }
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var args = e as ShowEntitySuccessEventArgs;

    }
    

    #region - Debug Mode
    public void ChangeLevel(int level)
    {
        var playerDm = GF.DataModel.GetOrCreate<PlayerDataModel>();
        playerDm.LevelId = level;
        this.procedure.SetData<VarBoolean>("EnterNextLevel", true);
        ChangeState<MenuProcedure>(procedure);
    }
    #endregion
}
