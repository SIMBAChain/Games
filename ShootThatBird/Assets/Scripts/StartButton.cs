using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class StartButton : MonoBehaviour
{
    public string anim;
    public AudioClip buttonClick;
    public GameObject manager;
    private GameManager managerScript;
    public virtual void Update()
    {
        this.manager = GameObject.Find("GameManager");
        this.managerScript = (GameManager) this.manager.GetComponent(typeof(GameManager));
    }

    public virtual IEnumerator OnMouseDown()
    {
        this.transform.parent.GetComponent<Animation>().Play(this.anim);
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonClick);
        yield return new WaitForSeconds(0.2f);
        this.managerScript.gameStart = true;
    }

}