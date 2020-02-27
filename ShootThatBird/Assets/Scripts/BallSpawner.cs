using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BallSpawner : MonoBehaviour
{
    public bool spawn;
    public GameObject ball;
    public virtual void Update()
    {
        if (this.spawn)
        {
            this.spawn = false;
            UnityEngine.Object.Instantiate(this.ball, this.transform.position, this.transform.rotation);
        }
    }

}