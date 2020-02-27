using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[System.Serializable]
public partial class ThemeMusic : MonoBehaviour
{
    public int musicCount;
    public int musicNo;
    public string objectName;
    public AudioClip themeMusic;
    public GameObject otherMusic;
    public ThemeMusic otherMusicScript;
    public virtual void Update()
    {
        this.musicNo = PlayerPrefs.GetInt("MusicNumber");
        if (this.musicNo == 1)
        {
            this.GetComponent<AudioSource>().mute = true;
        }
        if (this.musicNo == 0)
        {
            this.GetComponent<AudioSource>().mute = false;
        }
        this.otherMusic = GameObject.FindWithTag("Music");
        this.otherMusicScript = (ThemeMusic) this.otherMusic.GetComponent(typeof(ThemeMusic));
        if (this.otherMusicScript.musicCount > this.musicCount)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
        UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
        this.objectName = "GameMusic" + this.musicCount;
        this.gameObject.name = this.objectName;
    }

    public virtual IEnumerator OnLevelWasLoaded(int level)
    {
        this.musicCount = this.musicCount + 1;
        yield return new WaitForSeconds(0.5f);
    }

	/*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("OnSceneLoaded: " + scene.name);
		Debug.Log(mode);
	}*/


    public ThemeMusic()
    {
        this.objectName = "GameMusic" + this.musicCount;
    }

}