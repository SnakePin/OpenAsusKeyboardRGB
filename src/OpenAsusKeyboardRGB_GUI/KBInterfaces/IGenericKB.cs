namespace RogArmouryKbRevengGUI.KBInterfaces
{
    interface IGenericKB
    {
        void Close();
        void Connect();
        string GetPrettyName();
        bool DoesExistOnSystem();
    }
}
