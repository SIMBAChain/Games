using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using RestSharp;
public partial class RestoreButton : MonoBehaviour
{
    public string animationName;
    public AudioClip buttonClick;
    public InputField seedField;
    public Text addressLabel;
    public Text seedLabel;
    public virtual IEnumerator OnMouseDown()
    {
        this.transform.parent.GetComponent<Animation>().Play(this.animationName);
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonClick);
        yield return new WaitForSeconds(0.2f);

        if (seedField.text != null)
        {
            GenWallet.WalletFromSeed(seedField.text);
            WalletStorage.SaveWallet();
            addressLabel.text = "Address: " + SimbaInfo.Wallet.GetAccount(0).Address.ToString();
            seedLabel.text = "Recovery Seed: " + SimbaInfo.WalletSeed;
            var scoreContent = GetTransactions.GetSimbaTransactions("/Highscore/?useraddress_contains=" + SimbaInfo.Wallet.GetAccount(0).Address);
            var highscoreJson = JObject.Parse(scoreContent);
            var highscoreArray = JArray.Parse(highscoreJson["results"].ToString());
            var highscoreObject = JObject.Parse(highscoreArray[0].ToString());
            var highscore = highscoreObject["payload"]["inputs"]["score"].ToString();
            PlayerPrefs.SetInt("HighScore", int.Parse(highscore));
           
        }
    }
}
