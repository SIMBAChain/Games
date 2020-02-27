using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
public partial class AccountButton : MonoBehaviour
{
    public string animationName;
    public AudioClip buttonClick;
    public GameObject addressText;
    public GameObject seedText;
    public GameObject seedField;
    public GameObject restoreButton;

    public Text addressLabel;
    public Text seedLabel;
    public InputField seedInput;
    bool toggle = false;
    public virtual IEnumerator OnMouseDown()
    {
        this.transform.parent.GetComponent<Animation>().Play(this.animationName);
        this.GetComponent<AudioSource>().PlayOneShot(this.buttonClick);
        yield return new WaitForSeconds(0.2f);
        toggle = !toggle;
        addressText.SetActive(toggle);
        seedText.SetActive(toggle);
        seedField.SetActive(toggle);
        restoreButton.SetActive(toggle);

        if (toggle)
        {
            WalletStorage.LoadWallet();
            if (SimbaInfo.Wallet == null)
            {
                GenWallet.RandomWallet();
                WalletStorage.SaveWallet();
            }
            addressLabel.text = "Address: " + SimbaInfo.Wallet.GetAccount(0).Address.ToString();
            seedLabel.text = "Recovery Seed: " + SimbaInfo.WalletSeed;
        }
       

    }

}
