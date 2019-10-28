using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace TodoManager.Utility
{
    public static class EnumExtension
    {
        static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        static readonly Dictionary<string, string> EnumMap = new Dictionary<string, string>(10);

        public static string GetMessage(this Enum em)
        {
            EnsureInitialized(em);
            return EnumMap[string.Format("{0}_{1}", em.GetType().Name, em)];
        }

        private static void EnsureInitialized(Enum em)
        {
            string key = string.Format("{0}_{1}", em.GetType().Name, em);
            if (!EnumMap.ContainsKey(key))
            {
                semaphoreSlim.WaitAsync();
                try
                {
                    if (!EnumMap.ContainsKey(key))
                    {
                        FieldInfo[] fieldInfos = em.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
                        if (fieldInfos != null && fieldInfos.Length > 0)
                        {
                            foreach (FieldInfo f in fieldInfos)
                            {
                                string message = "";
                                DescriptionAttribute desAttr = null;
                                if ((desAttr = f.GetCustomAttribute<DescriptionAttribute>()) != null)
                                {
                                    message = desAttr.Description;
                                }
                                EnumMap.Add(string.Format("{0}_{1}", em.GetType().Name, f.Name), message);
                            }
                        }
                    }
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }
        }
    }
}
