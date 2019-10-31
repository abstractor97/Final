using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CastleMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

      //  transform.DOScale(transform.position,1f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    #region 训练层相关操作And数据

    private Dictionary<Attribute, DemandRes> demandLevel;


    public void OpenStrengthen()
    {

    }

    public void OpenHire()
    {

    }

    private void InitLv()
    {
        int i = Enum.GetNames(typeof(Attribute)).Length;
        demandLevel = new Dictionary<Attribute, DemandRes>(i);
        foreach (Attribute type in Enum.GetValues(typeof(Type)))
        {
            demandLevel.Add(type, new DemandRes());
        }
    }

    public void UpLevel(int attributeid)
    {
        Attribute attribute = (Attribute)attributeid;
        Dictionary<GameResourceController.Type, int> Inquiry = new Dictionary<GameResourceController.Type, int>();
        DemandRes demandRes = demandLevel[attribute];
        if (demandRes.baseCoin>0)
        {
            Inquiry.Add(GameResourceController.Type.coin, demandRes.baseCoin*(demandRes.lv+ demandRes.growthRate));
        }
        Inquiry.Add(GameResourceController.Type.food, demandRes.baseFood * (demandRes.lv + demandRes.growthRate));
        Inquiry.Add(GameResourceController.Type.mineral, demandRes.baseMin * (demandRes.lv + demandRes.growthRate));
        Inquiry.Add(GameResourceController.Type.crystal, demandRes.baseCyr * (demandRes.lv + demandRes.growthRate));
        if (GameResourceController.GameData.Check(Inquiry))
        {
            foreach (var inq in Inquiry)
            {
                GameResourceController.GameData.ResourceAdd(inq.Key, inq.Value);
            }
            demandRes.lv++;
            demandRes.growthRate += demandRes.lv;
            //todo 显示更新 
        }
        else
        {
            PublicManager.ShowTips("资源不足");
        }
    }

    private class DemandRes
    {
        public int baseCoin = 400;

        public int baseFood = 5;

        public int baseMin = 0;

        public int baseCyr = 0;

        public int growthRate = 1;

        public int lv = 1;

    }

    #endregion


    #region 牢房
    private List<People1> captives = new List<People1>();

    private int ration=0;

    private int selectCaptive;

    public Text cellRationText;

    public RectTransform captivesList;

    public GameObject captiveItem;


    public void AddCaptive(People1 people1)
    {
        captives.Add(people1);
        GameObject item= GameObject.Instantiate<GameObject>(captiveItem);
        item.transform.SetParent(captivesList, false);
        item.name = (captives.Count - 1).ToString();
        ListItem li= item.AddComponent<ListItem>();
        li.leftAction = delegate (int i) {
            selectCaptive = i;
        };
        ration += 1;
    }

    public void ChangeRation(int num)
    {
        if (ration==0&&num<0)
        {
            //todo 文字变红动画
        }
        else
        {
            ration += num;
            cellRationText.text = ration.ToString();
        }
    }

    public void LockRation()
    {

    }

    public void CellRelease()
    {
        PublicManager.ShowArlog("释放", delegate (Pass pass) {
            if (pass==Pass.yes)
            {
                captives.Remove(captives[selectCaptive]);
                Destroy(captivesList.GetChild(selectCaptive).gameObject);
            }
        });
    
    }


    #endregion

    public enum Attribute
    {
        str,
        agi,
        mint,
        end,

    }
}
