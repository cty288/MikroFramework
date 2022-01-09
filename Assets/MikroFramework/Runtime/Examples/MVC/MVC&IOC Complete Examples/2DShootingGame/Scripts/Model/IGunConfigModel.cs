using System.Collections;
using System.Collections.Generic;
using MikroFramework.Architecture;
using UnityEngine;

public interface IGunConfigModel : IModel {
    GunConfigItem GetItemByName(string name);

}

public class GunConfigModel : AbstractModel, IGunConfigModel {
    private Dictionary<string, GunConfigItem> items = new Dictionary<string, GunConfigItem>() {
        {"Pistol", new GunConfigItem("Pistol", 7, 1, 1, 0.5f, false, 3, "Normal")},
        {"Submachine", new GunConfigItem("Submachine", 30, 1, 6, 0.34f, true, 3, "None")},
        {"Rifle", new GunConfigItem("Rifle", 50, 3, 3, 1f, true, 1, "Has some recoil")},
        {"Sniper", new GunConfigItem("Sniper", 12, 6, 1, 1f, true, 5, "Aiming + High recoil")},
        {"RPG", new GunConfigItem("RPG", 1, 5, 1, 1, true, 4, "Tracing + booming")},
        {"Shotgun", new GunConfigItem("Shotgun", 1, 1, 1, 0.5f, true, 1, "Shoot 1-12 bullets a time")}
    };
    protected override void OnInit() {
        
    }

    public GunConfigItem GetItemByName(string name) {
        return items[name];
    }
}
