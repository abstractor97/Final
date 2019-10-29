using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI增强/分格进度条")]
public class LatticeSliderFill : MonoBehaviour
{
    public RectTransform fill;

    public Texture texture;

    public RectOffset rect;
    public float space=4;

    public Direction direction;

    public int min;
    public int max;

    public int value { get { return fill.childCount; }set { ValueChange(value);  } }

    private float cacheImgs;

    private void Start()
    {
        if (fill==null)
        {
            fill = GetComponent<RectTransform>();
        }
        if (direction == Direction.horizontal)
        {
            HorizontalLayoutGroup group= fill.gameObject.AddComponent<HorizontalLayoutGroup>();
            group.spacing = space;
            group.childAlignment = TextAnchor.MiddleLeft;
            group.padding = rect;
            group.childControlWidth = false;
          //  group.childControlHeight = false;
            group.childForceExpandWidth = false;
        }
        else
        {
            VerticalLayoutGroup group = fill.gameObject.AddComponent<VerticalLayoutGroup>();
            group.spacing = space;
            group.padding = rect;
            group.childAlignment = TextAnchor.UpperCenter;
         //   group.childControlWidth = false;
            group.childControlHeight = false;
            group.childForceExpandHeight = false;
        }

        ValueChange(min);

    }

    private void ValueChange(float value)
    {
        if (value>max)
        {
            value = max;
        }
        if (value<min)
        {
            value = min;
        }
        if (cacheImgs <= value)
        {
            for (int i = 0; i < value - cacheImgs; i++)
            {
                GameObject im = new GameObject();
                im.AddComponent<RawImage>().texture = texture;
                im.transform.SetParent(fill.transform, false);
                if (direction==Direction.horizontal)
                {
                    im.GetComponent<RectTransform>().sizeDelta = new Vector2((fill.sizeDelta.x-rect.left-rect.right-space*(max-1))/max,0);
                }
                else
                {
                    im.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (fill.sizeDelta.y-rect.top - rect.bottom- space * (max - 1)) / max);
                }

            }
        }
        else
        {
            for (int i = 0; i < cacheImgs - value; i++)
            {
                Destroy(fill.GetChild(fill.childCount - 1).gameObject);
            }
        }
    }

    public enum Direction
    {
        horizontal,
        vertical
    }
}
