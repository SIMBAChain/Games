using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BombScript : MonoBehaviour
{
    public bool launchBomb;
    public GameObject swipeStart;
    public GameObject swipeEnd;
    public float swipeDistance;
    public float multiplier;
    public bool movePlayer;
    public bool ballInPlay;
    public float power;
    public GameObject dragBlood;
    public GameObject bloodSpray;
    public GameObject heart;
    public GameObject ammo;
    public GameObject nukeObj;
    public GameObject miniGunObj;
    public GameObject manager;
    private GameManager managerScript;
    public virtual void Start()
    {
        this.swipeStart = GameObject.Find("SwipeStart");
        this.swipeEnd = GameObject.Find("SwipeEnd");
        this.transform.LookAt(this.swipeStart.transform);
        this.swipeDistance = Vector3.Distance(this.swipeStart.transform.position, this.swipeEnd.transform.position);
        if (this.swipeDistance > 20)
        {
            this.swipeDistance = 20;
        }
        this.power = this.swipeDistance * this.multiplier;
        UnityEngine.Object.Destroy(this.swipeStart.gameObject);
        UnityEngine.Object.Destroy(this.swipeEnd.gameObject);
        this.GetComponent<Rigidbody>().AddRelativeForce(-Vector3.forward * this.power);
    }

    public virtual void Update()
    {
        this.manager = GameObject.Find("GameManager");
        this.managerScript = (GameManager) this.manager.GetComponent(typeof(GameManager));
    }

    public virtual IEnumerator OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().gameObject.tag == "Bird")
        {
            UnityEngine.Object.Destroy(collision.gameObject);
            this.managerScript.kill = true;
            int powerUpChance = Random.Range(1, 50);
            if ((powerUpChance >= 0) && (powerUpChance < 11))
            {
                UnityEngine.Object.Instantiate(this.heart, this.transform.position, Quaternion.Euler(0, 0, -180));
            }
            if ((powerUpChance >= 11) && (powerUpChance < 16))
            {
                UnityEngine.Object.Instantiate(this.ammo, this.transform.position, Quaternion.Euler(0, 0, -180));
            }
            if ((powerUpChance >= 16) && (powerUpChance < 21))
            {
                UnityEngine.Object.Instantiate(this.nukeObj, this.transform.position, Quaternion.Euler(0, 0, -180));
            }
            if ((powerUpChance >= 21) && (powerUpChance < 26))
            {
                UnityEngine.Object.Instantiate(this.miniGunObj, this.transform.position, Quaternion.Euler(0, 0, -180));
            }
            UnityEngine.Object.Instantiate(this.bloodSpray, this.transform.position, this.transform.rotation);
            this.dragBlood = GameObject.Find("BloodSpray(Clone)");
            this.dragBlood.transform.parent = this.gameObject.transform;
            this.dragBlood.gameObject.name = "OldBlood";
            yield return new WaitForSeconds(0.2f);
            this.dragBlood.transform.parent = null;
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "Heart")
        {
            if (this.managerScript.hearts < 6)
            {
                this.managerScript.hearts = this.managerScript.hearts + 1;
            }
            UnityEngine.Object.Destroy(collision.gameObject);
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "Ammo")
        {
            this.managerScript.bullets = this.managerScript.bullets + 5;
            UnityEngine.Object.Destroy(collision.gameObject);
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "Nuke")
        {
            this.managerScript.nuke = true;
            UnityEngine.Object.Destroy(collision.gameObject);
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "MiniGun")
        {
            this.managerScript.miniGun = true;
            this.managerScript.rememberBulletCount = this.managerScript.bullets;
            this.managerScript.bullets = 250;
            UnityEngine.Object.Destroy(collision.gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "DisableBall")
        {
            this.gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

}