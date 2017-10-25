using UnityEngine;
using UniRx;
using OwrBase.World;
using OwrBase.Event;
using OwrBase.Scene;
using OwrBase.Terrain;
using OwrBase.Filesystem;
using OwrBase.Characters.Player;
using OwrBase.SceneTransition;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class fieldsSceneController : MonoBehaviour
{

	void Awake () {
        //StartCoroutine(fieldsInitialize());
        //MessageBroker.Default.Receive<TerrainCreated>().Subscribe(x => Debug.Log("Terrain Created"));
        //MessageBroker.Default.Receive<TerrainCreated>().Subscribe(x => this.WorldCreate());
	}

	// Use this for initialization
	void Start () {
        //StartCoroutine(fieldsInitialize());
        this.WorldCreate();
	}

	// Update is called once per frame
	void Update () {

	}
/*
    IEnumerator fieldsInitialize() {
        yield;
    }
*/



    public void WorldCreate()
    {
        this.TerrainCreate();
        this.PlayerCreate();
    }



    public void TerrainCreate()
    {
        GameObject terrain_gameobject = GameObject.Find("Terrain");
        TerrainConfig terrain_config = TerrainConfigFactory.loadFile(TerrainConfigFactory.createDefault());
        WorldConfig world_config = (SceneService.transition_scene_data as WorldCreated).world_config;
        Debug.Log("World Name: " + world_config.world_name);
        Debug.Log("seed: " + world_config.terrain_seed);

        TerrainService.reset(terrain_gameobject, terrain_config, world_config);
        TerrainService.update(0, 0, terrain_config.texture_filepath);

        Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: reservation");
        MessageBroker.Default.Receive<playerTerrainChunkMove>().Subscribe(x => {
            Debug.Log(StrOpe.i + "Receive: playerTerrainChunkMove: " + x.x + " , " + x.z);
            TerrainService.update(x.x, x.z, terrain_config.texture_filepath);
        });

        MessageBroker.Default.Publish(new TerrainCreated{});
    }



    public void PlayerCreate()
    {
        PlayerConfig player_config = PlayerConfigFactory.loadFile(PlayerConfigFactory.createDefault());

        if (!((new File()).exist(player_config.player_fbx_filepath))) {
            throw new System.Exception("fieldsSceneController::PlayerCreate(): player_config.player_fbx_filepath file not found.");
        }

        // 本体の召喚
        GameObject go = Instantiate(Resources.Load(player_config.player_fbx_filepath, typeof(GameObject))) as GameObject;
        go.tag = "Player";

        // 当たり判定設定
        CapsuleCollider cc = go.AddComponent<CapsuleCollider>();
        cc.center = new Vector3(0f, 0.7f, 0f);
        cc.radius = 0.35f;
        cc.height = 1.6f;

        // 衝突判定設定
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // カメラ設置
        Camera ca = Camera.main;
        ca.transform.position = new Vector3(0f, 1.5f, -3f);
        ca.transform.parent = go.transform;

        // キャラ位置の設定
        float center_y = TerrainService.getHeight(0f, 0f);
        go.transform.position = new Vector3(0f, center_y + 1f, 0f);
    }

}
