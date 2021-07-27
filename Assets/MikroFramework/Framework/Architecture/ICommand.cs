using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Architecture
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture, ICanGetSystem, ICanGetModel,
    ICanGetUtility, ICanSendEvent, ICanSendCommand{
        void Execute(params object[] parameters);
    }
}
