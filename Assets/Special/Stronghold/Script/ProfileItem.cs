using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileItem : MonoBehaviour
{

    public Image headImage;
    public Text nameText; 

    public DigitText strText;

    public DigitText agiText;
    public DigitText intText;
    public DigitText endText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitProfile(People1 people) {
        headImage.sprite = people.head;
        nameText.text = people.defName;
        InitAttribute(people.attribute.str, people.attribute.agi, people.attribute.mint, people.attribute.end);
    }

    public void InitProfile(Sprite head ,string name, int str, int agi, int mint, int end)
    {
        headImage.sprite = head;
        nameText.text = name;
        InitAttribute(str,agi,mint,end);

    }


    private void InitAttribute(int str, int agi, int mint, int end)
    {
        strText.text = str.ToString();
        agiText.text = agi.ToString();
        intText.text = mint.ToString();
        endText.text = end.ToString();
    }

}
