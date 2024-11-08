using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityGameFramework.Runtime;

public partial class UnitEntity
{
    private void GoblinsOnShow()
    {
        GF.Entity.GetEntity<LevelEntity>(LevelInfo.LevelId).enemyList.Add(this.Id);
        
        //TO DO
        //Read DataTable to Init
         
    }

    private void GoblinsOnHide()
    {
        //GF.Entity.GetEntity<LevelEntity>(LevelInfo.LevelId).enemyList.Remove(this.Id);
    }
    
    
    
    
    private async Task GoblinsAttack()
    {
        // check distance
        if (Vector3.Distance(GetPlayerTransform().position, this.transform.position) > atkDistance )
        {
            m_Agent.speed = 3;
            atkPosition.gameObject.SetActive(false);
            return;
        }
        await Task.Delay(1000);
        GobinsAttack_Explosion();
        
    }

    private void GobinsAttack_Explosion()
    {
        
        atkPosition.gameObject.SetActive(true);
        GF.Entity.HideEntity(this.Id);
        
        Log.Info("<<<<<<<<" + "Boom");  
        
        Collider[] colliders = Physics.OverlapSphere(Vector3.zero, 3, 1 << LayerMask.NameToLayer("Enemy"));

        Log.Info(colliders.Length);
        
        foreach (var unit in colliders )
        {
            //TO DO生成一个effect罢

            var unitId = unit.GetComponent<UnitEntity>().Id;
            
            GF.Entity.HideEntity(unitId);
            
            Log.Info(colliders.Length);
        }
        //do attack 
        m_Agent.speed = 0;
    }
    
    

    
}