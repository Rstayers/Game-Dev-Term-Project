using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class HeartContainer : MonoBehaviour
{
    [HideInInspector] public HeartContainer next;
    [Range(0, 1)]
    private float fill;
    [SerializeField] Image FillImage;
    public void SetHeart(float count)
    {
        //go down the linked list and set the heart value accordingly
        fill = count;
        FillImage.fillAmount = fill;
        count--;
        if(next != null)
        {
            next.SetHeart(count);
        }
    }
}
