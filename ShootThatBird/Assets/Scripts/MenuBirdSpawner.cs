using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MenuBirdSpawner : MonoBehaviour
{
    public bool spawn;
    public GameObject bird;
    public float timer;
    public float spawnGap;
    public float maxTime;
    public virtual void Update()
    {
        this.timer = this.timer + Time.deltaTime;
        if (this.timer >= this.maxTime)
        {
            this.timer = 0;
            this.maxTime = this.spawnGap = this.spawnGap + Random.Range(-2f, 2f);
            this.Spawn();
        }
    }

    public virtual void Spawn()
    {

        {
            float _17 = Random.Range(-35f, -21f);
            Vector3 _18 = this.transform.position;
            _18.x = _17;
            this.transform.position = _18;
        }

        {
            float _19 = Random.Range(15f, -5f);
            Vector3 _20 = this.transform.position;
            _20.y = _19;
            this.transform.position = _20;
        }
        UnityEngine.Object.Instantiate(this.bird, this.transform.position, this.transform.rotation);
    }

}