using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<UnitEntity>(out UnitEntity unitEntity);
        
        GF.Entity.HideEntity(unitEntity.Id);
        
    }
}
