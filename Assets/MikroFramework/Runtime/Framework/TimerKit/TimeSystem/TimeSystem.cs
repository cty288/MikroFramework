using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MikroFramework.TimeSystem
{

    public enum TimerUnit {
        Millisecond,
        Second,
        Minute,
        Hour,
        Day
    }
    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        public class TimeSystemUpdate : MonoBehaviour
        {
            public event Action OnUpdate;



            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }

        private List<DelayTask> taskList = new List<DelayTask>();

        private TimeSystemUpdate updater;
        private bool isStart = false;
        public bool IsStart
        {
            get
            {
                return isStart;
            }
        }
        public TimeSystem()
        {
            OnInit();
            Start();
        }

        /// <summary>
        /// Start or resume the timer
        /// </summary>
        public void Start()
        {
            updater.OnUpdate -= OnUpdate;
            updater.OnUpdate += OnUpdate;
            isStart = true;
        }

        public void Reset()
        {
            CurrentSeconds = 0;
            delayTasks.Clear();
            taskList.Clear();
        }

        /// <summary>
        /// Pause the timer
        /// </summary>
        public void Pause()
        {
            updater.OnUpdate -= OnUpdate;
            isStart = false;
        }

        protected override void OnInit()
        {
            GameObject updateBehaviorGameObj = new GameObject(nameof(TimeSystemUpdate));

            Object.DontDestroyOnLoad(updateBehaviorGameObj);

            updater = updateBehaviorGameObj.AddComponent<TimeSystemUpdate>();
        }

        private void OnUpdate()
        {
            CurrentSeconds += Time.deltaTime;
            if (delayTasks.Count > 0)
            {
                var currentNode = delayTasks.First;

                while (currentNode != null)
                {
                    DelayTask delayTask = currentNode.Value;
                    var nextNode = currentNode.Next;

                    if (delayTask.State == DelayTaskState.NotStart)
                    {
                        delayTask.State = DelayTaskState.Started;
                        delayTask.StartSeconds = CurrentSeconds;
                        delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                    }
                    else if (delayTask.State == DelayTaskState.Started)
                    {
                        if (CurrentSeconds > delayTask.FinishSeconds) {
                            delayTask.CurrentLoopCount++;

                            if (delayTask.CurrentLoopCount >= delayTask.MaxLoopCount && delayTask.MaxLoopCount>=0) {
                                delayTask.State = DelayTaskState.Finished;
                                
                                delayTasks.Remove(currentNode);
                            }
                            else {
                                delayTask.StartSeconds = CurrentSeconds;
                                delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                            }
                            delayTask.OnFinish?.Invoke();

                        }
                    }

                    currentNode = nextNode;
                }
            }
        }

        public float CurrentSeconds { get; private set; } = 0f;

        private LinkedList<DelayTask> delayTasks = new LinkedList<DelayTask>();

        [Obsolete("AddDelayTask() with two parameters is obsolete, use the one with three parameters instead")]
        public ITimeSystem AddDelayTask(float seconds, Action onFinished)
        {
            DelayTask delayTask = new DelayTask()
            {
                Seconds = seconds,
                OnFinish = onFinished,
                State = DelayTaskState.NotStart,
                MaxLoopCount = 1,
                CurrentLoopCount = 0
            };

            delayTasks.AddLast(new LinkedListNode<DelayTask>(delayTask));
            taskList.Add(delayTask);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="onFinished"></param>
        /// <param name="timerUnit"></param>
        /// <param name="loopCount">Use -1 for infinite loop count</param>
        /// <returns></returns>
        public ITimeSystem AddDelayTask(float delay, Action onFinished, TimerUnit timerUnit = TimerUnit.Second, int loopCount = 1) {
            float timeInSeconds = 0;

            
            switch (timerUnit) {
                case TimerUnit.Second:
                    timeInSeconds = delay;
                    break;
                case TimerUnit.Millisecond:
                    timeInSeconds = delay / 1000f;
                    break;
                case TimerUnit.Minute:
                    timeInSeconds = delay * 60f;
                    break;
                case TimerUnit.Hour:
                    timeInSeconds = delay * 60f * 60f;
                    break;
                case TimerUnit.Day:
                    timeInSeconds = delay * 60f * 60f * 24f;
                    break;
            }
            
            DelayTask delayTask = new DelayTask()
            {
                Seconds = timeInSeconds,
                OnFinish = onFinished,
                State = DelayTaskState.NotStart,
                MaxLoopCount = loopCount,
                CurrentLoopCount = 0
            };

            delayTasks.AddLast(new LinkedListNode<DelayTask>(delayTask));
            taskList.Add(delayTask);
            return this;
        }

        /// <summary>
        /// Reset the timer
        /// </summary>
        public void Restart()
        {
            CurrentSeconds = 0;
            delayTasks.Clear();

            foreach (DelayTask delayTask in taskList)
            {
                delayTask.State = DelayTaskState.NotStart;
                delayTask.CurrentLoopCount = 0;
                delayTasks.AddLast(delayTask);
            }
            Start();
        }


    }

}
