namespace RogArmouryKbRevengGUI.KBInterfaces
{
    interface IBasicHIDKB
    {
        string PrettyName { get; }
        void Close();
        void Connect();
        bool DoesExistOnSystem();
    }
}
