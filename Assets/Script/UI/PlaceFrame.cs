using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceFrame : MonoBehaviour
{

    public GameObject placePlane;

    public GameObject actionPlane;

    public GameObject placeLattice;

    public GameObject actionLattice;

    public GameObject plane;

    public Actions[] walkActions;
    public Actions[] interactionActions;
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

    private PlaceLattice[] placeLattices;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Place[] places,bool isNew=true)
    {
        if (isNew)
        {

            placeLattices = new PlaceLattice[totalDistance];
            for (int i = 0; i < totalDistance; i++)
            {
                GameObject pl = GameObject.Instantiate<GameObject>(placeLattice);
                PlaceLattice pls = pl.GetComponent<PlaceLattice>();
                pls.plane = plane;
                pl.transform.SetParent(placePlane.transform, false);
                placeLattices[i] = pls;
            }

            foreach (var tr in gameObject.transform)
            {
                Destroy(((Transform)tr).gameObject);
            }

            foreach (var p in places)
            {
                placeLattices[p.position].place = p;
                int count = p.intact / (100 / p.lowSprite.Length);
                placeLattices[p.position].placeIcon.sprite = p.lowSprite[count];
            }
        }    
        CanvasGroup group= gameObject.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;

    }

    private void WalkAction(int sel)
    {
        switch (sel)
        {
            case 0:
                Move(-1);
                break;
            case 1:
                Move(1);
                break;
            case 2:
                Move(1, MoveType.run);
                break;
            case 3:
                Move(1, MoveType.quiet);
                break;
            case 4:
                break;
        }
    }

    private void IdAction(int sel)
    {
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

    public void Move(int step, MoveType move=MoveType.walk)
    {
        FindObjectOfType<MapControl>().eventEmitter.Explore(placeLattices[position+step].place);
        //todo 触发
    }


    public void GetInto()
    {

    }

    public enum MoveType
    {
        walk,
        run,
        quiet,
    }

    
}
