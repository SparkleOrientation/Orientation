using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item
{
    public enum ItemType
    {
        Dague,
        Indice,
        Bois,
    } 

    public ItemType itemType;
    public int amount;

    public Item(ItemType itemType,int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
    }

    public Sprite GetSprite() 
    {
        switch (itemType)
        {
            default:
            case ItemType.Bois: return ItemAssets.Instance.boisSprite;
            case ItemType.Dague: return ItemAssets.Instance.dagueSprite;
            case ItemType.Indice: return ItemAssets.Instance.indiceSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.Dague:
                return false;
            default:
            case ItemType.Bois:
            case ItemType.Indice:
                return true;
        }
    }
}
