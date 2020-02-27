using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class FlashScript : MonoBehaviour
{
    public float delay;
    public virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(this.delay);
        UnityEngine.Object.Destroy(this.gameObject);
    }

}