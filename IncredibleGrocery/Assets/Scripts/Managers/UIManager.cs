using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public ProductsMenuUI productMenuUI;
    public GameObject settingsUI;
    public Text moneyText;
    public AudioClip buttonClick;
    private void Start()
    {
        moneyText.text = "$ " + StoreManager.instance.Money;
    }
    
    public void OpenSettings() 
    {
        AudioManager.instance.PlaySound(buttonClick);
        settingsUI.SetActive(true);
        GameManager.instance.SetPause(true);
    }
    public void CloseSettings() 
    {
        AudioManager.instance.PlaySound(buttonClick);
        settingsUI.SetActive(false);
    }
    public void OpenProductsMenuUI(int productsCount) 
    {
        productMenuUI.gameObject.SetActive(true);
        productMenuUI.ChooseProducts(productsCount);
    }
    public void UpdateMoneyText() 
    {
        moneyText.text = "$ "+StoreManager.instance.Money;
    }
}
