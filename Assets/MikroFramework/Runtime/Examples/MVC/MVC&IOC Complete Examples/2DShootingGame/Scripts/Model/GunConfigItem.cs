using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunConfigItem{
    public string Name { get; set; }
    public int BulletMaxCount { get; set; }
    public float Attack { get; set; }
    public float Frequency { get; set; }
    public float ShootDistance { get; set; }
    public bool NeedBullet { get; set; }
    public float ReloadSeconds { get; set; }
    public string Description { get; set; }

    public GunConfigItem(string name, int bulletMaxCount, float attack, float freq, float shootDistance,
        bool needBullet, float reloadSeconds, string description) {
        this.Name = name;
        this.BulletMaxCount = bulletMaxCount;
        this.Attack = attack;
        this.Frequency = freq;
        this.ShootDistance = shootDistance;
        this.NeedBullet = needBullet;
        this.ReloadSeconds = reloadSeconds;
        this.Description = description;
    }

}
