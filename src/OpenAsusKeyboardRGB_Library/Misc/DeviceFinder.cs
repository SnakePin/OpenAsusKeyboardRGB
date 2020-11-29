using OpenAsusKeyboardRGB.KBInterfaces;
using System.Collections.Generic;

namespace OpenAsusKeyboardRGB.Misc
{
    public class ArmouryKeyboardFinder
    {
        public static IEnumerable<IArmouryProtocolKB> Find()
        {
            foreach (IArmouryProtocolKB currentKBInstance in ReflectiveEnumerator.GetEnumerableOfType<IArmouryProtocolKB>())
            {
                if (currentKBInstance.DoesExistOnSystem())
                {
                    yield return currentKBInstance;
                }
            }
        }
    }
}
