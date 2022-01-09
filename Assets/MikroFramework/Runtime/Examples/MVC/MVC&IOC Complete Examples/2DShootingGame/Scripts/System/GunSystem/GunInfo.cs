using System;
using System.Collections;
using System.Collections.Generic;
using MikroFramework.BindableProperty;
using UnityEngine;

public class GunInfo {
    public enum GunState
    {
        Idle,
        Shooting,
        Reload,
        EmptyBullet,
        Cooldown
    }

    

    public BindableProperty<int> BulletCountInGun;

    public BindableProperty<string> Name;

    public BindableProperty<GunState> State;

    public BindableProperty<int> BulletCountOutGun;
}
