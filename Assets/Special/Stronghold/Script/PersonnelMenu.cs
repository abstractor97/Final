using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnelMenu : MonoBehaviour
{

    private RectTransform cacheRt;

    public RectTransform strengthen;

    public RectTransform hire;

    public RectTransform manager;


    public void OpenStrengthen()
    {
        cacheRt.GetComponent<CanvasGroup>().alpha = 0;
        strengthen.GetComponent<CanvasGroup>().alpha = 1;
        cacheRt = strengthen;

    }

    public void OpenHire()
    {
        cacheRt.GetComponent<CanvasGroup>().alpha = 0;
        hire.GetComponent<CanvasGroup>().alpha = 1;
        cacheRt = hire;
    }

    public void OpenManager()
    {
        cacheRt.GetComponent<CanvasGroup>().alpha = 0;
        manager.GetComponent<CanvasGroup>().alpha = 1;
        cacheRt = manager;
    }

    private void Start()
    {
        InitLv();
    }

    #region 招募层

    public RectTransform peopleList;

    public GameObject peopleItem;
    /// <summary>
    /// 被选中的提示框
    /// </summary>
    public RectTransform selectProfile;

    public ProfileDetails profileDetails;

    private int rtlv;

    public void ShowRecruit()
    {
        if (rtlv==GameTeamController.GameData.rtLv)
        {

        }
        else
        {
            foreach (RectTransform pl in peopleList)
            {
                Destroy(pl.gameObject);
            }
            
            foreach (People1 p in GameTeamController.GameData.waitRecruit)
            {
                GameObject pi=  GameObject.Instantiate<GameObject>(peopleItem);
                ProfileItem pfi= pi.GetComponent<ProfileItem>();
                pfi.click = delegate (ProfileItem pl1)
                {
                    if (GameTeamController.GameData.CheckWaitTeam())
                    {
                        PublicManager.ShowArlog("是否招募", delegate (Pass pass)
                        {
                            if (pass == Pass.yes)
                            {
                                GameTeamController.GameData.AddWaitTeam(pl1.people);
                                Destroy(pl1.gameObject);
                            }
                        });
                    }
                    else
                    {
                        PublicManager.ShowTips("已满员");
                    }
                };
                pfi.enter = delegate (Transform tr)
                {
                    selectProfile.SetParent(tr, false);
                    profileDetails.InitDetails(tr.GetComponent<ProfileItem>().people);
                };
                //todo 随机化属性技能
                pfi.InitProfile(p);
                pi.transform.SetParent(peopleList,false);
            }
            rtlv = GameTeamController.GameData.rtLv;

        }

    }


    #endregion

    #region 训练层相关操作And数据

    private Dictionary<Attribute, DemandRes> demandLevel;


    public DigitText strText;

    public DigitText agiText;
    public DigitText intText;
    public DigitText endText;



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
        if (demandRes.baseCoin > 0)
        {
            Inquiry.Add(GameResourceController.Type.coin, demandRes.baseCoin * (demandRes.lv + demandRes.growthRate));
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
