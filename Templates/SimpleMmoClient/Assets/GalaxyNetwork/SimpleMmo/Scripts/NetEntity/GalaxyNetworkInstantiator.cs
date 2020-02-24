﻿using UnityEngine;
using GalaxyCoreLib.Api;
using GalaxyCoreLib.NetEntity;
/// <summary>
/// Компонент отвечающий за создание новый сетевых объектов
/// </summary>
public class GalaxyNetworkInstantiator : MonoBehaviour
{
    private void OnEnable()
    {
        // подписываемся на событие о создании сетевой сущности
        GalaxyEvents.OnGalaxyNetEntityInstantiate += OnGalaxyNetEntityInstantiate;
    }

    private void OnDisable()
    {
        // отписываемся
        GalaxyEvents.OnGalaxyNetEntityInstantiate -= OnGalaxyNetEntityInstantiate;
    }

    private ClientNetEntity OnGalaxyNetEntityInstantiate(ClientNetEntity netEntity)
    {        
        // если нет имени у сетевой сущности, значит не нужно создавать для неё go
        if (netEntity.name == "") return null;
        UnityNetEntity go = Instantiate(Resources.Load<UnityNetEntity>(netEntity.name), netEntity.position.Vector3(), netEntity.rotation.Quaternion());
        // возвращяем целевой экземпляр ClientNetEntity из нового go
        return go.netEntity;   
    }
}
