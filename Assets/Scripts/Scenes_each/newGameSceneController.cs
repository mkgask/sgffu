using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using OwrBase.Config;
using OwrBase.Utility;
using OwrBase.Scene;
using OwrBase.SceneTransition;
using OwrBase.World;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class newGameSceneController : MonoBehaviour {
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
    public void Start()
    {
        /* MessageBroker.Default.Publish(new sceneChange { sceneName = "fields" }); */
    }



    public void OnWorldCreate()
    {
        // Inputから新規World名を取得
        GameObject world_name_object = GameObject.FindGameObjectWithTag("NewWorldName");
        InputField world_name_component = world_name_object.GetComponent<InputField>();
        string world_name = world_name_component.text;

        // world/新規World名.jsonの存在チェック
        // 存在したらエラー生成して関数終了
        if (ConfigFile.exist(StrOpe.i + "/Configs/Worlds/" + world_name + ".json")) {
            GameObject world_name_err_obj = GameObject.FindGameObjectWithTag("WorldNameExistErrorMessage");
            Text err_text = world_name_err_obj.GetComponent<Text>();
            err_text.text = "このワールド名は既に存在します。";
            Debug.Log("既に存在するワールド名です: " + world_name);
            return;
        }

        // World名からシード値生成
        uint seed = Rand.xorshift(world_name);
        //Debug.Log("seed: " + seed);
/*
        // world/新規World名.jsonにWorld名とシード値を保存
        ConfigData.worldConfig = new WorldConfig {
            world_name = world_name,
            terrain_seed = seed
        };
*/
        //MessageBroker.Default.Publish(new WorldCreateStart { world_config = new WorldConfig{} });
        MessageBroker.Default.Publish(new WorldCreated {
            scene_name = SceneName.fields,
            world_config = WorldConfigFactory.create(world_name, seed)
        });
    }


}
