using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellMenuItem : MonoBehaviour
{
    Color baseColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Image  background;
    void Awake()
    {
        baseColor = background.color;
        baseColor.a = 255f;
    }

    public void Select()
    {
        background.color = hoverColor;

    }
    public void Deselect()
    {
        background.color = baseColor;
       

    }
}
