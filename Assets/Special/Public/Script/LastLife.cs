using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLife : MonoBehaviour
{

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LastDes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator LastDes()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}
