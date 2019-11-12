using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{

    public Light backLight;

    public GameObject menu;
    [Tooltip("提示牌")]
    public Image notice;

    public GameObject closeTime;

    private bool isCache;

    private Vector3 textOriginalV3;

    private Sequence se;

    private CanvasGroup textCanvasGroup;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left)
        {
            if (isCache)
            {
                PublicManager.Show(menu);
            }
            else
            {
                menu = GameObject.Instantiate<GameObject>(menu);
                menu.transform.SetParent(PublicManager.HUD, false);
                isCache = true;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textCanvasGroup==null)
        {
            textCanvasGroup = notice.GetComponent<CanvasGroup>();
        }
        textCanvasGroup.alpha = 0.1f;
        backLight.gameObject.SetActive(true);
        PublicManager.Show(notice.gameObject);
        se = DOTween.Sequence();
        se.Join(notice.transform.DOLocalMoveY(GetComponent<RectTransform>().sizeDelta.y/3, 1f));
        se.Join(textCanvasGroup.DOFade(1, 1f));
        se.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backLight.gameObject.SetActive(false);
        se.Kill();
        notice.transform.position = textOriginalV3;
        textCanvasGroup.alpha = 0;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCache)
            {
                PublicManager.Show(menu);
            }
            else
            {
                menu = GameObject.Instantiate<GameObject>(menu);
                menu.transform.SetParent(PublicManager.HUD, false);
                PublicManager.Show(menu);
                isCache = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        backLight.gameObject.SetActive(true);
        notice.transform.position = textOriginalV3;
        CanvasGroup group = notice.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = false;
        group.blocksRaycasts = false;
        //PublicManager.Show(notice.gameObject);
        //se = DOTween.Sequence();
        //se.Join(notice.transform.DOLocalMoveY(80, 0.5f));
        //se.Join(notice.DOFade(1, 1f));
        //se.Play();

    }

    private void OnMouseExit()
    {
        backLight.gameObject.SetActive(false);
       // se.Kill();
        notice.transform.position = textOriginalV3;
        CanvasGroup group = notice.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
     //   PublicManager.Hide(notice.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        float x = transform.localScale.x * GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float y = transform.localScale.x * GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        backLight.transform.position = gameObject.transform.position;
        // backLight.range = transform.localScale.x * GetComponent<RectTransform>().rect.size.x/80;
      //  backLight.range = transform.localScale.x * transform.s.x / 80;
        backLight.gameObject.SetActive(false);
        textOriginalV3 = transform.position + new Vector3(0,y,0);
        notice.transform.position = textOriginalV3;
        closeTime = GameObject.Instantiate<GameObject>(closeTime);
        closeTime.transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCloseTime(int hour,int mins)
    {
       
        closeTime.GetComponentInChildren<Text>().text = hour+":"+mins;
        FindObjectOfType<WorldClock>().clock=delegate() {
            string time= closeTime.GetComponentInChildren<Text>().text;
            string[] ts= time.Split(':');
            int h = int.Parse(ts[0]);
            int m = int.Parse(ts[0]);
            if (m==0)
            {
                h--;
                m = 59;
            }
            else
            {
                m--;
            }
            closeTime.GetComponentInChildren<Text>().text = h + ":" + m;

        };
    }
}
