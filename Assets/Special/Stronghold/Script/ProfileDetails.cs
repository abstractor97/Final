using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileDetails : MonoBehaviour
{
    public RectTransform skillLayout;

    public SkillItem skillItem;

    public Text superSkillTitle;

    public Text superSkillNote;

    public Text sketch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDetails(People1 people)
    {
        GameObject si= GameObject.Instantiate<GameObject>(skillItem.gameObject);
        si.GetComponent<SkillItem>().Init("战斗", people.skill.battle);
        si.transform.SetParent(skillLayout,false);
        si = GameObject.Instantiate<GameObject>(skillItem.gameObject);
        si.GetComponent<SkillItem>().Init("探索", people.skill.explore);
        si.transform.SetParent(skillLayout, false);
        si = GameObject.Instantiate<GameObject>(skillItem.gameObject);
        si.GetComponent<SkillItem>().Init("运输", people.skill.transport);
        si.transform.SetParent(skillLayout, false);
        si = GameObject.Instantiate<GameObject>(skillItem.gameObject);
        si.GetComponent<SkillItem>().Init("应变", people.skill.strain);
        si.transform.SetParent(skillLayout, false);
        //skillItem.Init("战斗", people.skill.battle);
        //skillItem.Init("探索", people.skill.explore);
        //skillItem.Init("运输", people.skill.transport);
        //skillItem.Init("应变", people.skill.strain);
        switch (people.skill.superName)
        {
            case People1.SuperSkill.reinforcements:
                superSkillTitle.text =ProcessManager.language.Text("援军");
                superSkillNote.text = "";
                break;
            case People1.SuperSkill.shelling:
                superSkillTitle.text = ProcessManager.language.Text("炮击");
                superSkillNote.text = "";
                break;
            case People1.SuperSkill.subjugate:
                superSkillTitle.text = ProcessManager.language.Text("凝滞");
                superSkillNote.text = "";
                break;
            case People1.SuperSkill.garrison:
                superSkillTitle.text = ProcessManager.language.Text("驻守"); 
                superSkillNote.text = "";
                break;
        }
        sketch.text = ProcessManager.language.Text(people.sketch.ToString());

    }

    
}
