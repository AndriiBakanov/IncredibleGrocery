using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductItemUI : MonoBehaviour
{
    ProductScriptableObject product;
    public Image productImage;
    public Image resultImage;
    Func<ProductScriptableObject,bool> addEvent;
    Func<ProductScriptableObject, bool> removeEvent;
    bool IsSelected = false;
    public Sprite selectedSprite;
    public void SetResult(Sprite sprite) 
    {
        resultImage.gameObject.SetActive(true);
        resultImage.sprite = sprite;
        Color color = productImage.color;
        color.a = 0.3f;
        productImage.color = color;
    }
    public void SetEvents(Func<ProductScriptableObject, bool> addEvent, Func<ProductScriptableObject, bool> removeEvent) 
    {
        Button.ButtonClickedEvent buttonClickedEvent = new Button.ButtonClickedEvent();
        buttonClickedEvent.AddListener(ButtonClick);
        Button button = GetComponent<Button>();
        button.enabled = true;
        button.onClick = buttonClickedEvent;
        this.addEvent = addEvent;
        this.removeEvent = removeEvent;
    }
    public void SetProduct(ProductScriptableObject product) 
    {
        this.product = product;
        productImage.sprite = product.image;
    }
    public void Reset()
    {
        resultImage.gameObject.SetActive(false);
        Color color = productImage.color;
        color.a = 1;
        IsSelected = false;
        productImage.color = color;
    }
    void ButtonClick() 
    {
        if (IsSelected)
        {
            if (removeEvent(product))
            {
                Reset();
                IsSelected = false;
            }
        }
        else 
        {
            if (addEvent(product))
            {
                SetResult(selectedSprite);
                IsSelected = true;
            }
        }
    }
}
