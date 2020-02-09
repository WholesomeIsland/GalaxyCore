﻿using GalaxyCoreServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMmoServer
{
    /// <summary>
    /// Класс для отслеживания логов ядра
    /// </summary>
    public class LogVisualizator
    {
        public LogVisualizator()
        {
            Log.OnLogInfo += OnLogInfo;
            Log.OnLogWarning += OnLogWarning;
            Log.OnLogError += OnLogError;
        }

        private void OnLogError(string publisher, string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Error-> " + publisher + ": " + message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void OnLogWarning(string publisher, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning-> " + publisher + ": " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void OnLogInfo(string publisher, string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Info-> " + publisher + ": " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}