using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamList : MonoBehaviour
{
    public GameObject teamItem;
    public RectTransform list;
    /// <summary>
    /// 最多允许选中几个
    /// </summary>
    public int outNumber=1;
    private List<int> sels;
    // Start is called before the first frame update
    void Start()
    {
        sels = new List<int>(outNumber);
        int i = 0;
        foreach (var team in GameTeamController.GameData.outTeam)
        {
            GameObject ti = GameObject.Instantiate<GameObject>(teamItem);
            TeamItem tii= ti.GetComponent<TeamItem>();
            tii.Init(team);
            ti.name = i.ToString();
            ti.AddComponent<ListItem>().leftAction = delegate (int sel) {
                if (sels.Contains(sel))
                {
                    tii.selectFarme.alpha = 0;
                    sels.Remove(sel);
                }
                else if(sels.Count<outNumber)
                {
                    tii.selectFarme.alpha = 1;
                    sels.Add(sel);
                }
            };
            ti.transform.SetParent(list,false);
            i++;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter()
    {

    }

    public void CloseTeamList()
    {
        Destroy(gameObject);
    }
}
