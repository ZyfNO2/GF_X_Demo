using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using Random = System.Random;

/// <summary>
/// 生成之后改成多线程
/// </summary>
public class BuildingEntity : SampleEntity
{
    
    private float spawnInterval = 5f;
    private float lastSpawnTime;    
    
    List<int> loadEntityTaskList;

    private List<UnitEntity> selfUnitList;
    
    
    Image image;
    
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        GF.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        
        
        loadEntityTaskList = new List<int>();
        selfUnitList = new List<UnitEntity>();
        //imagefillamount要加this。否者会影响第一个
        image = this.transform.Find("Canvas").transform.Find("Image").GetComponent<Image>();
        
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
    


    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        //this.transform.position += Vector3.forward * 0.01f;
        
        SpawnEntity();
    }

    


    private void SpawnEntity()
    { 
        //要加this
        this.image.fillAmount -= Time.deltaTime * 0.2f;
        
        if (Time.time - lastSpawnTime >  spawnInterval)
        {
            var unitEntityParams = EntityParams.Create(this.transform.position, this.transform.eulerAngles,
                this.transform.localScale);
        
            this.image.fillAmount = 1;
            
            //!!!硬编码，之后读表改
            var mUnitEntityId = GF.Entity.ShowEntity<UnitEntity>("EnemyTest", Const.EntityGroup.Unit, unitEntityParams );
            
            loadEntityTaskList.Add(mUnitEntityId);
            lastSpawnTime += Time.time;
        }
    }


    
    
    
    private void OnShowEntitySuccess(object sender, GameEventArgs e)
    {
        var eArgs = e as ShowEntitySuccessEventArgs;
        
        if (loadEntityTaskList.Contains(eArgs.Entity.Id))
        {
            var unitEntity = GF.Entity.GetEntity<UnitEntity>(eArgs.Entity.Id);
        }
    }


}