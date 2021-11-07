using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.FSM;
using NUnit.Framework;
using UnityEngine;

namespace MikroFramework.Test
{
    public class FSMTest {
        [Test]
        public void FSMSimpleTest() {
            //Idle
            //Run
            //Jump
            //DoubleJump
            //Die

            //Run->"up"->jump
            //Jump->"up"->doubleJump
            //jump->"down"->run
            //doubleJump->"down"->run

            FSM.FSM fsm = new FSM.FSM();
            
            FSMState idleState=FSMState.Allocate("idle");
            FSMState runState = FSMState.Allocate("run");
            FSMState jump = FSMState.Allocate("jump");
            FSMState doubleJump=FSMState.Allocate("double_jump");
            FSMState die = FSMState.Allocate("die");

            FSMTranslation runToJump= FSMTranslation.Allocate(runState,"up",jump,()=>{});
            FSMTranslation jumpToDoubleJump=FSMTranslation.Allocate(jump,"up",doubleJump,()=>{});
            FSMTranslation jumpToRun = FSMTranslation.Allocate(jump, "down", runState, () => {Debug.Log("Jump to run");});
            FSMTranslation doubleJumpToRun = FSMTranslation.Allocate(doubleJump, "down", runState, () => { });

            fsm.AddState(idleState);
            fsm.AddState(runState);
            fsm.AddState(jump);
            fsm.AddState(doubleJump);
            fsm.AddState(die);

            fsm.AddTranslation(runToJump);
            fsm.AddTranslation(jumpToDoubleJump);
            fsm.AddTranslation(jumpToRun);
            fsm.AddTranslation(doubleJumpToRun);

            fsm.Start(runState);
        } 
    }
}
