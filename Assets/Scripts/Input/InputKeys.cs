namespace sgffu.Input {

    public struct Actions {
        public const string MainAction = "MainAction";  // Mayinly mouse button left
        public const string SubAction = "SubAction";  // Sub mouse button right
        public const string FocusAction = "FocusAction";  // Action to thing in front

        public const string Jump = "Jump";
        public const string Dash = "Dash";
        public const string Roll = "Roll";
        public const string Reload = "Reload";
        public const string Crouch = "Crouch";  // かがむ
        public const string LayDown = "LayDown";  // 伏せる

        public const string Up = "Up";
        public const string Down = "Down";
        public const string Left = "Left";
        public const string Right = "Right";

        public const string Menu = "Menu";
        public const string Inventory = "Inventory";
        public const string Status = "Status";
        public const string Skill = "Skill";
        public const string Map = "Map";

    }

    public struct Mouse {
        public const string X = "Mouse X";
        public const string Y = "Mouse Y";
        public const string Wheel = "Mouse ScrollWheel";
    }

}