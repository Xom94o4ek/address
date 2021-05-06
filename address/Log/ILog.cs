using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace address.Log
{
    public interface ILog
    {
        /// <summary>
        /// Предупреждение: на работу приложения не влияет, 
        /// но может сообщать о проблемах при работе с приложением
        /// </summary>
        /// <param name="message">сообщение</param>
        void Warning(string message);


        /// <summary>
        /// Информирование: не влияет на работу приложения,
        /// является инструментом информирования
        /// </summary>
        /// <param name="message">сообщение</param>
        void Info(string message);

        /// <summary>
        /// Информирование: не влияет на работу приложения,
        /// является инструментом информирования
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <param name="args">аргументы</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Дебагирование: инструмент для трассировки и отладки
        /// </summary>
        /// <param name="message">сообщение</param>
        void Debug(string message);

        /// <summary>
        /// Дебагирование: инструмент для трассировки и отладки
        /// </summary>
        /// <param name="message">сообщение</param>
        /// <param name="e">Exception</param>
        void Debug(string message, Exception e);

        /// <summary>
        /// Дебагирование: инструмент для трассировки и отладки
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args">аргументы</param>
        void DebugFormat(string message, params object[] args);

    }
}
