using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

public enum Camp
{
    Undefined,
    Knights,
    Goblins,
    
}



public class BuildingEntity : SampleEntity
{
    private Camp camp;
    private float spawnInterval = 5f;
    private float lastSpawnTime;    
    
    List<int> loadEntityTaskList;

    private List<UnitEntity> unitList;


    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        loadEntityTaskList = new List<int>();
        unitList = new List<UnitEntity>();
    }


    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        GF.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
    }
    
    public void SetCamp(Camp camp)
    {
        this.camp = camp;
    }

    public Camp GetCamp()
    {
        return this.camp;
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        SpawnEntity();
    }

    


    private void SpawnEntity()
    {
        if (Time.time - lastSpawnTime >  spawnInterval)
        {
            //Do Spawn
            var unitEntityParams = EntityParams.Create(this.transform.position, this.transform.eulerAngles,
                this.transform.localScale);

            unitEntityParams.OnShowCallback = logic =>
            {
                
            };
            
            lastSpawnTime += Time.time;
            //!!!硬编码，之后读表改
            var mUnitEntityId = GF.Entity.ShowEntity<UnitEntity>("EnemyTest", Const.EntityGroup.Unit, unitEntityParams );
            
            
            loadEntityTaskList.Add(mUnitEntityId);
        
            
            
        }
    }
    
    
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var eArgs = e as ShowEntitySuccessEventArgs;
        
        if (loadEntityTaskList.Contains(eArgs.Entity.Id))
        {
            var unitEntity = GF.Entity.GetEntity<UnitEntity>(eArgs.Entity.Id);
            unitEntity.SetCamp(Camp.Goblins);
            unitList.Add(unitEntity);
            Log.Info("<<<<<<<<<<<<<<<" + unitEntity.GetCamp());
            
            loadEntityTaskList.Remove(eArgs.Entity.Id);
        }
    }

    
}