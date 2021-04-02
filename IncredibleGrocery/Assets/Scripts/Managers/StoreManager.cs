using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    public ProductScriptableObject[] products;
    public int Money { private set; get; }
    public AudioClip moneyClip;
    protected override void Initialise()
    {
        LoadPlayerSave();
    }
    void LoadPlayerSave()
    {
        PlayerSave save = SaveSystem.LoadPlayer();
        Money = save.money;
    }
    public void AddMoney(int money)
    {
        if (money>0) 
        {
            Money += money;
            AudioManager.instance.PlaySound(moneyClip);
        }
        SavePlayer();
    }
    void SavePlayer()
    {
        PlayerSave save = new PlayerSave();
        save.money = Money;
        SaveSystem.SavePlayer(save);
    }
}
