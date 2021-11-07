using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MikroFramework.DataStructures;
using NUnit.Framework;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MikroFramework.Test {
    public class DataStructuresTest {
        public class TestDataItem {
            public string Name { get; set; }

            public int Age { get; set; }
        }


        public class TestDataTable : Table<TestDataItem> {
            public TableIndex<string,TestDataItem> NameIndex { get; private set; }
            public TableIndex<int,TestDataItem> AgeIndex { get; private set; }

            public TestDataTable() {
                NameIndex = new TableIndex<string, TestDataItem>(item => item.Name);
                AgeIndex = new TableIndex<int, TestDataItem>(item => item.Age);
            }

            protected override void OnClear() {
                NameIndex.Clear();
                AgeIndex.Clear();
            }

            public override void OnAdd(TestDataItem item) {
                NameIndex.Add(item);
                AgeIndex.Add(item);
            }

            public override void OnRemove(TestDataItem item) {
                NameIndex.Remove(item);
                AgeIndex.Remove(item);
            }
        }

        [Test]
        public void TableAddGetTest() {
            TestDataTable table = new TestDataTable();

            for (int i = 0; i < 300; i++) {
                table.Add(new TestDataItem() {
                    Name = "Name "+i,
                    Age = i
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10000; i++) {
                foreach (var test in table.Get(item=>item.Age==150 && item.Name=="Name 150")) {
                    
                }
            }

            var oldTime = stopwatch.ElapsedMilliseconds;
            Debug.Log(oldTime);

           stopwatch.Reset();
           stopwatch.Start();
           for (int i = 0; i < 10000; i++) {
               foreach (var test in table.AgeIndex.Get(150).Where(item=>item.Name=="Name 150")) {
                   

               }
           }

           var newTime = stopwatch.ElapsedMilliseconds;
           Debug.Log(newTime);
        }
    }
}

