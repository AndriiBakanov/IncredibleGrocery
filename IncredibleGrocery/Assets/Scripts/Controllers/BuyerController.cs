using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyerController : MonoBehaviour
{
    public float speed;
    public Transform storeEntranceTransform;
    public Transform cashRegisterTransform;
    Vector3 destination;
    public Bubble bubble;
    bool isWaiting;
    public SpriteRenderer spriteRenderer;
    Animator animator;
    Action planedAction;
    private void Start()
    {
        animator = GetComponent<Animator>();
        isWaiting = false;
        animator.SetBool("IsWaiting", isWaiting);
        transform.position = storeEntranceTransform.position;
        destination = cashRegisterTransform.position;
        planedAction = new Action(() => StartCoroutine(StartShopping()));
    }
    private void Update()
    {
        if (!isWaiting) 
        {
                if (MoveToDestination())
                {
                    planedAction();
                }
        }
    }
    public IEnumerator GoShopping() 
    {
        bubble.CloseBubble();
        isWaiting = true;
        planedAction = new Action(() => StartCoroutine(StartShopping()));
        animator.SetBool("IsWaiting", isWaiting);
        yield return new WaitForSeconds(1);
        spriteRenderer.flipX = false;
        destination = cashRegisterTransform.position;
        isWaiting = false;
        animator.SetBool("IsWaiting", isWaiting);
    }
    public void GoHome(bool isCorrectOrder)
    {
        bubble.ShowEmotion(isCorrectOrder);
        destination = storeEntranceTransform.position;
        isWaiting = false;
        animator.SetBool("IsWaiting", isWaiting);
        spriteRenderer.flipX = true;
        planedAction = new Action(() => StartCoroutine(GoShopping()));
    }
    public IEnumerator StartShopping() 
    {
        destination = storeEntranceTransform.position;
        isWaiting = true;
        animator.SetBool("IsWaiting", isWaiting);
        List<ProductScriptableObject> products=GetWishedProducts();
        bubble.ShowProducts(products);
        yield return new WaitForSeconds(5);
        bubble.CloseBubble();
        GameManager.instance.PlaceOrder(products);
    }

    private List<ProductScriptableObject> GetWishedProducts()
    {
        int productCount = Random.Range(1, 4);
        List<ProductScriptableObject> products = new List<ProductScriptableObject>();
        ProductScriptableObject[] storeProducts = StoreManager.instance.products;
        if (storeProducts.Length <= productCount) 
        {
            return new List<ProductScriptableObject>(storeProducts);
        }
        while (productCount > products.Count) 
        {
            ProductScriptableObject selectedProduct = storeProducts[Random.Range(0, storeProducts.Length - 1)];
            if (!products.Contains(selectedProduct)) 
            {
                products.Add(selectedProduct);
            }
        }
        return products;
    }

    bool MoveToDestination() 
    {
        Vector3 direction = destination - transform.position;
        if (direction.magnitude > 0.1)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            return false;
        }
        return true;
    }
}
