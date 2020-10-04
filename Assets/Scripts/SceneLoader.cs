using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoader : MonoBehaviour {

    public GameObject panels;
    public GameObject snow;

    public AudioSource audioSource;

    public Vector3 startPoint;
    public Vector3 endPoint;  

    public float journeyTime = 3f;
    private float startTime;
    float fracComplete;

    void Start()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;
    }      

    void Update() {
        float fracComplete = (Time.time - startTime) / journeyTime;
    }

    public void LoadGameOnClick() {
        panels.transform.position = Vector3.Slerp(startPoint, endPoint, fracComplete);       
        snow.SetActive(false);
    }

    public void LoadOperatorOnClick() {
        panels.transform.position = Vector3.Slerp(startPoint, endPoint, fracComplete);       
        snow.SetActive(false);
        audioSource.Stop();
    }    

    public void LoadSamplerOnClick() {
        panels.transform.position = Vector3.Slerp(startPoint, endPoint, fracComplete);
        snow.SetActive(true);
    }  

    public void clearSamplesOnClick() {
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().chopTime.Clear();
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().song.Clear();               
    }      

    public void stopCoroutineOnClick() {
        StopAllCoroutines();               
    }         
}