using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductsMenuUI : MonoBehaviour
{
    public GameObject productItem;
    public GameObject productItemsGrid;
    List<ProductItemUI> productItemUIs;
    public Button confirmButton;
    List<ProductScriptableObject> neededProducts;
    public AudioClip productSelect;
    public AudioClip buttonClick;
    Animator animator;
    int neededProductsCount;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        LoadProductOnGrid();
        Button.ButtonClickedEvent buttonClickedEvent= new Button.ButtonClickedEvent();
        buttonClickedEvent.AddListener(SellProducts);
        confirmButton.onClick = buttonClickedEvent;
    }
    void LoadProductOnGrid()
    {
        productItemUIs = new List<ProductItemUI>();
        foreach (ProductScriptableObject product in StoreManager.instance.products)
        {
            GameObject gameObject = Instantiate(productItem);
            ProductItemUI productUI= gameObject.GetComponent<ProductItemUI>();
            productUI.SetProduct(product);
            productUI.SetEvents(AddSelectedProduct, RemoveSelectedProduct);
            gameObject.transform.SetParent(productItemsGrid.transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            productItemUIs.Add(productUI);
        }
    }
    private void OnEnable()
    {
        animator.SetBool("IsHiden", false);
    }
    public void Reset()
    {
        foreach (ProductItemUI itemUI in productItemUIs) 
        {
            itemUI.Reset();
        }
    }
    public void ChooseProducts(int productsCount) 
    {
        neededProducts = new List<ProductScriptableObject>(productsCount);
        neededProductsCount = productsCount;
        ChangeConfirmButtonState(false);
    }
    public bool AddSelectedProduct(ProductScriptableObject product) 
    {
        AudioManager.instance.PlaySound(productSelect);
        if (neededProductsCount > neededProducts.Count) 
        {
            neededProducts.Add(product);
            if (neededProducts.Count == neededProductsCount) 
            {
                ChangeConfirmButtonState(true);
            }
            return true;
        }
        return false;
    }
    public void ChangeConfirmButtonState(bool isEnabled) 
    {
        confirmButton.enabled = isEnabled;
        if (isEnabled)
        {
            ColorBlock colorBlock = confirmButton.colors;
            colorBlock.normalColor = new Color(0, 1, 0, 1f);
            confirmButton.colors = colorBlock;
        }
        else
        {
            ColorBlock colorBlock = confirmButton.colors;
            colorBlock.normalColor = new Color(0, 1, 0, 0.5f);
            confirmButton.colors = colorBlock;
        }
    }
    public bool RemoveSelectedProduct(ProductScriptableObject product) 
    {
        AudioManager.instance.PlaySound(productSelect);
        ChangeConfirmButtonState(false);
        return neededProducts.Remove(product);
    }
    void SellProducts() 
    {
        AudioManager.instance.PlaySound(buttonClick);
        GameManager.instance.SellProducts(neededProducts);
        animator.SetBool("IsHiden", true);
    }
    public void Close() 
    {
        Reset();
        gameObject.SetActive(false);
    }
}
