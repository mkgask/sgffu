using UniRx;
using UnityEngine;
using OwrBase.Input;
//using OwrBase.EventMessage.Actions;

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

            MessageBroker.Default.Receive<InputEvent>().Subscribe(input => {
                switch(input.type) {
                    case Type.Key:
                        switch(input.name) {
                            case Actions.MainAction: if(input.key_value) { chara.OnMainAciton(); } break;
                            case Actions.SubAction: if(input.key_value) { chara.OnSubAction(); } break;
                            case Actions.FocusAction: if(input.key_value) { chara.OnFocusAction(); } break;

                            case Actions.Jump: if(input.key_value) { chara.OnJump(); } break;
                            case Actions.Dash: if(input.key_value) { chara.OnDash(); } else { chara.OffDash(); } break;
                            case Actions.Roll: if(input.key_value) { chara.OnRoll(); } break;
                            case Actions.Reload: if(input.key_value) { chara.OnReload(); } break;
                            case Actions.Crouch: if(input.key_value) { chara.OnCrouch(); } break;
                            case Actions.LayDown: if(input.key_value) { chara.OnLayDown(); } break;
                            
                            case Actions.Up: if(input.key_value) { chara.OnUp(); } else { chara.OffUp(); }  break;
                            case Actions.Down: if(input.key_value) { chara.OnDown(); } else { chara.OffDown(); }  break;
                            case Actions.Left: if(input.key_value) { chara.OnLeft(); } else { chara.OffLeft(); }  break;
                            case Actions.Right: if(input.key_value) { chara.OnRight(); } else { chara.OffRight(); }  break;
                            
                            case Actions.Menu: if(input.key_value) { chara.OnMenuOpen(); } break;
                            case Actions.Inventory: if(input.key_value) { chara.OnInventoryOpen(); } break;
                            case Actions.Status: if(input.key_value) { chara.OnStatusOpen(); } break;
                            case Actions.Skill: if(input.key_value) { chara.OnSkillOpen(); } break;
                            case Actions.Map: if(input.key_value) { chara.OnMapOpen(); } break;
                            
                        }
                        break;
                    case Type.Axis:
                        chara.OnAxis(input.name, input.axis_value);
                        break;
                }
            });

        }

    }
}