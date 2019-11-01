using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInCastleMenu : MonoBehaviour
{

    #region 牢房
    private List<People1> captives = new List<People1>();

    private int ration = 0;

    private int selectCaptive;

    public DigitText cellRationText;

    public RectTransform captivesList;

    public RectTransform selectFrame;

    public GameObject captiveItem;

    private void Start()
    {
        
    }

    public void AddCaptive(People1 people1)
    {
        captives.Add(people1);
        GameObject item = GameObject.Instantiate<GameObject>(captiveItem);
        item.transform.SetParent(captivesList, false);
        item.name = (captives.Count - 1).ToString();
        captiveItem.GetComponent<ProfileItem>().InitProfile(people1);
        ListItem li = item.AddComponent<ListItem>();
        li.leftAction = delegate (int i) {
            selectCaptive = i;
            selectFrame.SetParent(captivesList.GetChild(selectCaptive), false);
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
                captives.Remove(captives[selectCaptive]);
                Destroy(captivesList.GetChild(selectCaptive).gameObject);
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
                int lv = captives[selectCaptive].lv;
                captives.Remove(captives[selectCaptive]);
                Destroy(captivesList.GetChild(selectCaptive).gameObject);
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
                captives.Remove(captives[selectCaptive]);
                Destroy(captivesList.GetChild(selectCaptive).gameObject);
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


    public List<People1> GetPeoples() {
        return captives;
    }

    #endregion
}
