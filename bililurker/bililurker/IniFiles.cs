using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace bililurker
{
    public  class INIHelper
    {

        #region 读写INI文件相关
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);


        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);
        #endregion

        #region 读写操作（字符串）
        /// <summary>
        /// 向INI写入数据
        /// </summary>
        /// <PARAM name="Section">节点名</PARAM>
        /// <PARAM name="Key">键名</PARAM>
        /// <PARAM name="Value">值（字符串）</PARAM>
        public static void Write(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }
        /// <summary>
        /// 读取INI数据
        /// </summary>
        /// <PARAM name="Section">节点名</PARAM>
        /// <PARAM name="Key">键名</PARAM>
        /// <PARAM name="Path">值名</PARAM>
        /// <returns>值（字符串）</returns>
        public static string Read(string Section, string Key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }
        #endregion

        #region 配置节信息
        /// <summary>
        /// 读取一个ini里面所有的节
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="path"></param>
        /// <returns>-1:没有节信息，0:正常</returns>
        public static int GetAllSectionNames(out string[] sections, string path)
        {
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
            {
                sections = null;
                return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return 0;
        }
        /// <summary>
        /// 返回指定配置文件下的节名称列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetAllSectionNames(string path)
        {
            List<string> sectionList = new List<string>();
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned != 0)
            {
                string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
                Marshal.FreeCoTaskMem(pReturnedString);
                sectionList.AddRange(local.Substring(0, local.Length - 1).Split('\0'));
            }
            return sectionList;
        }

        /// <summary>
        /// 得到某个节点下面所有的key和value组合
        /// </summary>
        /// <param name="section">指定的节名称</param>
        /// <param name="keys">Key数组</param>
        /// <param name="values">Value数组</param>
        /// <param name="path">INI文件路径</param>
        /// <returns></returns>
        public static int GetAllKeyValues(string section, out string[] keys, out string[] values, string path)
        {
            byte[] b = new byte[65535];//配置节下的所有信息
            GetPrivateProfileSection(section, b, b.Length, path);
            string s = System.Text.Encoding.Default.GetString(b);//配置信息
            string[] tmp = s.Split((char)0);//Key\Value信息
            List<string> result = new List<string>();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            values = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].Split(new char[] { '=' });//Key=Value格式的配置信息
                //Value字符串中含有=的处理，
                //一、Value加""，先对""处理
                //二、Key后续的都为Value
                if (item.Length > 2)
                {
                    keys[i] = item[0].Trim();
                    values[i] = result[i].Substring(keys[i].Length + 1);
                }
                if (item.Length == 2)//Key=Value
                {
                    keys[i] = item[0].Trim();
                    values[i] = item[1].Trim();
                }
                else if (item.Length == 1)//Key=
                {
                    keys[i] = item[0].Trim();
                    values[i] = "";
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                    values[i] = "";
                }
            }
            return 0;
        }
        /// <summary>
        /// 得到某个节点下面所有的key
        /// </summary>
        /// <param name="section">指定的节名称</param>
        /// <param name="keys">Key数组</param>
        /// <param name="path">INI文件路径</param>
        /// <returns></returns>
        public static int GetAllKeys(string section, out string[] keys, string path)
        {
            byte[] b = new byte[65535];

            GetPrivateProfileSection(section, b, b.Length, path);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            ArrayList result = new ArrayList();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].ToString().Split(new char[] { '=' });
                if (item.Length == 2)
                {
                    keys[i] = item[0].Trim();
                }
                else if (item.Length == 1)
                {
                    keys[i] = item[0].Trim();
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                }
            }
            return 0;
        }
        /// <summary>
        /// 获取指定节下的Key列表
        /// </summary>
        /// <param name="section">指定的节名称</param>
        /// <param name="path">配置文件名称</param>
        /// <returns>Key列表</returns>
        public static List<string> GetAllKeys(string section, string path)
        {
            List<string> keyList = new List<string>();
            byte[] b = new byte[65535];
            GetPrivateProfileSection(section, b, b.Length, path);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            List<string> result = new List<string>();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].Split(new char[] { '=' });
                if (item.Length == 2 || item.Length == 1)
                {
                    keyList.Add(item[0].Trim());
                }
                else if (item.Length == 0)
                {
                    keyList.Add(string.Empty);
                }
            }
            return keyList;
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetAllValues(string section, string path)
        {
            List<string> keyList = new List<string>();
            byte[] b = new byte[65535];
            GetPrivateProfileSection(section, b, b.Length, path);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            List<string> result = new List<string>();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].Split(new char[] { '=' });
                if (item.Length == 2 || item.Length == 1)
                {
                    keyList.Add(item[1].Trim());
                }
                else if (item.Length == 0)
                {
                    keyList.Add(string.Empty);
                }
            }
            return keyList;
        }

        #endregion

        #region 通过值查找键（一个节中的键唯一，可能存在多个键值相同，因此反查的结果可能为多个）

        /// <summary>
        /// 第一个键
        /// </summary>
        /// <param name="section"></param>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetFirstKeyByValue(string section, string path, string value)
        {
            foreach (string key in GetAllKeys(section, path))
            {
                if (ReadString(section, key, "", path) == value)
                {
                    return key;
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 所有键
        /// </summary>
        /// <param name="section"></param>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> GetKeysByValue(string section, string path, string value)
        {
            List<string > keys = new List<string>();
            foreach (string key in GetAllKeys(section, path))
            {
                if (ReadString(section, key, "", path) == value)
                {
                    keys.Add(key);
                }
            }
            return keys;
        }
        #endregion


        #region 具体类型的读写

        #region string
        /// <summary>
        ///
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue" />
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadString(string sectionName, string keyName, string defaultValue, string path)
        {
            const int MAXSIZE = 255;
            StringBuilder temp = new StringBuilder(MAXSIZE);
            GetPrivateProfileString(sectionName, keyName, defaultValue, temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteString(string sectionName, string keyName, string value, string path)
        {
            WritePrivateProfileString(sectionName, keyName, value, path);
        }
        #endregion

        #region Int
        /// <summary>
        ///
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int ReadInteger(string sectionName, string keyName, int defaultValue, string path)
        {

            return GetPrivateProfileInt(sectionName, keyName, defaultValue, path);

        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteInteger(string sectionName, string keyName, int value, string path)
        {

            WritePrivateProfileString(sectionName, keyName, value.ToString(), path);

        }
        #endregion

        #region bool
        /// <summary>
        /// 读取布尔值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ReadBoolean(string sectionName, string keyName, bool defaultValue, string path)
        {

            int temp = defaultValue ? 1 : 0;

            int result = GetPrivateProfileInt(sectionName, keyName, temp, path);

            return (result == 0 ? false : true);

        }
        /// <summary>
        /// 写入布尔值
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        public static void WriteBoolean(string sectionName, string keyName, bool value, string path)
        {
            string temp = value ? "1 " : "0 ";
            WritePrivateProfileString(sectionName, keyName, temp, path);
        }
        #endregion

        #endregion

        #region 删除操作
        /// <summary>
        /// 删除指定项
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        /// <param name="path"></param>
        public static void DeleteKey(string sectionName, string keyName, string path)
        {
            WritePrivateProfileString(sectionName, keyName, null, path);
        }

        /// <summary>
        /// 删除指定节下的所有项
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="path"></param>
        public static void EraseSection(string sectionName, string path)
        {
            WritePrivateProfileString(sectionName, null, null, path);
        }
        #endregion

        #region 判断节、键是否存在
        /// <summary>
        /// 指定节知否存在
        /// </summary>
        /// <param name="section"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ExistSection(string section, string fileName)
        {
            string[] sections = null;
            GetAllSectionNames(out sections, fileName);
            if (sections != null)
            {
                foreach (var s in sections)
                {
                    if (s == section)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 指定节下的键是否存在
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ExistKey(string section, string key, string fileName)
        {
            string[] keys = null;
            GetAllKeys(section, out keys, fileName);
            if (keys != null)
            {
                foreach (var s in keys)
                {
                    if (s == key)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 同一Section下添加多个Key\Value
        /// <summary>
        ///
        /// </summary>
        /// <param name="section"></param>
        /// <param name="keyList"></param>
        /// <param name="valueList"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool AddSectionWithKeyValues(string section, List<string> keyList, List<string> valueList, string path)
        {
            bool bRst = true;
            //判断Section是否已经存在，如果存在，返回false
            //已经存在，则更新
            //if (GetAllSectionNames(path).Contains(section))
            //{
            //    return false;
            //}
            //判断keyList中是否有相同的Key，如果有，返回false

            //添加配置信息
            for (int i = 0; i < keyList.Count; i++)
            {
                WriteString(section, keyList[i], valueList[i], path);
            }
            return bRst;
        }
        #endregion
    }
}