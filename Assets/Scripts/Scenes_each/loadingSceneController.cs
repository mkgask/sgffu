using UnityEngine;
using UniRx;
using UnityEngine.UI;
using sgffu.EventMessage;

public class loadingSceneController : MonoBehaviour {

    public GameObject progress_object;

    private Text progress_text;

    public int progress_point = 0;

    public int progress_max = 100;

    // Use this for initialization
    void Start () {
        progress_text = progress_object.GetComponent<Text>();

        MessageBroker.Default.Receive<ProgressStatus>().Subscribe(x => {
            progress_point += 1;
            progress_text.text = progress_point + "/" + progress_max;
        });
    }

/*
    // Update is called once per frame
    void Update () {

    }
*/
}
