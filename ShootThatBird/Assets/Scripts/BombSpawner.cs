using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BombSpawner : MonoBehaviour
{
    public bool spawn;
    public GameObject bomb;
    public virtual void Update()
    {
        if (this.spawn == true)
        {
            this.spawn = false;
            this.Spawn();
        }
    }

    public virtual void Spawn()
    {
        UnityEngine.Object.Instantiate(this.bomb, this.transform.position, this.transform.rotation);
    }

}