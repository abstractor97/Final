using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ProfileItem : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler
{

    public Image headImage;
    public Text nameText; 

    public DigitText strText;

    public DigitText agiText;
    public DigitText intText;
    public DigitText endText;

    public UnityAction<Transform> enter;
    public UnityAction<ProfileItem> click;
    public People1 people;

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
        this.people = people;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        enter?.Invoke(transform);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            click?.Invoke(this);
        }
    }
}
