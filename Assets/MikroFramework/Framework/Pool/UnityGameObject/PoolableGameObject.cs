using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MikroFramework.Pool
{
    public abstract class PoolableGameObject:MonoBehaviour {
        public abstract void OnRecycled();
    }
}
