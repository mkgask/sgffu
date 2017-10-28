using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UniRx;
using OwrBase.EventMessage;

namespace OwrBase.Input {
//using Messanger = OwrBase.Messanger.Messanger;

    public class Service
    {

        private static Dictionary<string, KeyCode> keys;

        private static string in_enter_key = "";


        public static void init()
        {
            keys = InputConfigFactory.loadFile(InputConfigFactory.createDefault());
        }

        public static void reload(Dictionary<string, KeyCode> key_settings) {
            keys = key_settings;
            /*
            Messanger.x.Clear();
            foreach (KeyValuePair<string, KeyCode> pair in keys) {
                Messanger.x[pair.Key] = new MessageBroker();
            }
            */
        }

        public static void inputCheck()
        {
            //keys = InputConfigFactory.loadFile(InputConfigFactory.createDefault());

            foreach (KeyValuePair<string, KeyCode> pair in keys) {
                if (UnityEngine.Input.GetKeyDown(pair.Value)) {

                    switch(pair.Key) {
                        case Actions.MainAction:
                            if (in_enter_key == Actions.MainAction) { return; }
                            //Debug.Log("Publish: MainActionKeyDown");
                            MessageBroker.Default.Publish(new MainActionKeyDown {});
                            in_enter_key = Actions.MainAction;
                            break;
                        case Actions.SubAction:
                            if (in_enter_key == Actions.SubAction) { return; }
                            //Debug.Log("Publish: SubActionKeyDown");
                            MessageBroker.Default.Publish(new SubActionKeyDown {});
                            in_enter_key = Actions.SubAction;
                            break;
                        case Actions.FocusAction:
                            if (in_enter_key == Actions.FocusAction) { return; }
                            //Debug.Log("Publish: FocusActionKeyDown");
                            MessageBroker.Default.Publish(new FocusActionKeyDown {});
                            in_enter_key = Actions.FocusAction;
                            break;
                        case Actions.Jump:
                            if (in_enter_key == Actions.Jump) { return; }
                            //Debug.Log("Publish: JumpKeyDown");
                            MessageBroker.Default.Publish(new JumpKeyDown {});
                            in_enter_key = Actions.Jump;
                            break;
                        case Actions.Dash:
                            if (in_enter_key == Actions.Dash) { return; }
                            //Debug.Log("Publish: DashKeyDown");
                            MessageBroker.Default.Publish(new DashKeyDown {});
                            in_enter_key = Actions.Dash;
                            break;
                        case Actions.Roll:
                            if (in_enter_key == Actions.Roll) { return; }
                            //Debug.Log("Publish: RollKeyDown");
                            MessageBroker.Default.Publish(new RollKeyDown {});
                            in_enter_key = Actions.Roll;
                            break;
                        case Actions.Reload:
                            if (in_enter_key == Actions.Reload) { return; }
                            //Debug.Log("Publish: ReloadKeyDown");
                            MessageBroker.Default.Publish(new ReloadKeyDown {});
                            in_enter_key = Actions.Reload;
                            break;
                        case Actions.Crouch:
                            if (in_enter_key == Actions.Crouch) { return; }
                            //Debug.Log("Publish: CrouchKeyDown");
                            MessageBroker.Default.Publish(new CrouchKeyDown {});
                            in_enter_key = Actions.Crouch;
                            break;
                        case Actions.LayDown:
                            if (in_enter_key == Actions.LayDown) { return; }
                            //Debug.Log("Publish: LayDownKeyDown");
                            MessageBroker.Default.Publish(new LayDownKeyDown {});
                            in_enter_key = Actions.LayDown;
                            break;
                        case Actions.Up:
                            if (in_enter_key == Actions.Up) { return; }
                            //Debug.Log("Publish: UpKeyDown");
                            MessageBroker.Default.Publish(new UpKeyDown {});
                            in_enter_key = Actions.Up;
                            break;
                        case Actions.Down:
                            if (in_enter_key == Actions.Down) { return; }
                            //Debug.Log("Publish: DownKeyDown");
                            MessageBroker.Default.Publish(new DownKeyDown {});
                            in_enter_key = Actions.Down;
                            break;
                        case Actions.Left:
                            if (in_enter_key == Actions.Left) { return; }
                            //Debug.Log("Publish: LeftKeyDown");
                            MessageBroker.Default.Publish(new LeftKeyDown {});
                            in_enter_key = Actions.Left;
                            break;
                        case Actions.Right:
                            if (in_enter_key == Actions.Right) { return; }
                            //Debug.Log("Publish: RightKeyDown");
                            MessageBroker.Default.Publish(new RightKeyDown {});
                            in_enter_key = Actions.Right;
                            break;
                        case Actions.Menu:
                            if (in_enter_key == Actions.Menu) { return; }
                            //Debug.Log("Publish: MenuKeyDown");
                            MessageBroker.Default.Publish(new MenuKeyDown {});
                            in_enter_key = Actions.Menu;
                            break;
                        case Actions.Inventory:
                            if (in_enter_key == Actions.Inventory) { return; }
                            //Debug.Log("Publish: InventoryKeyDown");
                            MessageBroker.Default.Publish(new InventoryKeyDown {});
                            in_enter_key = Actions.Inventory;
                            break;
                        case Actions.Status:
                            if (in_enter_key == Actions.Status) { return; }
                            //Debug.Log("Publish: StatusKeyDown");
                            MessageBroker.Default.Publish(new StatusKeyDown {});
                            in_enter_key = Actions.Status;
                            break;
                        case Actions.Skill:
                            if (in_enter_key == Actions.Skill) { return; }
                            //Debug.Log("Publish: SkillKeyDown");
                            MessageBroker.Default.Publish(new SkillKeyDown {});
                            in_enter_key = Actions.Skill;
                            break;
                        case Actions.Map:
                            if (in_enter_key == Actions.Map) { return; }
                            //Debug.Log("Publish: MapKeyDown");
                            MessageBroker.Default.Publish(new MapKeyDown {});
                            in_enter_key = Actions.Map;
                            break;
                        default:
                            break;
                    }
                }
                if (UnityEngine.Input.GetKeyUp(pair.Value)) {

                    switch(pair.Key) {
                        case Actions.MainAction:
                            MessageBroker.Default.Publish(new MainActionKeyUp {});
                            break;
                        case Actions.SubAction:
                            MessageBroker.Default.Publish(new SubActionKeyUp {});
                            break;
                        case Actions.FocusAction:
                            MessageBroker.Default.Publish(new FocusActionKeyUp {});
                            break;
                        case Actions.Jump:
                            MessageBroker.Default.Publish(new JumpKeyUp {});
                            break;
                        case Actions.Dash:
                            MessageBroker.Default.Publish(new DashKeyUp {});
                            break;
                        case Actions.Roll:
                            MessageBroker.Default.Publish(new RollKeyUp {});
                            break;
                        case Actions.Reload:
                            MessageBroker.Default.Publish(new ReloadKeyUp {});
                            break;
                        case Actions.Crouch:
                            MessageBroker.Default.Publish(new CrouchKeyUp {});
                            break;
                        case Actions.LayDown:
                            MessageBroker.Default.Publish(new LayDownKeyUp {});
                            break;
                        case Actions.Up:
                            MessageBroker.Default.Publish(new UpKeyUp {});
                            break;
                        case Actions.Down:
                            MessageBroker.Default.Publish(new DownKeyUp {});
                            break;
                        case Actions.Left:
                            MessageBroker.Default.Publish(new LeftKeyUp {});
                            break;
                        case Actions.Right:
                            MessageBroker.Default.Publish(new RightKeyUp {});
                            break;
                        case Actions.Menu:
                            MessageBroker.Default.Publish(new MenuKeyUp {});
                            break;
                        case Actions.Inventory:
                            MessageBroker.Default.Publish(new InventoryKeyUp {});
                            break;
                        case Actions.Status:
                            MessageBroker.Default.Publish(new StatusKeyUp {});
                            break;
                        case Actions.Skill:
                            MessageBroker.Default.Publish(new SkillKeyUp {});
                            break;
                        case Actions.Map:
                            MessageBroker.Default.Publish(new MapKeyUp {});
                            break;
                        default:
                            break;
                    }

                    in_enter_key = "";

/*
                    Messanger.x[pair.Key].Publish(new System.Object {
                        name = pair.Key,
                        code = pair.Value
                    });
*/
/*
                    Type class_type = Type.GetType(pair.Key);
                    var keyEvent = new {
                        name = pair.Key,
                        code = pair.Value
                    };
                    //Debug.Log("keyEvent.name: " + keyEvent.name);
                    //Debug.Log("keyEvent.code: " + keyEvent.code);
                    MessageBroker.Default.Publish(keyEvent);
*/
                }
            }
        }

    }

}
