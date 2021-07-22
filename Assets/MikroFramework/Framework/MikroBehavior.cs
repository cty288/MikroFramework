using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Event;
using MikroFramework.Extensions;
using MikroFramework.Utilities;
using UnityEngine;
using EventType = MikroFramework.Event.EventType;

namespace MikroFramework {
    public abstract partial class MikroBehavior : MonoBehaviour {
        #region Event System Integrated
        private List<CallbackRecord> registeredEventRecorder = new List<CallbackRecord>();

        private SimpleObjectPool<CallbackRecord> callbackRecordPool = new SimpleObjectPool<CallbackRecord>(
            () => { return new CallbackRecord();});

        private class CallbackRecord
        {
            /*
            private static Stack<CallbackRecord> callbackRecordPool = new Stack<CallbackRecord>();

            public CallbackRecord()
            {

            }

            public static CallbackRecord Allocate(EventType eventType, Action<MikroMessage> onEventReceived) {
                if (callbackRecordPool.Count > 0)
                {
                    CallbackRecord record = callbackRecordPool.Pop();
                    record.EventType = eventType;
                    record.OnEventReceived = onEventReceived;
                    return record;
                }
                return new CallbackRecord() { EventType = eventType, OnEventReceived = onEventReceived };
            }


            public void Recycle()
            {
                EventType = EventType.None;
                OnEventReceived = null;
                callbackRecordPool.Push(this);
            }*/

            public EventType EventType;
            public Action<MikroMessage> OnEventReceived;
        }

        /// <summary>
        /// Add a listener to the corresponding event type, which must have at least one callback function
        /// </summary>
        /// <param name="eventType">The type of the event (create it in the EventType class)</param>
        /// <param name="callBack">A callback function</param>
        public void AddListener(EventType eventType, Action<MikroMessage> callBack)
        {
            EventCenter.AddListener(eventType, callBack);
            CallbackRecord record= callbackRecordPool.Allocate();
            record.EventType = eventType;
            record.OnEventReceived = callBack;
           
            registeredEventRecorder.Add(record);
        }


        /// <summary>
        ///  Remove a callback function from the corresponding event type, which must have at least one callback function
        /// </summary>
        /// <param name="eventType">The type of the event (create it in the EventType class)</param>
        /// <param name="callBack">A callback function</param>
        public void RemoveListener(EventType eventType, Action<MikroMessage> callBack)
        {
            var selectedRecords = registeredEventRecorder.FindAll(record =>
               record.EventType == eventType && record.OnEventReceived == callBack);

            selectedRecords.ForEach(record => {
                EventCenter.RemoveListener(record.EventType, record.OnEventReceived);
                callbackRecordPool.Recycle(record);
                registeredEventRecorder.Remove(record);
                //record.Recycle();
            });

            selectedRecords.Clear();
        }

        /// <summary>
        ///  Remove all callback function of this object from the corresponding event type, which must have at least one callback function
        /// </summary>
        /// <param name="eventType">The type of the event (create it in the EventType class)</param>
        /// <param name="callBack">A callback function</param>
        public void RemoveListener(EventType eventType)
        {
            var selectedRecords = registeredEventRecorder.FindAll(record =>
                record.EventType == eventType);

            selectedRecords.ForEach(record => {
                EventCenter.RemoveListener(record.EventType, record.OnEventReceived);
                callbackRecordPool.Recycle(record);
                registeredEventRecorder.Remove(record);
                //record.Recycle();
            });

            selectedRecords.Clear();
        }

        /// <summary>
        /// Broadcast an event
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="data">data included in the event</param>

        public void Broadcast(EventType eventType, MikroMessage data)
        {
            EventCenter.Broadcast(eventType, data);
            //data.Recycle();
        }


        void OnDestroy()
        {
            OnBeforeDestroy();
            foreach (var eventRecord in registeredEventRecorder)
            {
                EventCenter.RemoveListener(eventRecord.EventType, eventRecord.OnEventReceived);
                callbackRecordPool.Recycle(eventRecord);
                //eventRecord.Recycle();
            }
            registeredEventRecorder.Clear();
        }

        /// <summary>
        /// Use this method instead of Destroy()
        /// </summary>
        protected abstract void OnBeforeDestroy();
        #endregion


        /// <summary>
        /// Trigger an event after some seconds
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="onFinished">triggered event</param>
        public void Delay(float seconds, Action onFinished)
        {
            StartCoroutine(DelayCoroutine(seconds, onFinished));
        }

        public void Delay(float seconds, EventType eventType, MikroMessage data)
        {
            StartCoroutine(DelayCoroutine(seconds, eventType, data));
        }

        private IEnumerator DelayCoroutine(float seconds, Action onFinished)
        {
            yield return new WaitForSeconds(seconds);
            onFinished?.Invoke();
        }

        private IEnumerator DelayCoroutine(float seconds, EventType eventType, MikroMessage data)
        {
            yield return new WaitForSeconds(seconds);
            Broadcast(eventType, data);
        }
    }



}

