using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MikroFramework.ResKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MikroFramework.Test {
    public class ResKitTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LoadAsyncExceptionTest() {
            var resLoader = new ResLoader();
            Assert.Throws<Exception>(() => {
                resLoader.LoadAsync<AssetBundle>("test_sprite",
                    ab => { Debug.Log(ab.name); });
                resLoader.LoadSync<AssetBundle>("test_sprite");
            });
            resLoader.ReleaseAllAssets();
            resLoader = null;
        }

        [UnityTest]
        public IEnumerator LoadAsyncTest() {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<AssetBundle>("mftest",
                ab => {  });

            resLoader.LoadAsync<AudioClip>("resources://War loop", result => {
                Assert.Equals("War loop", result.name);

                resLoader.ReleaseAllAssets();
                resLoader = null;
            });

            yield return null;
        }

        [UnityTest]
        public IEnumerator LoadAsyncCountTest() {
            var resLoader = new ResLoader();

            var loadCount = 0;

            resLoader.LoadAsync<AssetBundle>("mftest",
                ab => {
                    loadCount++;
                });

            resLoader.LoadAsync<AssetBundle>("mftest",
                ab => {
                    loadCount++;
                });

            yield return new WaitUntil(() => loadCount == 2);

            Res result= ResManager.GetSharedLoadedRes().Find(res => res.Name == "mftest");

            Assert.AreEqual(1,result.RefCount);

            var resLoade2 = new ResLoader();
            resLoade2.LoadAsync<AssetBundle>("mftest",
                ab => {
                    loadCount++;
                });
            
            result = ResManager.GetSharedLoadedRes().Find(res => res.Name == "mftest");

            Assert.AreEqual(2, result.RefCount);

            resLoade2.ReleaseAllAssets();
            resLoader.ReleaseAllAssets();

            Assert.False(ResManager.GetSharedLoadedRes().Any(res => res.Name == "mftest"));
        }

        [UnityTest]
        public IEnumerator LoadABDependencyTest() {
            var resLoader = new ResLoader();

            resLoader.LoadAsync<AssetBundle>("mftest",
                ab => {
                    GameObject obj = ab.LoadAsset<GameObject>("TestABObj");
                    Assert.IsNotNull(obj);
                });
            yield return null;
        }


        [UnityTest]
        public IEnumerator LoadAssetSyncTest() {
            ResLoader resLoader = new ResLoader();

            Texture2D texture2D = resLoader.LoadSync<Texture2D>("test_sprite", "Asteroid_1");

            Assert.IsNotNull(texture2D);
            resLoader.ReleaseAllAssets();
            resLoader = null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator LoadAssetAsyncTest() {
            ResLoader resLoader = new ResLoader();

            resLoader.LoadAsync<Texture2D>("test_sprite", "Asteroid_1",
                (t) => {

                    Assert.IsNotNull(t);
                    resLoader.ReleaseAllAssets();
                    resLoader = null;
                   
                });
            yield return null;
        }

        
    }

}
