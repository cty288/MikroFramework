using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikroFramework.Architecture
{
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture architectureModel;

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return architectureModel;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            this.architectureModel = architecture;
        }


        void ICommand.Execute(params object[] parameters) {
            OnExecute(parameters);
        }

        /// <summary>
        /// Execute this command
        /// </summary>
        /// <param name="parameters"></param>
        protected abstract void OnExecute(params object[] parameters);

       
    }
}
