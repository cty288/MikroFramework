using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using MikroFramework.Utilities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MikroFramework.Test {
    [Serializable]
    public class TestSerialize {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("List")]
        public List<int> List { get; set; }

    }
    public class XMLSerializationTests
    {
        
        [Test]
        public void SerializeXML() {
            TestSerialize serialize = new TestSerialize() {
                Id = 4,
                Name = "test",
                List = new List<int>() {4, 1, 10}
            };
            SerializationUtility.XmlSerialize(serialize,Application.dataPath+"/test.xml");
        }

        [Test]
        public void ReadXML() {
            TestSerialize testSerialize = SerializationUtility.XmlDeserialize<TestSerialize>
                (Application.dataPath+"/test.xml");
            Debug.Log(testSerialize.Id);
        }

        [Test]
        public void BinaryTest()
        {
            TestSerialize serialize = new TestSerialize()
            {
                Id = 1,
                Name = "test2",
                List = new List<int>() { 4, 1, 10 }
            };
            SerializationUtility.BinarySerialize<TestSerialize>(serialize, Application.dataPath + "/test.bytes");
        }

        [Test]
        public void BinaryDeserializeTest() {
            TestSerialize test = SerializationUtility.BinaryDeserialize<TestSerialize>(Application.dataPath + "/test.bytes");
            Debug.Log(test.Name);
        }

      

       

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator XMLSerializationTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }

}
