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

    public DigitText strText;

    public DigitText agiText;
    public DigitText intText;
    public DigitText endText;

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
            switch (attribute)
            {
                case Attribute.str:
                    strText.text = (int.Parse(strText.text) + 1).ToString();
                    break;
                case Attribute.agi:
                    agiText.text = (int.Parse(agiText.text) + 1).ToString();
                    break;
                case Attribute.mint:
                    intText.text = (int.Parse(intText.text) + 1).ToString();
                    break;
                case Attribute.end:
                    endText.text = (int.Parse(endText.text) + 1).ToString();
                    break;
            }
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


    public enum Attribute
    {
        str,
        agi,
        mint,
        end,

    }
}
