using UnityEngine;
using UniRx;
using OwrBase.Scene;
using OwrBase.EventMessage;
using OwrBase.World;

public class SceneController : MonoBehaviour
{

    public string[] keepOneObjectTags;

    // Use this for initialization
    void Start()
    {
        SceneService.init(this, keepOneObjectTags);
        MessageBroker.Default.Receive<sceneLoad>().Subscribe(x => this.OnSceneLoad(x));
        MessageBroker.Default.Receive<sceneChange>().Subscribe(x => this.OnSceneChange(x));

        MessageBroker.Default.Receive<WorldCreated>().Subscribe(x => this.OnSceneChange(x));
        
        MessageBroker.Default.Publish(new sceneLoad { scene_name = SceneName.title });
    }

    /*
        // Update is called once per frame
        void Update()
        {
        }
    */

    void OnSceneLoad(SceneTransition next_scene)
    {
        //Debug.Log("Event: Scene Load: " + sceneName);
        SceneService.load(next_scene);
    }

    void OnSceneChange(SceneTransition next_scene)
    {
        //Debug.Log("Event: Scene Change: " + sceneName);
        SceneService.change(next_scene);
    }
}
