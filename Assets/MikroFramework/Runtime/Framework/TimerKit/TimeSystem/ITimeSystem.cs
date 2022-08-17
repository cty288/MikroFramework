using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

namespace MikroFramework.TimeSystem
{
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }
        ITimeSystem AddDelayTask(float seconds, Action onFinished);

        ITimeSystem AddDelayTask(float delay, Action onFinished, TimerUnit timerUnit = TimerUnit.Second, int loopCount = 1);

        /// <summary>
        /// Restart means restart the timer as well as all tasks. Repeated tasks will also be reset
        /// </summary>
        void Restart();

        /// <summary>
        /// Reset means clear all tasks associated with this timer. Timer will be reset to 0 as well
        /// </summary>
        void Reset();
        void Pause();

        /// <summary>
        /// If the timer is paused, resume it. If the timer is not paused, do nothing
        /// </summary>
        void Start();
    }

}