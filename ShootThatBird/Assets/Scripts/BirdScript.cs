using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BirdScript : MonoBehaviour
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
    public AudioClip birdSound;
    public GameObject bloodSpray;
    public GameObject birdDeath;
    public GameObject manager;
    private GameManager managerScript;
    public virtual void Start()
    {
        this.manager = GameObject.Find("GameManager");
        this.managerScript = (GameManager) this.manager.GetComponent(typeof(GameManager));
        float flapForce = this.flapPower + Random.Range(-3f, 3f);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, flapForce, 0);
        //maxFlapTime += .1;
        this.flapTimer = 0;
        this.flap = true;
        this.aspect = this.transform.localScale.x / this.transform.localScale.y;
        float growPercent = Random.Range(-0.1f, 0.5f);
        float newX = (this.transform.localScale.x * growPercent) + this.transform.localScale.x;
        float newY = (this.transform.localScale.y * growPercent) + this.transform.localScale.y;

        {
            float _1 = newX;
            Vector3 _2 = this.transform.localScale;
            _2.x = _1;
            this.transform.localScale = _2;
        }

        {
            float _3 = newY;
            Vector3 _4 = this.transform.localScale;
            _4.y = _3;
            this.transform.localScale = _4;
        }
        this.speed = this.speed - (this.speed * growPercent);
    }

    public virtual void Update()
    {
        this.layoutNo = PlayerPrefs.GetInt("Layout");
        this.manager = GameObject.Find("GameManager");
        this.managerScript = (GameManager) this.manager.GetComponent(typeof(GameManager));

        {
            float _5 = this.transform.position.x + this.speed;
            Vector3 _6 = this.transform.position;
            _6.x = _5;
            this.transform.position = _6;
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
        if ((this.transform.position.x > 26f) && (this.gameObject.name == "Bird(Clone)"))
        {
            this.gameObject.name = "OldBird";
            this.managerScript.minusHeart = true;
        }
        if (this.managerScript.killAll == true)
        {
            this.StartCoroutine(this.KillAll());
        }
    }

    public virtual IEnumerator KillAll()
    {
        UnityEngine.Object.Instantiate(this.bloodSpray, this.transform.position, this.transform.rotation);
        UnityEngine.Object.Instantiate(this.birdDeath, this.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        UnityEngine.Object.Destroy(this.gameObject);
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