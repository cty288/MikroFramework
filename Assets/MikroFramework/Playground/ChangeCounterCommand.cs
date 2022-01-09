using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Playground
{
    public class ChangeCounterCommand {
        private int changeNum;
        public ChangeCounterCommand(int changeNum) {
            this.changeNum = changeNum;
        }

        public void Execute() {
            CounterExampleModel.Singleton.Count.Value += changeNum;
        }
    }
}
