
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposTest : MonoBehaviour
{
    private void Start()
    {
        // 获取球形检测的中心点（这里以当前物体的位置为例）
        Vector3 center = transform.position;

        // 进行球形检测，获取范围内的所有Collider
        Collider[] colliders = Physics.OverlapSphere(center, 3);

        // 遍历所有被检测到的Collider
        foreach (Collider collider in colliders)
        {
            // 判断Collider所属的游戏对象是否是你想要检测的单位（例如，带有"Enemy"标签的单位）
            if (collider.gameObject.CompareTag("Entity"))
            {
                // 获取到被检测到的单位，可以进行后续操作
                Debug.Log("Detected enemy: " + collider.gameObject.name);
            }
        }
    
    }
}
