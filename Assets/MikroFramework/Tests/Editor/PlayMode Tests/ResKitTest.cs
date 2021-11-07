using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MikroFramework.ResKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace MikroFramework.Test {
    public class ResKitTest
    {
        // A Test behaves as an ordinary method

        [UnityTest]
        public IEnumerator LoadAsyncExceptionTest() {
            bool finished = false;
            ResLoader.Create(resLoader => {
                Assert.Throws<Exception>(() => {
                    resLoader.LoadAsync<AssetBundle>("mftest",null);
                    resLoader.LoadSync<AssetBundle>("mftest");
                   
                    
                });

                resLoader.ReleaseAllAssets();
                resLoader = null;
                finished = true;

            });

            while (!finished) {
                yield return null;
            }
        }


        [UnityTest]
        public IEnumerator LoadAsyncTest() {
            ResLoader resLoader=null;
            ResLoader.Create(loader => {
                resLoader = loader;
                resLoader.LoadAsync<AssetBundle>("mftest",
                    ab => { });

                resLoader.LoadAsync<AudioClip>("resources://War loop", result => {
                    Assert.AreEqual("War loop", result.name);

                    
                });
            });

            

            yield return new WaitForSeconds(3f);
            resLoader.ReleaseAllAssets();
            resLoader = null;
        }

        [UnityTest]
        public IEnumerator LoadAsyncCountTest() {
            ResLoader resLoader=null;
            ResLoader.Create(loader => {
                resLoader = loader;
            });

            yield return new WaitForSeconds(3f);

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

            Res result = ResManager.GetSharedLoadedRes().NameIndex.Get("mftest").FirstOrDefault();

            Assert.AreEqual(1,result.RefCount);

            var resLoade2 = new ResLoader();
            resLoade2.LoadAsync<AssetBundle>("mftest",
                ab => {
                    loadCount++;
                });

            result = ResManager.GetSharedLoadedRes().NameIndex.Get("mftest").FirstOrDefault();

            Assert.AreEqual(2, result.RefCount);

            resLoade2.ReleaseAllAssets();
            resLoader.ReleaseAllAssets();

            Assert.False(ResManager.GetSharedLoadedRes().NameIndex.Get("mftest").FirstOrDefault()!=null);
        }

        [UnityTest]
        public IEnumerator LoadABDependencyTest() {
            ResLoader.Create(resLoader => {
                resLoader.LoadAsync<AssetBundle>("mftest",
                    ab => {
                        Debug.Log(ab.name);
                        GameObject obj = ab.LoadAsset<GameObject>("TestABObj");
                        Assert.IsNotNull(obj);
                    });
            });

            
            yield return new WaitForSeconds(3f);
        }


        [UnityTest]
        public IEnumerator LoadAssetSyncTest() {
            ResLoader resLoader = new ResLoader();
            yield return new WaitForSeconds(1f);
            Texture2D texture2D = resLoader.LoadSync<Texture2D>("mftest", "Asteroid_1");

            Debug.Log(texture2D.name);
            Assert.IsNotNull(texture2D);
            resLoader.ReleaseAllAssets();
            resLoader = null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator LoadAssetAsyncTest() {
            ResLoader resLoader = new ResLoader();
            yield return new WaitForSeconds(2f);
            resLoader.LoadAsync<Texture2D>("mftest", "Asteroid_1",
                (t) => {

                    Assert.IsNotNull(t);
                    Debug.Log(t.name);
                    
                    
                   
                });
            yield return new WaitForSeconds(4f);
            resLoader.ReleaseAllAssets();
            resLoader = null;
        }

        [UnityTest]
        public IEnumerator ReadJsonFile()
        {
            ResLoader resLoader = new ResLoader();
            yield return new WaitForSeconds(1f);
            Object json = resLoader.LoadSync<Object>("mftest", "FSMJsonTemplate");
            Assert.NotNull(json);
            Debug.Log(json.ToString());
        }

        [Test]
        public void GetAssetDataTest() {
            ResManager.SimulationMode = true;

            ResData.Singleton.Init(null,(e)=>{});

            AssetData audioClipData = ResData.Singleton.GetAssetData(ResSearchKeys.Allocate("War loop",
                typeof(AudioClip)));

            Assert.IsNotNull(audioClipData);
            Assert.AreEqual(audioClipData.OwnerBundleName, "mftest");
            Assert.AreEqual(audioClipData.AssetType,typeof(AudioClip));

            AssetData gameobjectData = ResData.Singleton.GetAssetData(ResSearchKeys.Allocate("Sphere",
                typeof(GameObject)));

            Assert.IsNotNull(gameobjectData);
            Assert.AreEqual(gameobjectData.OwnerBundleName, "test_sphere");
            Assert.AreEqual(gameobjectData.AssetType, typeof(GameObject));
        }


        [Test]
        public void LoadAssetWithNameAndTypeTest() {
#if UNITY_EDITOR
            ResManager.SimulationMode = true;
#endif
            ResLoader resLoader = new ResLoader();

            AudioClip audioClip = resLoader.LoadSync<AudioClip>("War loop");
            
            Assert.IsTrue(audioClip);

            GameObject gameObject = resLoader.LoadSync<GameObject>("War loop");
            Assert.IsTrue(gameObject);
            
            

            resLoader.ReleaseAllAssets();
            resLoader = null;

        }

        [UnityTest]
        public IEnumerator LoadAssetWithNameAndTypeAsyncTest()
        {
#if UNITY_EDITOR
            ResManager.SimulationMode = true;
#endif

            ResLoader resLoader = new ResLoader();

            int loadCount = 0;

            resLoader.LoadAsync<AudioClip>("War loop", (res) => {
                Assert.IsTrue(res);
                loadCount++;
            });

            resLoader.LoadAsync<GameObject>("War loop", res => {
                Assert.IsTrue(res);
                loadCount++;
            });

            while (loadCount!=2) {
                yield return null;
            }



            resLoader.ReleaseAllAssets();
            resLoader = null;

        }



    }

}
