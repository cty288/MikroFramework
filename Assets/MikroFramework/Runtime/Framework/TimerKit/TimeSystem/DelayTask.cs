using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.TimeSystem
{
    public enum DelayTaskState
    {
        NotStart,
        Started,
        Finished
    }

    public class DelayTask
    {
        public float Seconds { get; set; }
        public Action OnFinish { get; set; }
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }
        
        public int MaxLoopCount { get; set; }

        public int CurrentLoopCount { get; set; }
    }
}

