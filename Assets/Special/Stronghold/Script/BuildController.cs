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

    public Image textName;

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
            textCanvasGroup = textName.GetComponent<CanvasGroup>();
        }
        textCanvasGroup.alpha = 0.1f;
        backLight.gameObject.SetActive(true);
        PublicManager.Show(textName.gameObject);
        se = DOTween.Sequence();
        se.Join(textName.transform.DOLocalMoveY(GetComponent<RectTransform>().sizeDelta.y/3, 1f));
        se.Join(textCanvasGroup.DOFade(1, 1f));
        se.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backLight.gameObject.SetActive(false);
        se.Kill();
        textName.transform.position = textOriginalV3;
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
                isCache = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        backLight.gameObject.SetActive(true);
        PublicManager.Show(textName.gameObject);
        se = DOTween.Sequence();
        se.Join(textName.transform.DOLocalMoveY(80, 0.5f));
        se.Join(textName.DOFade(1, 1f));
        se.Play();

    }

    private void OnMouseExit()
    {
        backLight.gameObject.SetActive(false);
        se.Kill();
        textName.transform.position = textOriginalV3;
        PublicManager.Hide(textName.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
     
        backLight.transform.position = gameObject.transform.position;
        // backLight.range = transform.localScale.x * GetComponent<RectTransform>().rect.size.x/80;
      //  backLight.range = transform.localScale.x * transform.s.x / 80;
        backLight.gameObject.SetActive(false);
        textOriginalV3 = textName.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
