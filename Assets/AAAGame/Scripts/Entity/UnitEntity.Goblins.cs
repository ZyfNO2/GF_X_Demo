using UnityEngine;

public partial class UnitEntity
{
    private void GoblinsAttack()
    {
        // check distance
        if (Vector3.Distance(GetPlayerTransform().position, this.transform.position) > atkDistance)
        {
            m_Agent.speed = 3;
            return;
        }
        //get target
        
        
        // check gizmos
        // later
        
        //do attack 
        m_Agent.speed = 0;
        
        GF.Entity.HideEntity(this.Entity);
        
    }


}