using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonnelMenu : MonoBehaviour
{

    private RectTransform cacheRt;
    [Tooltip("按钮组")]
    public RectTransform buttons;

    public RectTransform hire;

    public RectTransform strengthen;

    public RectTransform manager;


    public void OpenStrengthen()
    {
        Hide(cacheRt);
        Show(strengthen);
        cacheRt = strengthen;
        Hide(buttons);

    }

    public void OpenHire()
    {
        Hide(cacheRt);
        Show(hire);
        cacheRt = hire;
        ShowRecruit();
        Hide(buttons);
    }

    public void OpenManager()
    {
        Hide(cacheRt);
        Show(manager);
        cacheRt = manager;
        ShowManager();
        Hide(buttons);
    }

    public void BackMain()
    {
        Hide(cacheRt);
        Show(buttons);
    }

    private void Show(RectTransform ui)
    {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
    }

    private void Hide(RectTransform ui)
    {
        CanvasGroup group = ui.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    private void Start()
    {
        Hide(strengthen);
        Hide(hire);
        Hide(manager);
        cacheRt = hire;
        InitLv();
        selectProfile.gameObject.SetActive(false);
    }

    #region 招募层
    [Header("招募层")]
    public RectTransform peopleList;

    public GameObject peopleItem;
    /// <summary>
    /// 被选中的提示框
    /// </summary>
    public RectTransform selectProfile;

    public ProfileDetails profileDetails;

    private void ShowRecruit()
    {
        if (!GameTeamController.GameData.recruitRefresh)
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
                    selectProfile.transform.position=tr.position;
                    profileDetails.InitDetails(tr.GetComponent<ProfileItem>().people);
                };
                //todo 随机化属性技能
                pfi.InitProfile(p);
                pi.transform.SetParent(peopleList,false);
            }
            if (peopleList.childCount==0)
            {
                selectProfile.gameObject.SetActive(false);
            }
            else
            {
                selectProfile.gameObject.SetActive(true);
                selectProfile.GetComponent<RectTransform>().sizeDelta = peopleList.GetChild(0).GetComponent<RectTransform>().sizeDelta;
            }
            GameTeamController.GameData.recruitRefresh = false;
        }

    }




    #endregion

    #region 训练层相关操作And数据


    [Header("训练层")]
    public LevelBar strBar;
    public LevelBar agiBar;
    public LevelBar intBar;
    public LevelBar endBar;

    private Dictionary<Attribute, DemandRes> demandLevel;


    private void InitLv()
    {
        int i = Enum.GetNames(typeof(Attribute)).Length;
        demandLevel = new Dictionary<Attribute, DemandRes>(i);
        foreach (Attribute type in Enum.GetValues(typeof(Attribute)))
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
                    strBar.Init(demandRes.lv, demandRes.baseCoin * (demandRes.lv + demandRes.growthRate), demandRes.baseFood * (demandRes.lv + demandRes.growthRate));
                    break;
                case Attribute.agi:
                    agiBar.Init(demandRes.lv, demandRes.baseCoin * (demandRes.lv + demandRes.growthRate), demandRes.baseFood * (demandRes.lv + demandRes.growthRate));
                    break;
                case Attribute.mint:
                    intBar.Init(demandRes.lv, demandRes.baseCoin * (demandRes.lv + demandRes.growthRate), demandRes.baseFood * (demandRes.lv + demandRes.growthRate));
                    break;
                case Attribute.end:
                    endBar.Init(demandRes.lv, demandRes.baseCoin * (demandRes.lv + demandRes.growthRate), demandRes.baseFood * (demandRes.lv + demandRes.growthRate));
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


    #region 管理层
    [Header("管理层")]
    public RectTransform waitTeamList;

    public EquipDetails equipDetails;

    private ProfileItem selectIndex;

    private void ShowManager()
    {
        foreach (RectTransform pl in peopleList)
        {
            Destroy(pl.gameObject);
        }

        foreach (People1 p in GameTeamController.GameData.waitRecruit)
        {
            GameObject pi = GameObject.Instantiate<GameObject>(peopleItem);
            ProfileItem pfi = pi.GetComponent<ProfileItem>();
            pfi.click = delegate (ProfileItem pl1)
            {
            };
            pfi.enter = delegate (Transform tr)
            {
                selectProfile.transform.position = tr.position;
                selectIndex = tr.GetComponent<ProfileItem>();
                equipDetails.InitDetails(selectIndex.people);
            };
            pfi.InitProfile(p);
            pi.transform.SetParent(waitTeamList, false);
        }
        if (peopleList.childCount == 0)
        {
            selectProfile.gameObject.SetActive(false);
        }
        else
        {
            selectProfile.gameObject.SetActive(true);
            selectProfile.GetComponent<RectTransform>().sizeDelta = waitTeamList.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        }
    }

    public void RotateFire()
    {
        PublicManager.ShowArlog("确定解雇",delegate(Pass pass) {
            if (pass==Pass.yes)
            {
                GameTeamController.GameData.FireWaitTeam(selectIndex.people);

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
