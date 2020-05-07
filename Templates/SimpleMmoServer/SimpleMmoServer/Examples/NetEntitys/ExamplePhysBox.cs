﻿using GalaxyCoreCommon;
using GalaxyCoreServer;
using GalaxyCoreServer.Physics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMmoServer.Examples.NetEntitys
{
    /// <summary>
    /// Сетевой объект показывающий пример работы бокс коллайдера
    /// </summary>
    public class ExamplePhysBox : NetEntity
    {  
          

        public ExamplePhysBox(Instance instance, GalaxyVector3 position = default, GalaxyQuaternion rotation = default, NetEntityAutoSync syncType = NetEntityAutoSync.position_and_rotation) : base(instance, position, rotation, syncType)
        {
            //Задаем имя префаба соответствующего данному объекту
            prefabName = "ExampleBox";           
        }

        public override void Start()
        {
           
            // создаем рабочий коллайдер. Без коллайдера объект не может учавствовать в физическом представлении
            ColliderBox collider = new ColliderBox(new GalaxyVector3(0.4f, 0.4f, 0.4f));  
            physics.Activate(collider); // активируем физику     
            physics.mass = 1f; // устанавливае вес объекта в кг
            transform.syncType = NetEntityAutoSync.position_and_rotation; // указываем способ синхронизации 
        }


        public override void InMessage(byte externalCode, byte[] data, Client client)
        {

        }

        public override void Update()
        {
            // применяем физические расчеты к объекту
            physics.ApplyPhys();           
        }


        public override void OnDestroy()
        {

        }
    }
}
