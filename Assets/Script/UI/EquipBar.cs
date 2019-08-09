using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipBar : MonoBehaviour
{
    public Item[] equips = new Item[6];

    public GameObject item;

    public GameObject lattice;

    public float interval;
    public Sprite relieveSprite;
    private GridView grid;
    // Start is called before the first frame update
    void Start()
    {
        grid= gameObject.AddComponent<GridView>();
        grid.item = item;
        grid.drawLattice = true;
        grid.lattice = lattice;
        grid.latticeNum = 6;
        grid.interval = interval;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Equip(Item item)
    {

        for (int i = 0; i < equips.Length; i++)
        {
            if (equips[i] == null)
            {
                equips[i] = item;
                grid.AddItem(item, View);
                return true;
            }
        }
        return false;
    }

    private void View(GameObject ui, Item item)
    {
        BagItem bagItem = ui.GetComponent<BagItem>();
        bagItem.deleteAction = Relieve;
        bagItem.icon.sprite = item.lowSprite;
        bagItem.itemName.text = item.name;
        bagItem.note.text = item.describe;
        bagItem.num.text = "1";
        bagItem.weight.text = item.weight.ToString()+"kg";
        bagItem.delete.gameObject.GetComponent<Image>().sprite = relieveSprite;
    }

    public void Relieve(int sel)
    {
        FindObjectOfType<PlayerManager>().bags[0].AddItem(new Bag.ItemInBag(equips[sel], 1));
        equips[sel] = null;
        grid.Remove(sel);       
    }

}
