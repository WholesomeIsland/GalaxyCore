﻿using GalaxyCoreCommon;
using GalaxyCoreServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMmoServer.RPGTemplate
{
    public class RPGTemplatePlayer : NetEntity
    {
        /// <summary>
        /// Скорость движения персонажа
        /// </summary>
        [GalaxyVar(1)]
        public float move;
        /// <summary>
        /// Текущие жизни
        /// </summary>
        [GalaxyVar(10)]
        public int heal = 100;
        /// <summary>
        /// Максимальный хп
        /// </summary>
        [GalaxyVar(11)]
        public int maxHeal = 100;
       
        public RPGTemplatePlayer(Instance instance, GalaxyVector3 position = default, GalaxyQuaternion rotation = default, NetEntityAutoSync syncType = NetEntityAutoSync.position_and_rotation) : base(instance, position, rotation, syncType)
        {
            prefabName = "RPGTemplatePlayer";          
        }   

        public override void InMessage(byte externalCode, byte[] data, Client clientSender)
        {
            // переоправляем сообщение всем кто видит эту сущность
            SendMessageByOctoVisible(externalCode, data,GalaxyDeliveryType.reliable);
        }

        public override void OnDestroy()
        {
            
        }

        public override void Start()
        {
            // дергаем метод поиска мобов раз в секунду
            // использовать InvokeRepeating значительно дешевле чем считать время в Update
            InvokeRepeating("MobFinder", 1, 1);
            InvokeRepeating("StatControl", 1, 1);
            galaxyVars.RegistrationClass(this);
        }

        public override void Update()
        {
            
        }

        public void StatControl()
        {
            if(heal<maxHeal)heal++;
        }

        public void MobFinder()
        {
            // смотрим нет ли мобов в радиусе 20 метров
            // искать мобов игроками дешевле чем мобами игроков
            Mob mob;
            foreach (var item in instance.entities.GetNearby(transform.position, 20))
            {
                mob = item as Mob;
                if (mob != null) {
                    mob.PlayerNear(this);                
                }
            }  
        }

        /// <summary>
        /// Нанесение урона
        /// </summary>
        /// <param name="damage"></param>
        public void SetDamage(int damage)
        {
            heal -= damage;
        }
    }
}
