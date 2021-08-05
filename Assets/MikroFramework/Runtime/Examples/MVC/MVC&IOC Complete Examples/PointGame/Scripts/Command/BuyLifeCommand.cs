using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikroFramework.Architecture;

namespace MikroFramework.Examples
{
    public class BuyLifeCommand:AbstractCommand
    {
        protected override void OnExecute(params object[] parameters) {
            IGameModel gameModel = this.GetModel<IGameModel>();
            gameModel.Gold.Value--;
            gameModel.Life.Value++;
        }
    }
}
