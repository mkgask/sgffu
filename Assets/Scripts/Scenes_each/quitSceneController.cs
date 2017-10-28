using UnityEngine;
using UniRx;
using OwrBase.Scene;
using OwrBase.SceneTransition;

public class quitSceneController : MonoBehaviour {
/*
    // Use this for initialization
    void Start()
    {

	}

	// Update is called once per frame
	void Update ()
    {

    }
*/
    public void OnBack()
    {
        //Debug.Log("Event: OnBack: sceneChange: title");
        MessageBroker.Default.Publish(new sceneChange { scene_name = SceneName.title });
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        //Debug.Log("Event: OnQuit: Application.Quit()");
        UnityEditor.EditorApplication.isPlaying = false;
# else
        //Debug.Log("Event: OnQuit: Application.Quit()");
        Application.Quit();
#endif
    }

}
