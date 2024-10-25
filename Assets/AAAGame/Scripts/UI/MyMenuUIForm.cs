using UnityEngine;
using GameFramework.Event;

public partial class MyMenuUIForm : UIFormBase
{
    [SerializeField] bool showLvSwitch = false;

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        GF.Event.Subscribe(PlayerDataChangedEventArgs.EventId, OnUserDataChanged);
        GF.Event.Subscribe(PlayerEventArgs.EventId, OnPlayerEvent);
        
    }
    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        GF.Event.Unsubscribe(PlayerDataChangedEventArgs.EventId, OnUserDataChanged);
        GF.Event.Unsubscribe(PlayerEventArgs.EventId, OnPlayerEvent);
        base.OnClose(isShutdown, userData);
    }

    private void OnPlayerEvent(object sender, GameEventArgs e)
    {
        var args = e as PlayerEventArgs;
        
    }

    protected override void OnButtonClick(object sender, string btId)
    {
        base.OnButtonClick(sender, btId);
        switch (btId)
        {
            case "SETTING":
                GF.UI.OpenUIForm(UIViews.SettingDialog);
                break;
        }
    }

    private void OnUserDataChanged(object sender, GameEventArgs e)
    {
        var args = e as PlayerDataChangedEventArgs;
        switch (args.DataType)
        {
            case PlayerDataType.Coins:
               
                break;
            case PlayerDataType.LevelId:

                break;
        }
    }


   
}
