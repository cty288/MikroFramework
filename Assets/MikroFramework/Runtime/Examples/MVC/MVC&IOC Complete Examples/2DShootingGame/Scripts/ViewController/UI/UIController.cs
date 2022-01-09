using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework;
using MikroFramework.Architecture;
using MikroFramework.Event;
using UnityEngine;

public class UIController : AbstractMikroController<ShootingEditor2D> {
    private IPlayerModel playerModel;
    private IStatSystem statSystem;
    private IGunSystem gunSystem;

    private int maxBulletCount;
    private void Awake() {
        playerModel = this.GetModel<IPlayerModel>();
        statSystem = this.GetSystem<IStatSystem>();
        gunSystem = this.GetSystem<IGunSystem>();

        maxBulletCount = this.SendQuery(new MaxBulletCountQuery(gunSystem.CurrentGun.Name.Value));
        this.RegisterEvent<OnCurrentGunChanged>(e => {
            maxBulletCount = this.SendQuery(new MaxBulletCountQuery(e.Name));
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private readonly Lazy<GUIStyle> labelStyle = new Lazy<GUIStyle>(
        () => {
            return new GUIStyle(GUI.skin.label) {
                fontSize = 40
            };
        });

    private void OnGUI() {
        GUI.Label(new Rect(10,10,300,100),$"Life: {playerModel.HP.Value}/3",labelStyle.Value);

        GUI.Label(new Rect(10, 60, 400, 100), $"Bullet in Gun: {gunSystem.CurrentGun.BulletCountInGun.Value} / {maxBulletCount}", labelStyle.Value);

        GUI.Label(new Rect(10,110,400,100),$"Bullet outside Gun: {gunSystem.CurrentGun.BulletCountOutGun.Value}",
            labelStyle.Value);

        GUI.Label(new Rect(10, 160, 300, 100), $"Gun Name: {gunSystem.CurrentGun.BulletCountOutGun.Value}",
            labelStyle.Value);

        GUI.Label(new Rect(10, 210, 300, 100), $"Gun State: {gunSystem.CurrentGun.State.Value}",
            labelStyle.Value);

        GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"Kill number: {statSystem.KillCount.Value}",
            labelStyle.Value);
    }

    private void OnDestroy() {
        playerModel = null;
        statSystem = null;
        gunSystem = null;
    }
}
