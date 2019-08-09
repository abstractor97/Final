using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceLattice : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Place place;

    public Vector3 deviation;

    public Image placeIcon;

    public GameObject plane;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (place==null)
        {
            return;
        }
        CanvasGroup group= plane.GetComponent<CanvasGroup>();
        group.alpha = 1;
        PlacePlane pp= plane.GetComponent<PlacePlane>();
        pp.placeName.text = place.name;
        pp.placeExplain.text = place.explain;
        switch (place.state)
        {
            case Place.State.Unreconnoitre:
                pp.placeState.text = "未知";
                pp.placeState.color = Color.gray;
                break;
            case Place.State.safe:
                pp.placeState.text = "安全";
                pp.placeState.color = Color.green;
                break;
            case Place.State.risky:
                pp.placeState.text = "有风险";
                pp.placeState.color = Color.yellow;
                break;
            case Place.State.hostile:
                pp.placeState.text = "敌对";
                pp.placeState.color = Color.red;
                break;
        }
       
        plane.transform.position = gameObject.transform.position + deviation;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (place == null)
        {
            return;
        }
        CanvasGroup group = plane.GetComponent<CanvasGroup>();
        group.alpha = 0;
      
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
