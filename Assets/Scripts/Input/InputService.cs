using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UniRx;
using sgffu.EventMessage;

namespace sgffu.Input {

    public class Service
    {

        private static Dictionary<string, InputEntity> entities;

        private static Dictionary<string, float> old_axis = new Dictionary<string, float> {
            { Mouse.X, 0f },
            { Mouse.Y, 0f },
            { Mouse.Wheel, 0f },
        };

        private static string in_enter_key = "";


        public static void init()
        {
            entities = InputConfigFactory.loadFile(InputConfigFactory.createDefault());
        }

        public static void reload(Dictionary<string, InputEntity> load_entities) {
            entities = load_entities;
        }

        public static void inputCheck()
        {
            foreach (KeyValuePair<string, InputEntity> pair in entities) {
                switch(pair.Value.type) {
                    case Type.Key:
                        if (pair.Value.key_code.Count < 1) { continue; }

                        foreach (KeyCode key in pair.Value.key_code) {
                            if (UnityEngine.Input.GetKeyDown(key)) {
                                if (in_enter_key == pair.Value.name) { continue; }
                                MessageBroker.Default.Publish(new InputEvent {
                                    name = pair.Value.name,
                                    key_code = pair.Value.key_code,
                                    type = pair.Value.type,
                                    key_value = true,
                                });
                                in_enter_key = pair.Value.name;
                            }
                            if (UnityEngine.Input.GetKeyUp(key)) {
                                MessageBroker.Default.Publish(new InputEvent {
                                    name = pair.Value.name,
                                    key_code = pair.Value.key_code,
                                    type = pair.Value.type,
                                    key_value = false,
                                });
                                in_enter_key = "";
                            }
                        }
                        
                        break;
                    case Type.Axis:
                        float value = UnityEngine.Input.GetAxis(pair.Value.name);

                        if (old_axis[pair.Value.name] != value) {
                            MessageBroker.Default.Publish(new InputEvent {
                                name = pair.Value.name,
                                key_code = pair.Value.key_code,
                                type = pair.Value.type,
                                axis_value = value,
                            });
                            old_axis[pair.Value.name] = value;
                        }
                        break;
                }
            }
        }

    }

}
