﻿namespace OpenAsusKeyboardRGB.KBInterfaces
{
    public interface IBasicHIDKB
    {
        string PrettyName { get; }
        void Close();
        void Connect();
        bool DoesExistOnSystem();
    }
}
