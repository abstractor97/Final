using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInCastleMenu : MonoBehaviour
{

    #region 牢房
    private int ration = 0;

    private ProfileItem selectCaptive;

    public DigitText cellRationText;

    public RectTransform captivesList;

    public RectTransform selectFrame;

    public GameObject captiveItem;

    private void Start()
    {
        
    }

    public void ShowCell()
    {
        foreach (RectTransform pl in captivesList)
        {
            Destroy(pl.gameObject);
        }
        foreach (var cp in GameTeamController.GameData.cellPrisoners)
        {
            AddCaptive(cp);
        }
        selectFrame.gameObject.SetActive(captivesList.childCount == 0);
    }

    private void AddCaptive(People1 people1)
    {
        GameObject item = GameObject.Instantiate<GameObject>(captiveItem);
        item.transform.SetParent(captivesList, false);
        ProfileItem pfi = captiveItem.GetComponent<ProfileItem>();
        pfi.InitProfile(people1);
        pfi.click = delegate (ProfileItem p) {
            selectFrame.transform.position = p.transform.position;
            selectFrame.sizeDelta =p.GetComponent<RectTransform>().sizeDelta;
        };
        ration += 1;
        cellRationText.text = ration.ToString();

    }

    public void ChangeRation(int num)
    {
        if (ration == 0 && num < 0)
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
            if (pass == Pass.yes)
            {
                GameTeamController.GameData.cellPrisoners.Remove(selectCaptive.people);
                Destroy(selectCaptive.gameObject);
                int i = 0;
                foreach (Transform tr in captivesList.transform)
                {
                    tr.gameObject.name = i.ToString();
                    i++;
                }
            }
        });

    }

    public void CellAskfor()
    {
        PublicManager.ShowArlog("索取赎金", delegate (Pass pass) {
            if (pass == Pass.yes)
            {
                int lv = selectCaptive.people.lv;
                GameTeamController.GameData.cellPrisoners.Remove(selectCaptive.people);
                Destroy(selectCaptive.gameObject);
                int i = 0;
                foreach (Transform tr in captivesList.transform)
                {
                    tr.gameObject.name = i.ToString();
                    i++;
                }
                int coin = UnityEngine.Random.Range(lv * 10, lv * 25);
                GameResourceController.GameData.ResourceAdd(GameResourceController.Type.coin, coin);
                PublicManager.ShowTips("获得了" + coin + "金币");
            }
        });

    }


    public void CellExecution()
    {
        PublicManager.ShowArlog("处决", delegate (Pass pass) {
            if (pass == Pass.yes)
            {
                GameTeamController.GameData.cellPrisoners.Remove(selectCaptive.people);
                Destroy(selectCaptive.gameObject);
                int i = 0;
                foreach (Transform tr in captivesList.transform)
                {
                    tr.gameObject.name = i.ToString();
                    i++;
                }
                GameResourceController.GameData.ResourceAdd(GameResourceController.Type.food, 10);
                GameResourceController.GameData.ResourceAdd(GameResourceController.Type.crystal, 1);
                PublicManager.ShowTips("获得了" + 10 + "食物,获取了" + 1 + "水晶");
            }
        });

    }

    #endregion
}
