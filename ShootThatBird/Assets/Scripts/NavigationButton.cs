using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class NavigationButton : MonoBehaviour
{
    public string animationName;
    public AudioClip buttonClick;
    public int levelNumber;
    public virtual IEnumerator OnMouseDown()
    {
        this.transform.parent.GetComponent<Animation>().Play(this.animationName);
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonClick);
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(this.levelNumber);
    }

}