using System.Collections;
using System.Collections.Generic;
using MikroFramework.ResKit;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MikroFramework.Test {
    public class HotUpdateTest
    {
        

        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator HotUpdateSimpleTest() {
            Application.OpenURL(Application.persistentDataPath);
            bool finished=false;
            HotUpdateManager.Singleton.Init(() => {
                HotUpdateManager.Singleton.HasNewVersionRes((needUpdate, resVersion) => {
                    if (needUpdate)
                    {
                        HotUpdateManager.Singleton.UpdateRes(() => {
                            Debug.Log("Continue");
                            Assert.IsTrue(true);
                            finished = true;
                        }, (error) => {
                            Debug.Log(error.ToString());
                        });
                    }
                    else
                    {
                        Debug.Log("Do not need hot-update. Continue");
                        Assert.IsTrue(true);
                        finished = true;
                    }
                }, error => {
                    Debug.Log(error.ToString());
                });

                
            }, error => {
                Debug.Log(error.ToString());
            });

            while (!finished)
            {
                yield return null;
            }
        }

        [Test]
        public void GetLocalResVersion() {
            //Debug.Log(HotUpdateManager.Singleton.GetNativeResVersion());
        }

        [UnityTest]
        public IEnumerator FakeServerTest()
        {
            HotUpdateManager.Singleton.GetRemoteResVersion(version => {
                Debug.Log(version.Version);
                Assert.NotNull(version);
            }, error => {
                Debug.Log(error.ToString());
            });
            yield return null;
        }

       
       
    }

}
