using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class SwipeController : MonoBehaviour
{
    public GameObject startNodeObj;
    public GameObject positionNodeObj;
    public GameObject endNodeObj;
    public GameObject lineObj;
    public GameObject currentLineRenderer;
    public LineRenderer line;
    public GameObject startNode;
    public GameObject positionNode;
    public GameObject endNode;
    public GameObject bombSpawnerObj;
    public BombSpawner bombSpawner;
    public AudioClip fireSound;
    public RaycastHit hit;
    public float angle;
    public GameObject cannon;
    public GameObject powerText;
    public AudioClip gunShot;
    public GameObject flashObj;
    public GameObject bloodSpray;
    public float fireRate;
    public GameObject gunNode;
    public GameObject gunNodeObj;
    public GameObject muzzleFlash;
    public GameObject manager;
    private GameManager managerScript;
    public virtual void Update()//}
    {
        this.manager = GameObject.Find("GameManager");
        this.managerScript = (GameManager) this.manager.GetComponent(typeof(GameManager));
        this.bombSpawnerObj = GameObject.Find("BombSpawner");
        this.bombSpawner = (BombSpawner) this.bombSpawnerObj.GetComponent(typeof(BombSpawner));
        if ((this.managerScript.gameStart == true) && (this.managerScript.disableWeapons == false))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (this.managerScript.miniGun == false)
                {
                    Ray mouseray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(mouseray1, out this.hit))
                    {
                        if (this.hit.collider.gameObject.tag == "NodePlane")
                        {
                            UnityEngine.Object.Instantiate(this.startNodeObj, this.hit.point, Quaternion.identity);
                            //Instantiate(lineObj, transform.position, transform.rotation);
                            //currentLineRenderer = GameObject.Find("LineObj(Clone)");
                            this.startNode = GameObject.Find("StartNode(Clone)");
                            this.startNode.gameObject.name = "SwipeStart";
                        }
                    }
                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (this.managerScript.miniGun == false)
                {
                    Ray mouseray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(mouseray2, out this.hit))
                    {
                        this.muzzleFlash.SetActive(false);
                        if (this.hit.collider.gameObject.tag == "NodePlane")
                        {
                            if (this.positionNode)
                            {
                                UnityEngine.Object.Destroy(this.positionNode.gameObject);
                            }
                            UnityEngine.Object.Instantiate(this.positionNodeObj, this.hit.point, Quaternion.identity);
                            this.positionNode = GameObject.Find("PositionNode(Clone)");
                            this.positionNode.gameObject.name = "PositionNode";
                            this.powerText.SetActive(true);
                            float xDistance = this.startNode.transform.position.x - this.positionNode.transform.position.x;
                            float yDistance = this.startNode.transform.position.y - this.positionNode.transform.position.y;
                            float distance = Vector3.Distance(this.startNode.transform.position, this.positionNode.transform.position);
                            float powerPercent = (distance / 20) * 100;
                            powerPercent = Mathf.Round(powerPercent * 10) / 10;
                            if (powerPercent > 100)
                            {
                                powerPercent = 100;
                            }
                            ((TextMesh) this.powerText.GetComponent(typeof(TextMesh))).text = powerPercent + "%".ToString();
                            this.angle = Mathf.Atan2(xDistance, yDistance) * Mathf.Rad2Deg;

                            {
                                float _21 = -this.angle;
                                Vector3 _22 = this.cannon.transform.eulerAngles;
                                _22.z = _21;
                                this.cannon.transform.eulerAngles = _22;
                            }
                        }
                    }
                }
                else
                {
                    if (this.managerScript.miniGun == true)
                    {
                        this.gunNode = GameObject.Find("GunNode");
                        if (this.gunNode)
                        {
                            UnityEngine.Object.Destroy(this.gunNode.gameObject);
                        }
                        this.fireRate = this.fireRate + Time.deltaTime;
                        if (this.fireRate > 0.01f)
                        {
                            this.muzzleFlash.SetActive(false);
                        }
                        if (this.fireRate > 0.035f)
                        {
                            this.fireRate = 0;
                            if (this.managerScript.bullets > 0)
                            {
                                Debug.Log("MiniActive");
                                Ray directionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                                if (Physics.Raycast(directionRay, out this.hit))
                                {
                                    UnityEngine.Object.Instantiate(this.gunNodeObj, this.hit.point, Quaternion.identity);
                                    this.gunNode = GameObject.Find("GunNode(Clone)");
                                    this.gunNode.gameObject.name = "GunNode";
                                }
                                float gunNodeX = this.gunNode.transform.position.x - this.cannon.transform.position.x;
                                float gunNodeY = this.gunNode.transform.position.y - this.cannon.transform.position.y;
                                this.angle = Mathf.Atan2(gunNodeX, gunNodeY) * Mathf.Rad2Deg;

                                {
                                    float _23 = -this.angle;
                                    Vector3 _24 = this.cannon.transform.eulerAngles;
                                    _24.z = _23;
                                    this.cannon.transform.eulerAngles = _24;
                                }
                                Ray miniGunRay = Camera.main.ScreenPointToRay(Input.mousePosition + (Random.onUnitSphere * 25));
                                //audio.Stop();
                                this.muzzleFlash.SetActive(true);
                                if (Physics.Raycast(miniGunRay, out this.hit))
                                {
                                    this.GetComponent<AudioSource>().PlayOneShot(this.gunShot);
                                    UnityEngine.Object.Instantiate(this.flashObj, this.hit.point, Quaternion.identity);
                                    this.managerScript.bullets = this.managerScript.bullets - 1;
                                    if (this.hit.collider.gameObject.tag == "Bird")
                                    {
                                        UnityEngine.Object.Instantiate(this.bloodSpray, this.hit.point, Quaternion.identity);
                                        UnityEngine.Object.Destroy(this.hit.collider.gameObject);
                                        this.managerScript.kill = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                this.powerText.SetActive(false);
                this.muzzleFlash.SetActive(false);
                if (this.managerScript.miniGun == false)
                {
                    Ray mouseray3 = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(mouseray3, out this.hit))
                    {
                        if (this.hit.collider.gameObject.tag == "NodePlane")
                        {
                            UnityEngine.Object.Instantiate(this.endNodeObj, this.hit.point, Quaternion.identity);
                            this.endNode = GameObject.Find("EndNode(Clone)");
                            this.endNode.gameObject.name = "SwipeEnd";
                            //Destroy (currentLineRenderer.gameObject);
                            UnityEngine.Object.Destroy(this.positionNode.gameObject);
                            this.startNode.transform.parent = this.endNode.transform;

                            {
                                float _25 = this.bombSpawner.transform.position.x;
                                Vector3 _26 = this.endNode.transform.position;
                                _26.x = _25;
                                this.endNode.transform.position = _26;
                            }

                            {
                                float _27 = this.bombSpawner.transform.position.y;
                                Vector3 _28 = this.endNode.transform.position;
                                _28.y = _27;
                                this.endNode.transform.position = _28;
                            }
                            if (this.managerScript.bullets > 0)
                            {
                                this.bombSpawner.spawn = true;
                                this.managerScript.bullets = this.managerScript.bullets - 1;
                                this.GetComponent<AudioSource>().PlayOneShot(this.fireSound);
                            }
                            else
                            {
                                if (this.managerScript.bullets <= 0)
                                {
                                    UnityEngine.Object.Destroy(this.startNode.gameObject);
                                    UnityEngine.Object.Destroy(this.endNode.gameObject);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}