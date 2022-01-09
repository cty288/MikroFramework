using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using MikroFramework.Pool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayerCommand : AbstractCommand<HurtPlayerCommand> {

    private int damage = 1;

    public static HurtPlayerCommand Allocate(int damage=1) {
        HurtPlayerCommand command = SafeObjectPool<HurtPlayerCommand>.Singleton.Allocate();
        command.damage = damage;
        return command;
    }

    protected override void OnExecute() {
        IPlayerModel playerModel = this.GetModel<IPlayerModel>();
        playerModel.HP.Value -= damage;

        if (playerModel.HP.Value <= 0) {
            SceneManager.LoadScene("GameOver");
        }
    }
}
