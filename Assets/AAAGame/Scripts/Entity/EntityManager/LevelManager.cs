using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LevelManager : Singleton<LevelManager>
{
    public int playerId;

    public List<int> enemyList = new List<int>();

    public override void OnInit()
    {
        base.OnInit();
        
        enemyList = new List<int>();
        
    }
}
