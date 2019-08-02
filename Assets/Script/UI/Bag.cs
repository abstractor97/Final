using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public GameObject item;


    public List<ItemInBag> items;

    public float maxWeight;

    public bool playerBag;

    public bool isBag;//todo


    [Serializable]
    public class ItemInBag
    {
        public Item item;

        public int num;

    }


    private GridView grid;
    private float weight;
    private int actionSel;

    // Start is called before the first frame update
    void Start()
    {
        grid= gameObject.AddComponent<GridView>();
        grid.item = item;
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemInBag item)
    {
        items.Add(item);
        grid.AddItem(item, View);
    }

    private void View(GameObject ui, ItemInBag item)
    {
        BagItem bagItem = ui.AddComponent<BagItem>();
        bagItem.deleteAction = DeleteOrMove;
        bagItem.clickAction = Use;
        bagItem.icon.sprite = item.item.lowSprite;
        bagItem.itemName.text = item.item.name;
        bagItem.note.text = item.item.describe;
        bagItem.num.text = item.num.ToString();
        bagItem.weight.text = item.item.weight.ToString();

    }

    public void UpBag()
    {
        grid.UpData(items.ToArray(), View);
    }


    public void Use(int sel)
    {
        actionSel = sel;
        if (playerBag)
        {
            switch (items[sel].item.type)
            {
                case Item.ItemType.available:
                    FindObjectOfType<PublicManager>().ShowArlog("是否使用" + items[sel].item.name, UseCallBack);
                    break;
                case Item.ItemType.material:
                    break;
                case Item.ItemType.equip:
                    FindObjectOfType<PublicManager>().ShowArlog("装备" + items[sel].item.name, EquipCallBack);
                    break;
            }
        }
      
       
      
    }

    private void UseCallBack(Ardialog.Pass pass)
    {
        if (pass==Ardialog.Pass.yes)
        {

        }
    }

    private void EquipCallBack(Ardialog.Pass pass)
    {
        if (pass == Ardialog.Pass.yes)
        {

        }
    }

    public void DeleteOrMove(int sel)
    {
        actionSel = sel;
        if (playerBag)
        {
            FindObjectOfType<PublicManager>().ShowSelectNum(items[sel].num, 1, DeleteNum);
        }
        else
        {
            foreach (var bag in FindObjectsOfType<Bag>())
            {
                if (bag.playerBag)
                {
                    bag.AddItem(items[sel]);
                }
            }

        }
    }

    public void DeleteNum(int num)
    {
        if (items[actionSel].num== num)
        {
            items.RemoveAt(actionSel);
        }
        else
        {
            items[actionSel].num -= num;
        }
        UpBag();
    }
}
