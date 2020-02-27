using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class DeleteItem : MonoBehaviour
{
    public float waitTime;
    public virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(this.waitTime);
        UnityEngine.Object.Destroy(this.gameObject);
    }

}