using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Architecture;

namespace MikroFramework.Examples
{
    public class MissCommand:AbstractCommand
    {
        protected override void OnExecute(params object[] parameters) {
            IGameModel gameModel = this.GetModel<IGameModel>();

            if (gameModel.Life.Value > 0) {
                gameModel.Life.Value--;
            }
            else {
                this.SendEvent<OnMissEvent>();
            }

            
        }
    }
}
