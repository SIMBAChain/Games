using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BirdSpawner : MonoBehaviour
{
    public bool spawn;
    public GameObject bird;
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

        {
            float _7 = Random.Range(-40f, -30f);
            Vector3 _8 = this.transform.position;
            _8.x = _7;
            this.transform.position = _8;
        }

        {
            float _9 = Random.Range(15f, -5f);
            Vector3 _10 = this.transform.position;
            _10.y = _9;
            this.transform.position = _10;
        }
        UnityEngine.Object.Instantiate(this.bird, this.transform.position, this.transform.rotation);
    }

}