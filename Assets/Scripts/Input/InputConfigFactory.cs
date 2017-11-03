using System.Collections.Generic;
using UnityEngine;
using OwrBase.Config;

namespace OwrBase.Input {

    public class InputConfigFactory
    {
        
        public static Dictionary<string, InputEntity> createDefault()
        {
            return new Dictionary<string, InputEntity>() {
                { Actions.MainAction, new InputEntity {
                    name = Actions.MainAction,
                    key_code = new List<KeyCode> { KeyCode.Mouse0, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.SubAction, new InputEntity {
                    name = Actions.SubAction,
                    key_code = new List<KeyCode> { KeyCode.Mouse1, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.FocusAction, new InputEntity {
                    name = Actions.FocusAction,
                    key_code = new List<KeyCode> { KeyCode.E, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                
                { Actions.Jump, new InputEntity {
                    name = Actions.Jump,
                    key_code = new List<KeyCode> { KeyCode.Space, KeyCode.None, KeyCode.None },
                    type = Type.Key
                }},
                { Actions.Dash, new InputEntity {
                    name = Actions.Dash,
                    key_code = new List<KeyCode> { KeyCode.LeftShift, KeyCode.RightShift, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Roll, new InputEntity {
                    name = Actions.Roll,
                    key_code = new List<KeyCode> { KeyCode.G, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Reload, new InputEntity {
                    name = Actions.Reload,
                    key_code = new List<KeyCode> { KeyCode.V, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Crouch, new InputEntity {
                    name = Actions.Crouch,
                    key_code = new List<KeyCode> { KeyCode.LeftControl, KeyCode.RightControl, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.LayDown, new InputEntity {
                    name = Actions.LayDown,
                    key_code = new List<KeyCode> { KeyCode.Q, KeyCode.None, KeyCode.None },
                    type = Type.Key,
                }},
                
                { Actions.Up, new InputEntity {
                    name = Actions.Up,
                    key_code = new List<KeyCode> { KeyCode.W, KeyCode.UpArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Down, new InputEntity {
                    name = Actions.Down,
                    key_code = new List<KeyCode> { KeyCode.S, KeyCode.DownArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Left, new InputEntity {
                    name = Actions.Left,
                    key_code = new List<KeyCode> { KeyCode.A, KeyCode.LeftArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Right, new InputEntity {
                    name = Actions.Right,
                    key_code = new List<KeyCode> { KeyCode.D, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},
                
                { Actions.Menu, new InputEntity {
                    name = Actions.Menu,
                    key_code = new List<KeyCode> { KeyCode.Escape, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Inventory, new InputEntity {
                    name = Actions.Inventory,
                    key_code = new List<KeyCode> { KeyCode.Tab, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Status, new InputEntity {
                    name = Actions.Status,
                    key_code = new List<KeyCode> { KeyCode.B, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Skill, new InputEntity {
                    name = Actions.Skill,
                    key_code = new List<KeyCode> { KeyCode.N, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},
                { Actions.Map, new InputEntity {
                    name = Actions.Map,
                    key_code = new List<KeyCode> { KeyCode.M, KeyCode.RightArrow, KeyCode.None },
                    type = Type.Key,
                }},

                { Input.Mouse.X, new InputEntity {
                    name = Input.Mouse.X,
                    type = Type.Axis,
                }},
                { Input.Mouse.Y, new InputEntity {
                    name = Input.Mouse.Y,
                    type = Type.Axis,
                }},
                { Input.Mouse.Wheel, new InputEntity {
                    name = Input.Mouse.Wheel,
                    type = Type.Axis,
                }},
            };
        }

        public static Dictionary<string, InputEntity> loadFile(Dictionary<string, InputEntity> input_config_default)
        {
            return ConfigFile.load<Dictionary<string, InputEntity>>(ConfigFile.inputConfigFilename, input_config_default);
        }
    }

}