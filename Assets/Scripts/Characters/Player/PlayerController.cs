using UnityEngine;
using UniRx;
using OwrBase.EventMessage;
using OwrBase.Terrain;
using OwrBase.Filesystem;
using OwrBase.Characters;
using OwrBase.Characters.Input;
using OwrBase.Characters.Player;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class PlayerController : MonoBehaviour, IControlCharacter
{
    PlayerInputControl input_controller;

    private bool in_action = false;

    private bool window_open = false;

    private bool dash_key = false;
    private bool up_key = false;
    private bool down_key = false;
    private bool left_key = false;
    private bool right_key = false;



    private int terrain_chunk_size = 1;

    private int terrain_chunk_x = 0;
    private int terrain_chunk_z = 0;



    float player_move_speed_front = 0.2f;
    float player_move_speed_back = -0.1f;
    float player_move_speed_left = -0.2f;
    float player_move_speed_right = 0.2f;
    float player_dash_speed_front = 1f;
    float player_dash_speed_back = -0.8f;
    float player_dash_speed_left = -1f;
    float player_dash_speed_right = 1f;



	// Use this for initialization
	public void Start () {
        input_controller = new PlayerInputControl(this);
        this.terrain_chunk_size = TerrainService.terrain_config.chunk_size;
        //Log.write(StrOpe.i + "terrain_chunk_size: " + this.terrain_chunk_size);
	}
	
	// Update is called once per frame
	public void Update () {
		if (this.window_open) { return; }

        Vector3 char_move = this.updatePreInput();
        this.updatePostCharaMove(char_move);
	}

    private Vector3 updatePreInput() {
        Vector3 move = new Vector3(0f, 0f, 0f);

        if (this.up_key) {
            move.z += this.dash_key ? player_dash_speed_front : player_move_speed_front;
        }
        if (this.down_key) {
            move.z += this.dash_key ? player_dash_speed_back : player_move_speed_back;
        }
        if (this.left_key) {
            move.x = this.dash_key ? player_dash_speed_left : player_move_speed_left;
        }
        if (this.right_key) {
            move.x = this.dash_key ? player_dash_speed_right : player_move_speed_right;
        }
        if (((move.z < 0f) || (0f < move.z)) && ((move.x < 0f) || (0f < move.x))) {
            move = new Vector3(move.x * 0.7f, 0f, move.z * 0.7f);
        }

        return move;
    }

    private void updatePostCharaMove(Vector3 move) {
        this.transform.position += move;

        int chunk_x = (int)(this.transform.position.x / this.terrain_chunk_size);
        int chunk_z = (int)(this.transform.position.z / this.terrain_chunk_size);
        if (terrain_chunk_x != chunk_x || terrain_chunk_z != chunk_z) {
            Debug.Log(StrOpe.i + "updatePostCharaMove: " + chunk_x + " , " + chunk_z + " : " + this.terrain_chunk_x + " , " + this.terrain_chunk_z);
            Log.write(StrOpe.i + "updatePostCharaMove: " + chunk_x + " , " + chunk_z + " : " + this.terrain_chunk_x + " , " + this.terrain_chunk_z);
            //Log.write("Publish: playerTerrainChunkMove");
            MessageBroker.Default.Publish<playerTerrainChunkMove>(new playerTerrainChunkMove {
                x = chunk_x,
                z = chunk_z
            });
        }
        this.terrain_chunk_x = chunk_x;
        this.terrain_chunk_z = chunk_z;
    }

    public void OnMainAciton() 
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnSubAction()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnFocusAction()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnJump()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnDash()
    {
        this.dash_key = true;
    }

    public void OnRoll()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnReload()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnCrouch()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnLayDown()
    {
        if(this.in_action) { return; }
        this.in_action = true;
        this.in_action = false;
    }

    public void OnUp()
    {
        this.up_key = true;
    }

    public void OnDown()
    {
        this.down_key = true;
    }

    public void OnLeft()
    {
        this.left_key = true;
    }

    public void OnRight()
    {
        this.right_key = true;
    }

    public void OnMenuOpen()
    {
        if(this.window_open) { return; }
        this.window_open = true;
        this.window_open = false;
    }

    public void OnInventoryOpen()
    {
        if(this.window_open) { return; }
        this.window_open = true;
        this.window_open = false;
    }

    public void OnStatusOpen()
    {
        if(this.window_open) { return; }
        this.window_open = true;
        this.window_open = false;
    }

    public void OnSkillOpen()
    {
        if(this.window_open) { return; }
        this.window_open = true;
        this.window_open = false;
    }

    public void OnMapOpen()
    {
        if(this.window_open) { return; }
        this.window_open = true;
        this.window_open = false;
    }




    public void OffDash()
    {
        this.dash_key = false;
    }

    public void OffUp()
    {
        this.up_key = false;
    }

    public void OffDown()
    {
        this.down_key = false;
    }

    public void OffLeft()
    {
        this.left_key = false;
    }

    public void OffRight()
    {
        this.right_key = false;
    }

}
