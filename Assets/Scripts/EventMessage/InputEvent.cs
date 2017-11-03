using UnityEngine;
using System.Collections.Generic;

namespace OwrBase.Input {
    public class InputEvent {

        public string name = "";

        public List<KeyCode> key_code = new List<KeyCode> { KeyCode.None, KeyCode.None, KeyCode.None };

        public Type type = Type.None;

        public bool key_value = false;

        public float axis_value = 0f;
    }
}