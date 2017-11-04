using UnityEngine;
using System.Collections.Generic;

namespace sgffu.Input {

    public class InputEntity
    {

        public string name = "";

        public List<KeyCode> key_code = new List<KeyCode> { KeyCode.None, KeyCode.None, KeyCode.None };

        public Type type = Type.None;

    }
}