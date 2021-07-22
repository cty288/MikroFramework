using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace MikroFramework.Test {

    //[CreateAssetMenu(fileName = "TestAssets", menuName = "Create Assets", order = 0)]
    public class AssetsScriptable : ScriptableObject {
        public int Id;
        public string Name;
        public List<string> TestList;
    }


    public class AssetSerializationTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AssetSerializationTestSimplePasses()
        {
            
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AssetSerializationTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }

}
