using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public GameObject item;


    public List<ItemInBag> items;

    public float maxWeight;

    public bool playerBag;

    public bool isBag;//todo

    public Sprite addSprite;

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
        if (!playerBag)
        {
            bagItem.delete.gameObject.GetComponent<Image>().sprite = addSprite;
        }

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
                    FindObjectOfType<PublicManager>().ShowArlog("是否使用" + items[sel].item.name, delegate (Ardialog.Pass pass)
                    {
                        if (pass == Ardialog.Pass.yes)
                        {
                            AttrToInvoke(items[sel].item.use);
                        }
                    });
                    break;
                case Item.ItemType.material:
                    break;
                case Item.ItemType.equip:
                    FindObjectOfType<PublicManager>().ShowArlog("装备" + items[sel].item.name, delegate (Ardialog.Pass pass)
                    {
                        if (pass == Ardialog.Pass.yes)
                        {
                            AttrToInvoke(items[sel].item.use);
                        }
                    });
                    break;
            }
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
    /// <summary>
    ///  负载的速度乘数 y=\frac{\left(2-\frac{xx}{mm})}/{2}{y>0}{x>0}
    /// </summary>
    /// <returns>0~1</returns>
    public float LoadSpeed()
    {
        return (2 - (weight * weight) / (maxWeight * maxWeight)) / 2;
    }

    private void AttrToInvoke(Item.Use[] uses)
    {
        foreach (var use in uses)
        {
            switch (use.type)
            {
                case Item.UseType.recovery:

                    FindObjectOfType<PlayerManager>().ChangeState(use.state,use.degree,use.totalTime);

                    break;
                case Item.UseType.buff:
                    break;
                case Item.UseType.arm:
                    break;
                case Item.UseType.hat:
                    break;
                case Item.UseType.clothes_up:
                    break;
                case Item.UseType.clothes_down:
                    break;
                case Item.UseType.shoes:
                    break;
                default:
                    break;
            }
        }
       
    }
}
