using UniRx;
using UnityEngine;
using sgffu.Input;
using sgffu.Characters;
using sgffu.EventMessage;

namespace sgffu.Characters.Input {

    public class PlayerInputControlAction
    {
        IControlCharacterAction target;

        public PlayerInputControlAction(IControlCharacterAction target) {
            this.target = target;

            MessageBroker.Default.Receive<InputEvent>().Subscribe(input => {
                switch(input.type) {
                    case Type.Key:
                        switch(input.name) {
                            case Actions.MainAction: if(input.key_value) { target.OnMainAciton(); } break;
                            case Actions.SubAction: if(input.key_value) { target.OnSubAction(); } break;
                            case Actions.FocusAction: if(input.key_value) { target.OnFocusAction(); } break;

                            case Actions.Jump: if(input.key_value) { target.OnJump(); } break;
                            case Actions.Dash: if(input.key_value) { target.OnDash(); } else { target.OffDash(); } break;
                            case Actions.Roll: if(input.key_value) { target.OnRoll(); } break;
                            case Actions.Reload: if(input.key_value) { target.OnReload(); } break;
                            case Actions.Crouch: if(input.key_value) { target.OnCrouch(); } break;
                            case Actions.LayDown: if(input.key_value) { target.OnLayDown(); } break;
                            
                            case Actions.Up: if(input.key_value) { target.OnUp(); } else { target.OffUp(); }  break;
                            case Actions.Down: if(input.key_value) { target.OnDown(); } else { target.OffDown(); }  break;
                            case Actions.Left: if(input.key_value) { target.OnLeft(); } else { target.OffLeft(); }  break;
                            case Actions.Right: if(input.key_value) { target.OnRight(); } else { target.OffRight(); }  break;
                            
                            case Actions.Menu: if(input.key_value) { target.OnMenuOpen(); } break;
                            case Actions.Inventory: if(input.key_value) { target.OnInventoryOpen(); } break;
                            case Actions.Status: if(input.key_value) { target.OnStatusOpen(); } break;
                            case Actions.Skill: if(input.key_value) { target.OnSkillOpen(); } break;
                            case Actions.Map: if(input.key_value) { target.OnMapOpen(); } break;
                            
                        }
                        break;
                }
            });

        }
    }

    public class PlayerInputControlAxis
    {
        IControlCharacterAxis target;

        public PlayerInputControlAxis(IControlCharacterAxis target) {
            this.target = target;

            MessageBroker.Default.Receive<InputEvent>().Subscribe(input => {
                switch(input.type) {
                    case Type.Axis:
                        target.OnAxis(input.name, input.axis_value);
                        break;
                }
            });

        }

    }
}