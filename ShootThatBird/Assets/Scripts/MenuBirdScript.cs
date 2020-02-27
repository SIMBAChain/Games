using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MenuBirdScript : MonoBehaviour
{
    public float speed;
    public bool flap;
    public float flapTimer;
    public float maxFlapTime;
    public float flapPower;
    public int layoutNo;
    public Material matA_1;
    public Material matB_1;
    public Material matC_1;
    public Material matA_2;
    public Material matB_2;
    public Material matC_2;
    public float aspect;
    public virtual void Start()
    {
        float flapForce = this.flapPower + Random.Range(-3f, 3f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, flapForce, 0);
        this.flapTimer = 0;
        this.flap = true;
        this.aspect = this.transform.localScale.x / this.transform.localScale.y;
        float growPercent = Random.Range(-0.25f, 0.5f);
        float newX = (this.transform.localScale.x * growPercent) + this.transform.localScale.x;
        float newY = (this.transform.localScale.y * growPercent) + this.transform.localScale.y;

        {
            float _11 = newX;
            Vector3 _12 = this.transform.localScale;
            _12.x = _11;
            this.transform.localScale = _12;
        }

        {
            float _13 = newY;
            Vector3 _14 = this.transform.localScale;
            _14.y = _13;
            this.transform.localScale = _14;
        }
        this.speed = this.speed - (this.speed * growPercent);
    }

    public virtual void Update()
    {
        this.layoutNo = PlayerPrefs.GetInt("Layout");

        {
            float _15 = this.transform.position.x + this.speed;
            Vector3 _16 = this.transform.position;
            _16.x = _15;
            this.transform.position = _16;
        }
        this.flapTimer = this.flapTimer + Time.deltaTime;
        if (this.flap)
        {
            this.flap = false;
            this.StartCoroutine(this.Flap());
        }
        if (this.flapTimer >= this.maxFlapTime)
        {
            float flapForce = this.flapPower + Random.Range(-3f, 3f);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0, flapForce, 0);
            //maxFlapTime += .1;
            this.flapTimer = 0;
        }
        if (this.transform.position.x > 19f)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual IEnumerator Flap()
    {
        if (this.layoutNo == 0)
        {
            this.GetComponent<Renderer>().material = this.matA_1;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matB_1;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matC_1;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matA_1;
            yield return new WaitForSeconds(0.1f);
            this.flap = true;
        }
        if (this.layoutNo == 1)
        {
            this.GetComponent<Renderer>().material = this.matA_2;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matB_2;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matC_2;
            yield return new WaitForSeconds(0.1f);
            this.GetComponent<Renderer>().material = this.matA_2;
            yield return new WaitForSeconds(0.1f);
            this.flap = true;
        }
    }

}