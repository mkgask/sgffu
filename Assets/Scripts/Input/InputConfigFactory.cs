using System.Collections.Generic;
using UnityEngine;
using OwrBase.Config;

namespace OwrBase.Input {

    public class InputConfigFactory
    {
        
        public static Dictionary<string, KeyCode> createDefault()
        {
            return new Dictionary<string, KeyCode>() {
                { Actions.MainAction, KeyCode.Mouse0 },
                { Actions.SubAction, KeyCode.Mouse1 },
                { Actions.FocusAction, KeyCode.E },

                { Actions.Jump, KeyCode.Space },
                { Actions.Dash, KeyCode.LeftShift },
                { Actions.Roll, KeyCode.G },
                { Actions.Reload, KeyCode.V },
                { Actions.Crouch, KeyCode.LeftControl },
                { Actions.LayDown, KeyCode.Q },
                
                { Actions.Up, KeyCode.W },
                { Actions.Down, KeyCode.S },
                { Actions.Left, KeyCode.A },
                { Actions.Right, KeyCode.D },
                
                { Actions.Menu, KeyCode.Escape },
                { Actions.Inventory, KeyCode.Tab },
                { Actions.Status, KeyCode.B },
                { Actions.Skill, KeyCode.N },
                { Actions.Map, KeyCode.M }
            };
        }

        public static Dictionary<string, KeyCode> loadFile(Dictionary<string, KeyCode> input_config_default)
        {
            return ConfigFile.load<Dictionary<string, KeyCode>>(ConfigFile.inputConfigFilename, input_config_default);
        }
    }

}