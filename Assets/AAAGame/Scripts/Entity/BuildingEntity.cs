using System.Collections.Generic;
using UnityEngine;

public enum Camp
{
    Knights,
    Goblins,
    
}



public class BuildingEntity : SampleEntity
{
    private Camp camp;
    private float spawnInterval = 5f;
    private float lastSpawnTime;    
    
    List<int> loadEntityTaskList;
    
    
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
                if (loadEntityTaskList.Count > 0)
                {
                    foreach (var unitEntityId in loadEntityTaskList)
                    {
                        var unitEntity = GF.Entity.GetEntity<BuildingEntity>(unitEntityId);
            
                        unitEntity.SetCamp(Camp.Knights);
                    }
                }
                
            };
            
            //!!!硬编码，之后读表改
            var mUnitEntityId = GF.Entity.ShowEntity<UnitEntity>("EnemyTest", Const.EntityGroup.Unit,unitEntityParams );
            
            loadEntityTaskList.Add( mUnitEntityId);

            lastSpawnTime += Time.time;
        }
    }
    
    
    
}