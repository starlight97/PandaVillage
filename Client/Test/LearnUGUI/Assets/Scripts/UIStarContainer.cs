using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStarContainer : MonoBehaviour
{
    public UIStar[] uiStars;

    public int myStars = 2;
    private int currentIdx = 0;

    void Start()
    {
        int idx = 0;
        foreach (var uiStar in this.uiStars)
        {

            uiStar.Init().AddListener(() => 
            {
                //Debug.LogFormat("anim complete : {0}, idx: {1}", uiStar.gameObject.name, this.currentIdx);

                //this.currentIdx++;

                //if (this.currentIdx < myStars)
                //{
                //    uiStars[this.currentIdx].Play();
                //}
            });

            uiStar.onUpdateAnimation = (norm) => 
            {

                if (norm >= 0.3f)
                {
                    var nextIdx = this.currentIdx + 1;

                    if (nextIdx < myStars)
                    {

                        if (uiStars[nextIdx].IsPlaying() == false)
                        {
                            this.currentIdx++;

                            if (this.currentIdx < myStars)
                            {
                                uiStars[this.currentIdx].Play();
                            }
                        }
                    }

                }
            };

            idx++;
        }

        if (this.myStars > 0)
        {
            uiStars[this.currentIdx].Play();
        }
    }
}