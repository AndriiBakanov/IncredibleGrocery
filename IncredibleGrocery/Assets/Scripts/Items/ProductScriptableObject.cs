using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "GameItems/Product", order = 1)]
public class ProductScriptableObject : ScriptableObject
{
    public string productName;
    public Sprite image;
    public static bool operator ==(ProductScriptableObject p1,ProductScriptableObject p2) 
    {
        return p1.productName == p2.productName;
    }
    public static bool operator !=(ProductScriptableObject p1, ProductScriptableObject p2)
    {
        return p1.productName != p2.productName;
    }
    public override int GetHashCode()
    {
        return productName.GetHashCode()+image.GetHashCode();
    }
    public override bool Equals(object other)
    {
        return productName.Equals((other as ProductScriptableObject).productName);
    }
}
