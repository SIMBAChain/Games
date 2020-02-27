using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using RestSharp;
[System.Serializable]
public partial class GameManager : MonoBehaviour
{
    public bool gameStart;
    public GameObject startGroup;
    public int kills;
    public int highScore;
    public GameObject killText;
    public int birdCount;
    public int killCount;
    public int bullets;
    public int rememberBulletCount;
    public GameObject bulletText;
    public int level;
    public bool levelIncrease;
    public bool disableWeapons;
    public bool spawn;
    public int spawnCount;
    public float spawnGap;
    public float timer;
    public bool kill;
    public GameObject birdSpawnerObj;
    public BirdSpawner birdSpawner;
    public bool gameOver;
    public GameObject gameOverBox;
    public GameObject currentScoreText;
    public GameObject highScoreText;
    public int actionLimit;
    public int hearts;
    public bool minusHeart;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public GameObject heart6;
    public bool nuke;
    public bool killAll;
    public bool miniGun;
    public AudioClip birdSound;
    public AudioClip splatSound;
    public AudioClip heartPop;

    public string walletAddress;
    public virtual void Start()
    {
        this.bullets = 20;
        this.level = 1;
        this.birdCount = 1;
        this.spawnGap = 0.2f;
        this.hearts = 3;

        WalletStorage.LoadWallet();
        if (SimbaInfo.Wallet == null)
        {
            GenWallet.RandomWallet();
            WalletStorage.SaveWallet();
        }
        walletAddress = SimbaInfo.Wallet.GetAccount(0).Address;
    }

    public virtual void Update()
    {
        Application.targetFrameRate = 60;
        this.highScore = PlayerPrefs.GetInt("HighScore");
        ((TextMesh) this.killText.GetComponent(typeof(TextMesh))).text = "Kills: " + this.kills.ToString();
        ((TextMesh) this.bulletText.GetComponent(typeof(TextMesh))).text = "Bullets: " + this.bullets.ToString();
        this.birdSpawnerObj = GameObject.Find("BirdSpawner");
        this.birdSpawner = (BirdSpawner) this.birdSpawnerObj.GetComponent(typeof(BirdSpawner));
        if (this.hearts == 6)
        {
            this.heart1.SetActive(true);
            this.heart2.SetActive(true);
            this.heart3.SetActive(true);
            this.heart4.SetActive(true);
            this.heart5.SetActive(true);
            this.heart6.SetActive(true);
        }
        if (this.hearts == 5)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(true);
            this.heart3.SetActive(true);
            this.heart4.SetActive(true);
            this.heart5.SetActive(true);
            this.heart6.SetActive(true);
        }
        if (this.hearts == 4)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(false);
            this.heart3.SetActive(true);
            this.heart4.SetActive(true);
            this.heart5.SetActive(true);
            this.heart6.SetActive(true);
        }
        if (this.hearts == 3)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(false);
            this.heart3.SetActive(false);
            this.heart4.SetActive(true);
            this.heart5.SetActive(true);
            this.heart6.SetActive(true);
        }
        if (this.hearts == 2)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(false);
            this.heart3.SetActive(false);
            this.heart4.SetActive(false);
            this.heart5.SetActive(true);
            this.heart6.SetActive(true);
        }
        if (this.hearts == 1)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(false);
            this.heart3.SetActive(false);
            this.heart4.SetActive(false);
            this.heart5.SetActive(false);
            this.heart6.SetActive(true);
        }
        if (this.hearts <= 0)
        {
            this.heart1.SetActive(false);
            this.heart2.SetActive(false);
            this.heart3.SetActive(false);
            this.heart4.SetActive(false);
            this.heart5.SetActive(false);
            this.heart6.SetActive(false);
            this.gameOver = true;
        }
        this.timer = this.timer + Time.deltaTime;
        if (this.gameStart)
        {
            this.startGroup.SetActive(false);
            if ((this.timer >= this.spawnGap) && (this.spawnCount < this.birdCount))
            {
                this.birdSpawner.spawn = true;
                this.spawnCount = this.spawnCount + 1;
                this.spawnGap = this.spawnGap - 0.001f;
                this.timer = 0;
            }
            if (this.kill)
            {
                this.kill = false;
                this.kills = this.kills + 1;
                this.killCount = this.killCount + 1;
                this.bullets = this.bullets + 1;
                this.GetComponent<AudioSource>().PlayOneShot(this.birdSound);
                this.GetComponent<AudioSource>().PlayOneShot(this.splatSound);
                if (this.killCount == this.birdCount)
                {
                    this.level = this.level + 1;
                    this.birdCount = this.birdCount + 1;
                    this.killCount = 0;
                    this.spawnCount = 0;
                }
            }
            if (this.nuke)
            {
                this.nuke = false;
                this.killAll = true;
                int birdsLeft = this.spawnCount - this.killCount;
                this.kills = this.kills + birdsLeft;
                this.bullets = this.bullets + birdsLeft;
                this.StartCoroutine(this.YieldKill());
            }
            if (this.minusHeart)
            {
                this.minusHeart = false;
                this.hearts = this.hearts - 1;
                this.GetComponent<AudioSource>().PlayOneShot(this.heartPop);
                this.killCount = this.killCount + 1;
                if (this.killCount == this.birdCount)
                {
                    this.level = this.level + 1;
                    this.birdCount = this.birdCount + 1;
                    this.killCount = 0;
                    this.spawnCount = 0;
                }
            }
        }
        if (this.gameOver && (this.actionLimit == 0))
        {
            this.gameOver = false;
            this.disableWeapons = true;
            this.actionLimit = 1;
            this.gameOverBox.GetComponent<Animation>().Play("GameOver");
            PlayerPrefs.SetInt("RecentScore", this.kills);

            var scoreContent = GetTransactions.GetSimbaTransactions("/Highscore/?score_gt=" + this.kills + "&useraddress_contains=" + SimbaInfo.Wallet.GetAccount(0).Address);
            var highscoreJson = JObject.Parse(scoreContent);
            var ishighscore = int.Parse(highscoreJson["count"].ToString());
            Debug.Log(ishighscore);
            if (ishighscore > 0)
            {
                Debug.Log("No New Highscore");
            }
            else
            {
                Debug.Log("new highscore");
                PlayerPrefs.SetInt("HighScore", this.kills);
                var request = new RestRequest("");


                request.AddParameter("from", SimbaInfo.Wallet.GetAccount(0).Address.ToString());
                request.AddParameter("useraddress", SimbaInfo.Wallet.GetAccount(0).Address.ToString());
                request.AddParameter("__highscores", SimbaInfo.Wallet.GetAccount(0).Address.ToString());
                request.AddParameter("score", this.kills);
                var content = PostandSign.PostSimbaTransaction("/Highscore/", request);
                JObject json = JObject.Parse(content);
                JObject raw = JObject.Parse(json["payload"]["raw"].ToString());
                Debug.Log(content);
                Sign(json, raw);
            }

        /*    if (this.kills > this.highScore)
            {
              
            }*/
        }
        if ((this.miniGun == true) && (this.bullets <= 0))
        {
            this.miniGun = false;
            this.bullets = this.rememberBulletCount;
        }
        ((TextMesh) this.currentScoreText.GetComponent(typeof(TextMesh))).text = this.kills + " Kills".ToString();
        ((TextMesh) this.highScoreText.GetComponent(typeof(TextMesh))).text = this.highScore + " Kills".ToString();
    }



    string Sign(JObject json, JObject raw)
    {
        var i = 0;
        while (i < 5)
        {
            var payload = PostandSign.SignSimbaTransaction(raw);

            string txnId = json["id"].ToString();
            Debug.Log("TXN ID BELOW");
            Debug.Log(txnId);

            var signRequest = new RestRequest("");
            signRequest.AddParameter("payload", "0x" + payload);

            var submitsigned = PostandSign.PostSimbaTransaction("/transaction/" + txnId + "/", signRequest);
            var submitJson = JObject.Parse(submitsigned);
            Debug.Log(submitsigned);


            try
            {
                var errors = submitJson["errors"];
                var error = errors[0];
                var code = error["code"].ToString();
                if (code == "15001")
                {
                    var newNonce = error["meta"]["suggested_nonce"].ToObject<int>();

                    raw["nonce"] = newNonce.ToString("X");

                    Debug.Log(newNonce);
                }
            }
            catch
            {
               
                return submitsigned;

            }

            i++;
        }
        return "";
    }


    public virtual IEnumerator YieldKill()
    {
        yield return new WaitForSeconds(0.1f);
        this.killAll = false;
        yield return new WaitForSeconds(1);
        this.level = this.level + 1;
        this.birdCount = this.birdCount + 1;
        this.killCount = 0;
        this.spawnCount = 0;
    }

}