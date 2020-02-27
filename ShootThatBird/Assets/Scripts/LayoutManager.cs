using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class LayoutManager : MonoBehaviour
{
    public int layoutNo;
    public GameObject backgroundImage;
    public GameObject cityImage1;
    public GameObject bushesImage1;
    public GameObject cityImage2;
    public GameObject foregroundImage;
    public GameObject fireGroup;
    public Material bckImg1;
    public Material bckImg2;
    public Material forgrdImg1;
    public Material forgrdImg2;
    public virtual void Update()
    {
        this.layoutNo = PlayerPrefs.GetInt("Layout");
        if (this.layoutNo == 0)
        {
            this.backgroundImage.GetComponent<Renderer>().material = this.bckImg1;
            this.cityImage1.SetActive(true);
            this.bushesImage1.SetActive(true);
            this.cityImage2.SetActive(false);
            this.foregroundImage.GetComponent<Renderer>().material = this.forgrdImg1;
            this.fireGroup.SetActive(true);
        }
        if (this.layoutNo == 1)
        {
            this.backgroundImage.GetComponent<Renderer>().material = this.bckImg2;
            this.cityImage1.SetActive(false);
            this.bushesImage1.SetActive(false);
            this.cityImage2.SetActive(true);
            this.foregroundImage.GetComponent<Renderer>().material = this.forgrdImg2;
            this.fireGroup.SetActive(false);
        }
    }

}