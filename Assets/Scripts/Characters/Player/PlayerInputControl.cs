using UniRx;
using UnityEngine;
using OwrBase.SceneTransition;

namespace OwrBase.Characters.Input {

    public class PlayerInputControl
    {

        private bool main_action_key = false;
        private bool sub_action_key = false;
        private bool focus_action_key = false;

        private bool jump_key = false;
        private bool dash_key = false;
        private bool roll_key = false;
        private bool reload_key = false;
        private bool crouch_key = false;
        private bool laydown_key = false;

        private bool up_key = false;
        private bool down_key = false;
        private bool left_key = false;
        private bool right_key = false;

        private bool menu_key = false;
        private bool inventory_key = false;
        private bool status_key = false;
        private bool skill_key = false;
        private bool map_key = false;


        IControlCharacter chara;

        public PlayerInputControl(IControlCharacter chara) {
            this.chara = chara;

            MessageBroker.Default.Receive<MainActionKeyDown>().Subscribe(x => {if(!this.main_action_key) { chara.OnMainAciton(); this.main_action_key = true;}});
            MessageBroker.Default.Receive<SubActionKeyDown>().Subscribe(x => {if(!this.sub_action_key) { chara.OnSubAction(); this.sub_action_key = true;}});
            MessageBroker.Default.Receive<FocusActionKeyDown>().Subscribe(x => {if(!this.focus_action_key) { chara.OnFocusAction(); this.focus_action_key = true;}});

            MessageBroker.Default.Receive<JumpKeyDown>().Subscribe(x => {if(!this.jump_key) { chara.OnJump(); this.jump_key = true; }});
            MessageBroker.Default.Receive<DashKeyDown>().Subscribe(x => {if(!this.dash_key) { chara.OnDash(); this.dash_key = true; }});
            MessageBroker.Default.Receive<RollKeyDown>().Subscribe(x => {if(!this.roll_key) { chara.OnRoll(); this.roll_key = true; }});
            MessageBroker.Default.Receive<ReloadKeyDown>().Subscribe(x => {if(!this.reload_key) { chara.OnReload(); this.reload_key = true; }});
            MessageBroker.Default.Receive<CrouchKeyDown>().Subscribe(x => {if(!this.crouch_key) { chara.OnCrouch(); this.crouch_key = true; }});
            MessageBroker.Default.Receive<LayDownKeyDown>().Subscribe(x => {if(!this.laydown_key) { chara.OnLayDown(); this.laydown_key = true; }});

            MessageBroker.Default.Receive<UpKeyDown>().Subscribe(x => {if(!this.up_key) { chara.OnUp(); this.up_key = true; }});
            MessageBroker.Default.Receive<DownKeyDown>().Subscribe(x => {if(!this.down_key) { chara.OnDown(); this.down_key = true; }});
            MessageBroker.Default.Receive<LeftKeyDown>().Subscribe(x => {if(!this.left_key) { chara.OnLeft(); this.left_key = true; }});
            MessageBroker.Default.Receive<RightKeyDown>().Subscribe(x => {if(!this.right_key) { chara.OnRight(); this.right_key = true; }});

            MessageBroker.Default.Receive<MenuKeyDown>().Subscribe(x => {if(!this.menu_key) { chara.OnRight(); this.menu_key = true; }});
            MessageBroker.Default.Receive<InventoryKeyDown>().Subscribe(x => {if(!this.inventory_key) { chara.OnRight(); this.inventory_key = true; }});
            MessageBroker.Default.Receive<StatusKeyDown>().Subscribe(x => {if(!this.status_key) { chara.OnRight(); this.status_key = true; }});
            MessageBroker.Default.Receive<SkillKeyDown>().Subscribe(x => {if(!this.skill_key) { chara.OnRight(); this.skill_key = true; }});
            MessageBroker.Default.Receive<MapKeyDown>().Subscribe(x => {if(!this.map_key) { chara.OnRight(); this.map_key = true; }});

            MessageBroker.Default.Receive<MainActionKeyUp>().Subscribe(x => this.main_action_key = false);
            MessageBroker.Default.Receive<SubActionKeyUp>().Subscribe(x => this.sub_action_key = false);
            MessageBroker.Default.Receive<FocusActionKeyUp>().Subscribe(x => this.focus_action_key = false);

            MessageBroker.Default.Receive<JumpKeyUp>().Subscribe(x => this.jump_key = false);
            MessageBroker.Default.Receive<DashKeyUp>().Subscribe(x => {chara.OffDash(); this.dash_key = false;});
            MessageBroker.Default.Receive<RollKeyUp>().Subscribe(x => this.roll_key = false);
            MessageBroker.Default.Receive<ReloadKeyUp>().Subscribe(x => this.reload_key = false);
            MessageBroker.Default.Receive<CrouchKeyUp>().Subscribe(x => this.crouch_key = false);
            MessageBroker.Default.Receive<LayDownKeyUp>().Subscribe(x => this.laydown_key = false);

            MessageBroker.Default.Receive<UpKeyUp>().Subscribe(x => {chara.OffUp(); this.up_key = false;});
            MessageBroker.Default.Receive<DownKeyUp>().Subscribe(x => {chara.OffDown(); this.down_key = false;});
            MessageBroker.Default.Receive<LeftKeyUp>().Subscribe(x => {chara.OffLeft(); this.left_key = false;});
            MessageBroker.Default.Receive<RightKeyUp>().Subscribe(x => {chara.OffRight(); this.right_key = false;});

            MessageBroker.Default.Receive<MenuKeyUp>().Subscribe(x => this.menu_key = false);
            MessageBroker.Default.Receive<InventoryKeyUp>().Subscribe(x => this.inventory_key = false);
            MessageBroker.Default.Receive<StatusKeyUp>().Subscribe(x => this.status_key = false);
            MessageBroker.Default.Receive<SkillKeyUp>().Subscribe(x => this.skill_key = false);
            MessageBroker.Default.Receive<MapKeyUp>().Subscribe(x => this.map_key = false);
        }

    }
}