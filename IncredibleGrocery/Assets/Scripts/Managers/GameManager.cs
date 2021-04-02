using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    List<ProductScriptableObject> orderProducts;
    public BuyerController buyerController;
    public Bubble cashierBubble;
    public void SetPause(bool isPause) 
    {
        Time.timeScale=isPause? 0 : 1;
    }
    public void PlaceOrder(List<ProductScriptableObject> orderProducts) 
    {
        this.orderProducts = orderProducts;
        UIManager.instance.OpenProductsMenuUI(orderProducts.Count);
    }
    public void SellProducts(List<ProductScriptableObject> products) 
    {
        cashierBubble.ShowProducts(products);
        StartCoroutine(ProductsCheck(products));
    }
    public IEnumerator ProductsCheck(List<ProductScriptableObject> products) 
    {
        int money=0;
        bool isCorrect = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < products.Count; i++) 
        {
            if (orderProducts.Contains(products[i]))
            {
                money += 10;
                cashierBubble.ShowProductChoiseResult(i, true);
            }
            else
            {
                isCorrect = false;
                cashierBubble.ShowProductChoiseResult(i, false);
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (isCorrect)
        {
            money *= 2;
        }
        yield return new WaitForSeconds(1);
        cashierBubble.CloseBubble();
        buyerController.GoHome(isCorrect);
        StoreManager.instance.AddMoney(money);
        UIManager.instance.UpdateMoneyText();
    }
}
