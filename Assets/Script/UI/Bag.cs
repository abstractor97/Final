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

    public GameObject context;

    public bool playerBag;
    /// <summary>
    /// 可交互
    /// </summary>
    public bool Interactive = true;

    public bool drawLattice;

    public bool isBag;//todo

    public Sprite addSprite;
    public Sprite deleteSprite;

    [Serializable]
    public class ItemInBag
    {
        public ItemInBag() {
        }
        public ItemInBag(Item item,int num) {
            this.item = item;
            this.num = num;
        }

        public Item item;

        public int num;

    }


    private GridView grid;
    private float weight;
    private int actionSel;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(item.GetComponent<RectTransform>().sizeDelta.x+8, gameObject.GetComponent<RectTransform>().sizeDelta.y); 
        grid= context.AddComponent<GridView>();
        grid.item = item;
        grid.drawLattice = drawLattice;
        if (items!=null)
        {
            grid.AddData(items.ToArray(), View);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemInBag item)
    {
       // ItemInBag inBag = items.Find(i => i.item.name.Equals(item.item.name));
        int index= items.FindIndex(i => i.item.name.Equals(item.item.name));
        if (index != -1)
        {
            items[index].num += item.num;
            grid.UpItem(index, item, View);
        }
        else
        {
            items.Add(item);
            grid.AddItem(item, View);
        }
        weight += item.item.weight*item.num;
    }

    private void View(GameObject ui, ItemInBag item)
    {
        BagItem bagItem = ui.GetComponent<BagItem>();
        bagItem.deleteAction = DeleteOrMove;
        bagItem.clickAction = Use;
        bagItem.icon.sprite = item.item.lowSprite;
        bagItem.itemName.text = item.item.name;
        bagItem.note.text = item.item.describe;
        bagItem.num.text = item.num.ToString();
        bagItem.weight.text = item.item.weight.ToString()+"kg";      
        if (!Interactive)
        {
            bagItem.delete.gameObject.GetComponent<Image>().sprite = null;
        }
        else
        {
            if (!playerBag)
            {
                bagItem.delete.gameObject.GetComponent<Image>().sprite = addSprite;
            }
            else
            {
                bagItem.delete.gameObject.GetComponent<Image>().sprite = deleteSprite;
            }
        }
    }

    public void UpBag()
    {
        weight = 0;
        foreach (var item in items)
        {
           weight+= item.item.weight*item.num;
        }
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
                    PublicManager.ShowArlog("是否使用" + items[sel].item.name, delegate (Pass pass)
                    {
                        if (pass == Pass.yes)
                        {
                            AttrToInvoke(items[sel].item);
                        }
                    });
                    break;
                case Item.ItemType.material:
                    break;
                case Item.ItemType.equip:
                    PublicManager.ShowArlog("装备" + items[sel].item.name, delegate (Pass pass)
                    {
                        if (pass == Pass.yes)
                        {
                            AttrToInvoke(items[sel].item);
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
            PublicManager.ShowSelectNum(items[sel].num, 1, DeleteNum);
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

    private void AttrToInvoke(Item item)
    {
        foreach (var use in item.use)
        {
            switch (use.type)
            {
                case Item.UseType.recovery:

                    FindObjectOfType<PlayerManager>().Attribute(use.state,use.degree);

                    break;
                case Item.UseType.buff:
                    FindObjectOfType<PlayerManager>().AddBuff(use.buff);

                    break;
             
                default:
                    FindObjectOfType<PlayerManager>().Equip(item);
                    break;
            }
        }
       
    }
}
