using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ItemDropScipt : MonoBehaviour
{
    public bool heart;
    public bool ammo;
    public bool nuke;
    public bool miniGun;
    public virtual IEnumerator Start()
    {
        if (this.heart)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.tag = "Heart";
        }
        if (this.ammo)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.tag = "Ammo";
        }
        if (this.nuke)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.tag = "Nuke";
        }
        if (this.miniGun)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.tag = "MiniGun";
        }
    }

}