﻿using GalaxyCoreCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalaxyColliderSphere : MonoBehaviour
{
    SphereCollider collider;
    float size = 1;
    public string physTag = "";
    private void OnDrawGizmos()
    {
        if (collider == null) collider = GetComponent<SphereCollider>();
        GetSize();
        Gizmos.color = new Color(1f, 0.8f, 0f, 0.4f);
        Gizmos.DrawSphere(transform.position, size+0.3f);
    }
    public PhysSphereCollider Bake()
    {
        PhysSphereCollider bake = new PhysSphereCollider();
       
        bake.position = transform.position.NetworkVector3();
        bake.radius = size;
        bake.tag = physTag;
        if (physTag == "") bake.tag = transform.name;
        return bake;
    }

    private float GetSize()
    {
        size = collider.radius;
        size = size * transform.localScale.x + 0.1f;   
        return size;
    }
}
