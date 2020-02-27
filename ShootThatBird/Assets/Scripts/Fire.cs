using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Fire : MonoBehaviour
{
    public bool flames;
    public float resetFlames;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public virtual void Update()
    {
        if (this.flames)
        {
            this.flames = false;
            this.StartCoroutine(this.Flame());
        }
        this.resetFlames = this.resetFlames + Time.deltaTime;
        if (this.resetFlames >= 0.3f)
        {
            this.flames = true;
            this.resetFlames = 0;
        }
    }

    public virtual IEnumerator Flame()
    {
        this.GetComponent<Renderer>().material = this.mat1;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material = this.mat2;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material = this.mat3;
        yield return new WaitForSeconds(0.1f);
    }

}