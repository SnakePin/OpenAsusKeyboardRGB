namespace RogArmouryKbRevengGUI.KBInterfaces
{
    interface IBasicHIDKB
    {
        void Close();
        void Connect();
        string GetPrettyName();
        bool DoesExistOnSystem();
    }
}
