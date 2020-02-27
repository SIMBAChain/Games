using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class LayoutButton : MonoBehaviour
{
    public int layoutNo;
    public string animationName;
    public AudioClip buttonClick;
    public virtual void Update()
    {
        this.layoutNo = PlayerPrefs.GetInt("Layout");
    }

    public virtual void OnMouseDown()
    {
        if (this.layoutNo == 0)
        {
            PlayerPrefs.SetInt("Layout", 1);
        }
        else
        {
            if (this.layoutNo == 1)
            {
                PlayerPrefs.SetInt("Layout", 0);
            }
        }
        this.transform.parent.GetComponent<Animation>().Play(this.animationName);
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonClick);
    }

}