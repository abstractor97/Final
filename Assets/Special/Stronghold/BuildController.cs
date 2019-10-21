using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{

    public Light backLight;

    public GameObject menu;

    private bool isCache;

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
    }

    private void OnMouseExit()
    {
        backLight.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
     
        backLight.transform.position = gameObject.transform.position;
        backLight.range = transform.localScale.x * GetComponent<SpriteRenderer>().size.x*2;
        backLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
