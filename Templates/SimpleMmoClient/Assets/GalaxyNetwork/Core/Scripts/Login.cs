﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GalaxyCoreLib;
using GalaxyCoreLib.Api;
using SimpleMmoCommon.Messages;
using GalaxyCoreCommon;

public class Login : MonoBehaviour
{
    public InputField login; 
    public InputField password;
    public Text status;
    private void OnEnable()
    {
        GalaxyEvents.OnGalaxyApprovalResponse += OnGalaxyApprovalResponse; //подписка на событие об ошибках авторизации
        GalaxyEvents.OnGalaxyConnect += OnGalaxyConnect; // событие успешного коннекта         
    }

    private void OnGalaxyConnect(byte[] message)
    {
        Debug.Log(message.Length);
        MessageApproval messageApproval = new MessageApproval();
        messageApproval = BaseMessage.Deserialize<MessageApproval>(message); // распаковываем первое ответное сообщение   
        Debug.Log("Наше имя " + messageApproval.name);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GalaxyEvents.OnGalaxyApprovalResponse -= OnGalaxyApprovalResponse;
        GalaxyEvents.OnGalaxyConnect -= OnGalaxyConnect;
    }

    private void OnGalaxyApprovalResponse(byte code, string message)
    {
        status.text = message;
    }

    /// <summary>
    /// Метод авторизации вызываемый из UI
    /// </summary>
    public void Auth()
    {
        //Создаем новое сообщение аунтефикации, которое мы положили в GalaxyTemplateCommon
        MessageAuth messageAuth = new MessageAuth();
        if (login.text.Length < 4)
        {
            status.text = "Какой то очень короткий логин";
            return;
        }
        status.text = "Подключение...";
        messageAuth.login = login.text;
        messageAuth.password = password.text;
        byte[] data = messageAuth.Serialize();
        Debug.Log("data.Length " + data.Length);
        GalaxyApi.connection.Connect(data); // Отправляем запрос на сервер    
         
    }

    
}
