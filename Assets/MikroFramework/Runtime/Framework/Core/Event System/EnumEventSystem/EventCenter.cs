using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MikroFramework;
using UnityEngine;

namespace MikroFramework.Event {

    

    public class EventCenter
    {
        private static Dictionary<EventType, Action<MikroMessage>> eventTable = new Dictionary<EventType, Action<MikroMessage>>();
      
        
        /// <summary>
        /// Add a listener to the corresponding event type, which must have at least one callback function
        /// </summary>
        /// <param name="eventType">The type of the event (create it in the EventType class)</param>
        /// <param name="callBack">A callback function, which can have 0-5 parameters</param>
        public static IUnRegister AddListener(EventType eventType, Action<MikroMessage> callBack)
        {
            if (OnListenerAdding(eventType, callBack))
            {
                //�ಥί�����
                eventTable[eventType] = eventTable[eventType] + callBack;
            }

            return new EnumEventSystemUnRegister(eventType, callBack);

        }



        /// <summary>
        ///  Remove a callback function from the corresponding event type, which must have at least one callback function
        /// </summary>
        /// <param name="eventType">The type of the event (create it in the EventType class)</param>
        /// <param name="callBack">A callback function with no parameters</param>
        public static void RemoveListener(EventType eventType, Action<MikroMessage> callBack)
        {
            bool success = OnListenerRemoving(eventType, callBack);
            if (success) {
                //�Ƴ�ָ���Ļص�����
                eventTable[eventType] = eventTable[eventType] - callBack;
                OnListenerRemoved(eventType);
            }
            
        }



        /// <summary>
        /// Remove all callback functions of a specific event
        /// Note: This is irreversible! It will also remove callback functions of this event on other scripts!
        /// </summary>
        /// <param name="eventType"></param>
        public static void RemoveAllListeners(EventType eventType)
        {
            if (eventTable.ContainsKey(eventType))
            {
                eventTable.Remove(eventType);
            }
            else
            {
                throw new Exception(string.Format("Could not find event type {0}", eventType));
            }
        }

        /// <summary>
        /// Broadcast all callback functions with no parameters from an existing eventType
        /// </summary>
        /// <param name="eventType">The type of the event</param>
        public static void Broadcast(EventType eventType,MikroMessage data)
        {
            Action<MikroMessage> d;
            //��ȡ�������µ�ί��
            if (eventTable.TryGetValue(eventType, out d))
            {
                Action<MikroMessage> callback = d;
                if (callback != null)
                {
                    //����ί�е����лص�����
                    callback(data);
                }
                else
                {
                    throw new Exception(string.Format("Failed to broadcast event {0}: The delegate has a different type", eventType));
                }
            }
        }


        private static bool OnListenerAdding(EventType eventType, Action<MikroMessage> callBack)
        {
            //�¼���ָ���¼����͵�ֵΪ��ʱ, Ϊ���¼����ʹ���һ���µĿռ�, ������ί��
            if (!eventTable.ContainsKey(eventType))
            {
                eventTable.Add(eventType, callBack);
                return false;
            }

            return true;
        }

        private static bool OnListenerRemoving(EventType eventType, Action<MikroMessage> callBack)
        {
            //�������Ƿ��и��¼�����
            if (eventTable.ContainsKey(eventType))
            {
                Action<MikroMessage> d = eventTable[eventType];
                //�ж��¼������Ƿ�����κλص�����
                if (d == null)
                {
                    //throw new Exception(string.Format("The event {0} does not have corresponding delegate", eventType));
                    //�ж��Ƴ����¼��������Ƿ�ͱ��е�����һ��
                    return false;
                }

                return true;
            }
            else
            {
                //throw new Exception("Failed to remove the listener: no eventType");
                return false;
            }
        }

        private static void OnListenerRemoved(EventType eventType)
        {
            if (eventTable[eventType] == null)
            {
                eventTable.Remove(eventType);
            }
        }
    }

}







