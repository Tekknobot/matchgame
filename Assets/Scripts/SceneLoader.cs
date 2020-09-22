using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoader : MonoBehaviour {
 
    public void LoadSceneOnClick(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void clearSamplesOnClick() {
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().chopTime.Clear();
        GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>().song.Clear();               
    }      
 
}