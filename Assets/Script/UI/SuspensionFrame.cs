using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskFrame : MonoBehaviour
{
    public Font font;
    public Sprite sprite;
    private Builder builder;
    private Text titleText;
    private Text textText;
    private Image progressImage;
    private UnityAction progressFinish;
    // Start is called before the first frame update
    void Start()
    {
        VerticalLayoutGroup verticalLayout = gameObject.AddComponent<VerticalLayoutGroup>();
        verticalLayout.childControlHeight = false;
        verticalLayout.childControlWidth = true;
        verticalLayout.childForceExpandHeight = false;
        verticalLayout.padding = new RectOffset(20,20,10,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Build(Builder builder, UnityAction progressFinish=null)
    {
        if (this.builder == null)
        {
            GameObject title = new GameObject("Title");
            titleText = title.AddComponent<Text>();
            titleText.text = builder.title;
            titleText.font = font;
            titleText.fontStyle = FontStyle.Bold;
            titleText.fontSize = 24;
            title.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 32);
            title.transform.SetParent(gameObject.transform, false);

            GameObject text = new GameObject("Text");
            textText = text.AddComponent<Text>();
            textText.text = builder.text;
            textText.font = font;
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 120);
            text.transform.SetParent(gameObject.transform, false);

            GameObject progress = new GameObject("Progress");
            progressImage = progress.AddComponent<Image>();
            progressImage.sprite = sprite;
            progressImage.type = Image.Type.Filled;
            progressImage.fillMethod = Image.FillMethod.Horizontal;
            progressImage.fillAmount = builder.index / builder.max;
            progress.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 10);
            progress.transform.SetParent(gameObject.transform, false);

        }
        else
        {
            titleText.text = builder.title;
            textText.text = builder.text;
            progressImage.fillAmount = builder.index / builder.max;

        }
        if (builder.max == 0)
        {
            progressImage.gameObject.SetActive(false);
        }
        else
        {
            progressImage.gameObject.SetActive(true);
        }
        this.builder = builder;
        this.progressFinish = progressFinish;
    }

    public void ModifyTitle(string t)
    {
        builder.title = t;
        titleText.text = t;
    }
    public void ModifyText(string t)
    {
        builder.text = t;
        textText.text = t;
    }
    public void ModifyProgress(int i)
    {
        builder.index += i;
        progressImage.fillAmount = builder.index/ builder.max;
        if (i== builder.max)
        {
            progressFinish?.Invoke();
            //progressImage.gameObject.SetActive(false);
            //ModifyText(ProcessManager.Instance.language.Text("已完成"));
        }
    }

    public void Clear()
    {
        builder = null;
        foreach (var tr in gameObject.transform)
        {
            Destroy(((Transform)tr).gameObject);         
        }
    }


    public class Builder
    {
        public string title;
        public string text;
        public float max;
        public float index;
    }
}
