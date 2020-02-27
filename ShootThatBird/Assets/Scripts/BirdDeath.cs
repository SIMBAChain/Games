using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BirdDeath : MonoBehaviour
{
    public AudioClip deathSound;
    public float delay;
    public virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        this.GetComponent<AudioSource>().PlayOneShot(this.deathSound);
        yield return new WaitForSeconds(this.delay);
        UnityEngine.Object.Destroy(this.gameObject);
    }

}