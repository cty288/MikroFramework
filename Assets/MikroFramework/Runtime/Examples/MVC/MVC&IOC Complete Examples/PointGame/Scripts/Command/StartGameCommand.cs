using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.Examples;
using UnityEngine;

namespace MikroFramework
{
    public class StartGameCommand : AbstractCommand {

        protected override void OnExecute(params object[] parameters) {
            IGameModel gameModel = new GameModel();
            gameModel.KillCount.Value = 0;
            gameModel.BestScore.Value = 0;

            this.SendEvent<GameStartEvent>();
        }
    }
}
