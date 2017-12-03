using UnityEngine;
using UniRx;
using sgffu.EventMessage;

public class clickToEventIssue : MonoBehaviour {

    public string nextScene;

    /*
        /pp/ Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

        }
    */

    public void OnClick()
    {
        if (nextScene == "") { return; }
        // シーン切り替えイベント発行
        Debug.Log("clickToEventIssue.OnClick: " + nextScene);
        MessageBroker.Default.Publish(new sceneChange { scene_name = nextScene });
    }

}
