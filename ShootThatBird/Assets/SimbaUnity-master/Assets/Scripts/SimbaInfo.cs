using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.HdWallet;
public class SimbaInfo : MonoBehaviour
{

    public static string Url = "https://api.simbachain.com/v1/shootthatbird";
    public static string ApiKey = "dbeabeb2ea87cf2a6306d9e6579a3f1b5fb516dfba3071aee3848cc08f10fe04";
    public static string WalletSeed = "";
    public static Wallet Wallet;
    //bool to check if on circle of life network
    //if isCol is true then the code to check and request funds will be used
    public static bool isCol = false;
}
