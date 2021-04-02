using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
    public GameObject productItem;
    public GameObject productItemsGrid;
    List<ProductItemUI> productItemUIs;
    public Sprite correctMark;
    public Sprite uncorrectMark;
    public Sprite positiveEmotion;
    public Sprite negativeEmotion;
    public AudioClip appeared;
    public AudioClip disappeared;
    public void ShowProducts(List<ProductScriptableObject> products)
    {
        AudioManager.instance.PlaySound(appeared);
        gameObject.SetActive(true);
        productItemUIs = new List<ProductItemUI>();
        foreach (ProductScriptableObject product in products)
        {
            GameObject gameObject = Instantiate(productItem);
            ProductItemUI productUI = gameObject.GetComponent<ProductItemUI>();
            productUI.SetProduct(product);
            gameObject.transform.SetParent(productItemsGrid.transform);
            gameObject.transform.localScale = productItemsGrid.transform.localScale;
            productItemUIs.Add(productUI);
        }
    }
    public void CloseBubble()
    {
        AudioManager.instance.PlaySound(disappeared);
        foreach (ProductItemUI c in productItemsGrid.GetComponentsInChildren<ProductItemUI>())
        {
            Destroy(c.gameObject);
        }
        gameObject.SetActive(false);
    }
    public void ShowProductChoiseResult(int index, bool isCorrect)
    {
        if (isCorrect)
        {
            productItemUIs[index].SetResult(correctMark);
        }
        else 
        {
            productItemUIs[index].SetResult(uncorrectMark);
        } 
    }
    public void ShowEmotion(bool positive)
    {
        
        GameObject gameObject = Instantiate(productItem);
        ProductItemUI productUI = gameObject.GetComponent<ProductItemUI>();
        gameObject.transform.SetParent(productItemsGrid.transform);
        gameObject.transform.localScale = productItemsGrid.transform.localScale;
        if (positive)
        {
            productUI.productImage.sprite = positiveEmotion;
        }
        else 
        {
            productUI.productImage.sprite = negativeEmotion;
        }
        productItemUIs.Add(productUI);
        this.gameObject.SetActive(true);
    }
}
