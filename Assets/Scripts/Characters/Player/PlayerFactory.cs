using UnityEngine;
using OwrBase.Terrain;
using OwrBase.Filesystem;


namespace OwrBase.Characters.Player
{
        
    public class PlayerFactory
    {


        public static void create()
        {
            PlayerConfig player_config = PlayerConfigFactory.loadFile(PlayerConfigFactory.createDefault());
            //Debug.Log("player_config.player_fbx_filepath: " + player_config.player_fbx_filepath);

            if (!((new File()).resourceExist(player_config.player_fbx_filepath + ".prefab")) &&
                    !((new File()).resourceExist(player_config.player_fbx_filepath + ".fbx"))) {
                throw new System.Exception("fieldsSceneController::PlayerCreate(): player_config.player_fbx_filepath file not found.");
            }

            // 本体の召喚
            GameObject go = Object.Instantiate(Resources.Load(player_config.player_fbx_filepath, typeof(GameObject))) as GameObject;
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

}
