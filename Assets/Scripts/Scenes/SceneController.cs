using UnityEngine;
using UniRx;
using sgffu.Scene;
using sgffu.EventMessage;
using sgffu.World;

public class SceneController : MonoBehaviour
{

    public string[] keepOneObjectTags;

    // Use this for initialization
    void Start()
    {
        SceneService.init(this, keepOneObjectTags);
        MessageBroker.Default.Receive<sceneLoad>().Subscribe(x => this.OnSceneLoad(x));
        MessageBroker.Default.Receive<sceneChange>().Subscribe(x => this.OnSceneChange(x));
        MessageBroker.Default.Receive<sceneUnload>().Subscribe(x => this.OnSceneUnload(x));

        MessageBroker.Default.Receive<AllowWorldCreate>().Subscribe(x => this.OnSceneChange(x));
        
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
        Debug.Log("SceneController.OnSceneLoad: next_scene.scene_name: " + next_scene.scene_name);
        SceneService.load(next_scene);
    }

    void OnSceneChange(SceneTransition next_scene)
    {
        Debug.Log("SceneController.OnSceneChange: next_scene.scene_name: " + next_scene.scene_name);
        SceneService.change(next_scene);
    }

    void OnSceneUnload(SceneTransition unload_scene)
    {
        SceneService.unload(unload_scene);
    }
}
