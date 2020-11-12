using UnityEngine;
using UnityEngine.UI;
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
        startTime = Time.time;
    }      

    void Update() {
        float fracComplete = (Time.time - startTime) / journeyTime;
    }

    public void LoadSceneOnClick(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
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
        // Scene scene = SceneManager.GetActiveScene(); 
        // SceneManager.LoadScene(scene.name);
        Application.Quit();              
    }      

    public void stopCoroutineOnClick() {
        StopAllCoroutines();               
    }         
}