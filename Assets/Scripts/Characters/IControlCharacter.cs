namespace sgffu.Characters {
    public interface IControlCharacterAction
    {
        void OnMainAciton();
        void OnSubAction();
        void OnFocusAction();

        void OnJump();
        void OnDash();
        void OnRoll();
        void OnReload();
        void OnCrouch();
        void OnLayDown();

        void OnUp();
        void OnDown();
        void OnLeft();
        void OnRight();

        void OnMenuOpen();
        void OnInventoryOpen();
        void OnStatusOpen();
        void OnSkillOpen();
        void OnMapOpen();

        void OffDash();
        void OffUp();
        void OffDown();
        void OffLeft();
        void OffRight();
        
    }

    public interface IControlCharacterAxis {
        void OnAxis(string axis_name, float value);
    }
}