using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlaceFrame : MonoBehaviour
{

    public GameObject placePlane;

    public GameObject actionPlane;

    public GameObject placeLattice;

    public GameObject actionLattice;

    public GameObject plane;

    public GameObject scale;

    public GameObject idf;
    /// <summary>
    /// 行动
    /// </summary>
    public Actions[] walkActions;
    /// <summary>
    /// 交互
    /// </summary>
    public Actions[] interactionActions;
    /// <summary>
    /// 场景
    /// </summary>
    public Actions[] sceneActions;

    [System.Serializable]
    public struct Actions
    {
        public string name;

        public Sprite icon;
    }

    [HideInInspector]
    public int position;
    [HideInInspector]
    public int totalDistance;
    /// <summary>
    /// 每格间距
    /// </summary>
    private float distance;
    private PlaceLattice[] placeLattices;
    private bool isWalk;
    private Posture posture = Posture.normal;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var a in walkActions)
        {
            GameObject il = GameObject.Instantiate<GameObject>(actionLattice);
            il.GetComponentInChildren<Text>().text=a.name;
            il.GetComponentInChildren<Image>().sprite = a.icon;
            ListItem li = il.AddComponent<ListItem>();           
            li.leftAction += WalkAction;
            il.transform.SetParent(actionPlane.transform.Find("Walk"), false);
        }
        foreach (var i in interactionActions)
        {
            GameObject il = GameObject.Instantiate<GameObject>(actionLattice);
            il.GetComponentInChildren<Text>().text = i.name;
            il.GetComponentInChildren<Image>().sprite = i.icon;
            ListItem li = il.AddComponent<ListItem>();
            li.leftAction += IdAction;
            il.transform.SetParent(actionPlane.transform.Find("Interaction"), false);
        }
        foreach (var s in sceneActions)
        {
            GameObject il = GameObject.Instantiate<GameObject>(actionLattice);
            il.GetComponentInChildren<Text>().text = s.name;
            il.GetComponentInChildren<Image>().sprite = s.icon;
            ListItem li = il.AddComponent<ListItem>();
            li.leftAction += SceneAction;
            il.transform.SetParent(actionPlane.transform.Find("Scene"), false);
        }
        distance = (scale.GetComponent<RectTransform>().sizeDelta.x - 40) / (totalDistance - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 进入地点方法，需要先添加当前位置和总长度
    /// </summary>
    public void Show(Place[] places,bool isNew=true)
    {
        if (isNew)
        {

            foreach (var tr in gameObject.transform)
            {
                Destroy(((Transform)tr).gameObject);
            }

            placeLattices = new PlaceLattice[totalDistance];
            for (int i = 0; i < totalDistance; i++)
            {
                GameObject pl = GameObject.Instantiate<GameObject>(placeLattice);
                PlaceLattice pls = pl.GetComponent<PlaceLattice>();
                pls.plane = plane;
                pl.transform.SetParent(placePlane.transform, false);
                pls.place = places[i];
              //  int count = places[i].intact / (100 / places[i].lowSprite.Length);
                pls.placeIcon.sprite = places[i].lowSprite;
                placeLattices[i] = pls;
            }

            //foreach (var p in places)
            //{
            //    placeLattices[p.position].place = p;
            //    int count = p.intact / (100 / p.lowSprite.Length);
            //    placeLattices[p.position].placeIcon.sprite = p.lowSprite[count];
            //}
        }    
        CanvasGroup group= gameObject.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;

    }

    private void WalkAction(int sel)
    {
        if (isWalk)
        {
            FindObjectOfType<PublicManager>().ShowTips("正在行动中");
            return;
        }
        switch (sel)
        {
            case 0:
                isWalk = true;
                posture = Posture.normal;
                Move(-1);
                break;
            case 1:
                isWalk = true;
                posture = Posture.normal;
                Move(1);
                break;
            case 2:
                isWalk = true;
                posture = Posture.run;
                Move(1);
                break;
            case 3:
                isWalk = true;
                posture = Posture.quiet;
                Move(1);
                break;
            case 4:
                posture = Posture.normal;
                break;
        }
    }

    private void IdAction(int sel)
    {
        if (isWalk)
        {
            FindObjectOfType<PublicManager>().ShowTips("正在行动中");
            return;
        }
        switch (sel)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private void SceneAction(int sel)
    {
        if (isWalk)
        {
            FindObjectOfType<PublicManager>().ShowTips("正在行动中");
            return;
        }
        switch (sel)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public void Move(int step)
    {
        Dictionary<EventEmitter.ExploreEvent, float> perturbation = new Dictionary<EventEmitter.ExploreEvent, float>();
        switch (posture)
        {
            case Posture.normal:
                break;
            case Posture.run:
                perturbation.Add(EventEmitter.ExploreEvent.NPC, 0.3f);
                perturbation.Add(EventEmitter.ExploreEvent.place, -0.2f);
                perturbation.Add(EventEmitter.ExploreEvent.e, -0.1f);
                break;
            case Posture.quiet:
                perturbation.Add(EventEmitter.ExploreEvent.NPC, -0.3f);
                perturbation.Add(EventEmitter.ExploreEvent.place, 0.2f);
                perturbation.Add(EventEmitter.ExploreEvent.e, 0.1f);
                break;
        }
        
       FindObjectOfType<MapControl>().eventEmitter.MoveEvent(placeLattices[position+step].place, perturbation);
        FindObjectOfType<MapControl>().eventEmitter.position = position + step;
        Tweener tweener = idf.transform.DOMoveX(idf.transform.position.x+ (distance*step),1*step);
        tweener.OnComplete(delegate { isWalk = false; });
     
    }
  /// <summary>
  /// 进入地点
  /// </summary>
    public void GetInto()
    {

    }


   


    public enum Posture
    {
        normal,
        run,
        quiet,
    }

    
}
