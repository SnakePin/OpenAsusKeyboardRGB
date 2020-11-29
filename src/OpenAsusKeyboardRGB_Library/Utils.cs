using HidSharp;
using HidSharp.Reports.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenAsusKeyboardRGB
{
    class Utils
    {
        public static byte ConstructByteFromNibbles(byte byteUpper, byte byteLower)
        {
            return (byte)((byteUpper << 4) | (byteLower & 0x0F));
        }

        public static HidDevice FilterHidDevicesByUsage(IEnumerable<HidDevice> devices, uint usagePage, uint usage)
        {
            foreach (var dev in devices)
            {
                try
                {
                    var raw = dev.GetRawReportDescriptor();
                    var usages = EncodedItem.DecodeItems(raw, 0, raw.Length).Where(t => t.TagForGlobal == GlobalItemTag.UsagePage);

                    if (usages.Any(g => g.ItemType == ItemType.Global && g.DataValue == usagePage))
                    {
                        if (usages.Any(l => l.ItemType == ItemType.Local && l.DataValue == usage))
                        {
                            return dev;
                        }
                    }
                }
                catch
                {
                    //failed to get the report descriptor, skip
                }
            }
            return null;
        }
        public static HidDevice GetHidDevice(int vid, int pid, uint usage, uint usagepage, out byte reportIdToUse)
        {
            var hidDevice = FilterHidDevicesByUsage(DeviceList.Local.GetHidDevices(vid, pid), usagepage, usage);
            reportIdToUse = 0;

            if (hidDevice == null)
            {
                return null;
            }

            var allReports = hidDevice.GetReportDescriptor().Reports;
            if (!allReports.Any())
            {
                return null;
            }

            //Asus uses first report in their software too
            reportIdToUse = allReports.First().ReportID;

            return hidDevice;
        }
    }

    static class ReflectiveEnumerator
    {
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs)
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && typeof(T).IsAssignableFrom(x)))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            return objects;
        }
    }

    static class Extensions
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static T[,] TransposeMatrix<T>(this T[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var result = new T[columns, rows];
            for (var c = 0; c < columns; c++)
            {
                for (var r = 0; r < rows; r++)
                {
                    result[c, r] = matrix[r, c];
                }
            }
            return result;
        }

        public static T[] FlattenMatrix<T>(this T[,] matrix)
        {
            return matrix.Cast<T>().ToArray();
        }
    }
}
