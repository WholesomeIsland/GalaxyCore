﻿using GalaxyCoreLib.NetEntity; 
using UnityEngine;

public class ExamplePlayer : MonoBehaviour
{
    ClientNetEntity netEntity;
    Material material;
    void Awake()
    {
        netEntity = GetComponent<UnityNetEntity>().netEntity;
        material = GetComponentInChildren<MeshRenderer>().material;         
    }

    void OnEnable()
    {
        netEntity.OnInMessage += OnInMessage;
    }
    void OnDisable()
    {
        netEntity.OnInMessage -= OnInMessage;
    }

    private void OnInMessage(byte code, byte[] data)
    {
        switch (code)
        {
            case 200:
                material.color = Color.red;
                break;
            case 201:
                material.color = Color.green;
                break;
            default:
                break;
        }
    }
}
