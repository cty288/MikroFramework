using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using MikroFramework.ActionKit;
using MikroFramework.Pool;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace MikroFramework.Test {

    public class ActionKitTest {

        [UnityTest]
        public IEnumerator CallbackFirstTest() {
            bool callbackCalled = false;

            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            actionKit.AddAction(1.0f, CallbackAction.Allocate(()=> stopwatch.Stop()));
            actionKit.Play();

            Assert.IsTrue(stopwatch.IsRunning);
            yield return new WaitForSeconds(1.5f);
            Assert.IsFalse(stopwatch.IsRunning);

            Assert.IsTrue(stopwatch.ElapsedMilliseconds>900 && stopwatch.ElapsedMilliseconds<1100);
            Debug.Log(stopwatch.Elapsed);
        }


        [UnityTest]
        public IEnumerator SequenceTest() {
            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();

            Sequence sequence = Sequence.Allocate();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double totalElapsedSeconds = 0;

            for (int i = 0; i < 300; i++) {
                sequence.AddAction(CallbackAction.Allocate(() => {
                    stopwatch.Stop();
                    totalElapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                    stopwatch.Start();
                }));
            }

            actionKit.Play(sequence);
            yield return new WaitForSeconds(5f);
            Assert.Greater(totalElapsedSeconds,0.5f);
        }

        [UnityTest]
        public IEnumerator SpawnTest() {
            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();

            Spawn spawn = Spawn.Allocate();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double totalElapsedSeconds = 0;

            for (int i = 0; i < 300; i++)
            {
                spawn.AddAction(CallbackAction.Allocate(() => {
                    stopwatch.Stop();
                    totalElapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                    stopwatch.Start();
                }));
            }

            actionKit.Play(spawn);

            yield return new WaitForSeconds(5.0f);

            Assert.Less(totalElapsedSeconds,0.3f);

        }


        [UnityTest]
        public IEnumerator TimelineTest() {
            bool callbackCalled = false;

            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Timeline timeline = Timeline.Allocate();
            timeline.AddAction(1.0f, CallbackAction.Allocate(()=>stopwatch.Stop()));

            actionKit.Play(timeline);

            Assert.IsTrue(stopwatch.IsRunning);
            yield return new WaitForSeconds(1.5f);
            Assert.IsFalse(stopwatch.IsRunning);

            Assert.IsTrue(stopwatch.ElapsedMilliseconds > 900 && stopwatch.ElapsedMilliseconds < 1100);
            Debug.Log(stopwatch.Elapsed);
        }

        [Test]
        public void ActionKitPlayTest() {
            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();
            bool called = false;
            CallbackAction callbackAction = CallbackAction.Allocate(() => {
                called = true;
            });
            actionKit.Play(callbackAction);
            Assert.IsTrue(called);
        }

        [UnityTest]
        public IEnumerator NestedActionTest() {
            bool called = false;
            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();
            
            Sequence sequence = Sequence.Allocate();
            Spawn spawn = Spawn.Allocate();
            Timeline timeline = Timeline.Allocate();

            sequence.AddAction(spawn);
            spawn.AddAction(timeline);
            timeline.AddAction(1.0f, CallbackAction.Allocate(() => { called = true;}));

            actionKit.Play(sequence);
            yield return new WaitForSeconds(2f);
            Assert.IsTrue(called);
        }

        class ABCAction : MikroAction {
            

            private float startSeconds = 0;

            public Action finishCallback = () => { };

            public override void Reset() {
                
            }

            protected override void OnBegin() {
                startSeconds = Time.time;
            }

            protected override void OnExecuting() {
                if (Time.time - startSeconds > 2.0f)
                {
                    Finished.Value = true;
                    finishCallback();
                    
                }
               
            }

            protected override void RecycleBackToPool() {
                SafeObjectPool<ABCAction>.Singleton.Recycle(this);
            }
        }

        [UnityTest]
        public IEnumerator AsyncActionTest() {
            ActionKit.ActionKit actionKit = new ActionKit.ActionKit();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            actionKit.Play(new ABCAction() {
                finishCallback = () => { stopwatch.Stop();}
            });

            yield return new WaitForSeconds(2.5f);

            Assert.Greater(stopwatch.Elapsed.TotalSeconds,1.9f);
            Assert.Less(stopwatch.Elapsed.TotalSeconds,2.1f);
        }
    }
}

