using System;
using System.Collections.Generic;

namespace EnvBackup
{
    public class WindowsEnvironment
    {
        // 设置环境变量
        public static bool Set(string pathVar)
        {
            // pathVar为空
            if (string.IsNullOrWhiteSpace(pathVar)) return false;
            // 是否已经存在
            if (Exist(pathVar)) return false;

            // 配置
            string path = Get();
            string newPath = path + ";" + pathVar;
            Reset(newPath);

            // 检验
            return Exist(pathVar);
        }

        // 替换旧值并设置新值
        public static void ReplaceSet(string oldPath, string newPath)
        {
            Remove(oldPath);
            Set(newPath);
        }

        // 替换列表并设置
        public static void ReplaceListSet(List<string> oldPathList, string newPath)
        {
            oldPathList.ForEach(oldPath => Remove(oldPath));
            Set(newPath);
        }

        // 获取环境变量
        public static string Get()
        {
            return Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);
        }

        // 环境变量是否存在
        public static bool Exist(string pathVar)
        {
            if (string.IsNullOrWhiteSpace(pathVar)) return false;
            string path = Get();
            return path.Contains(pathVar);
        }

        // 从环境变量移除，如果环境变量存在值
        public static bool Remove(string pathVar)
        {
            if (Exist(pathVar))
            {
                string path = Get();
                path = path.Replace(pathVar, "").Replace(";;", "");
                Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
            }
            return !Exist(pathVar);
        }

        public static void Reset(string path)
        {
            Environment.SetEnvironmentVariable("Path", path, EnvironmentVariableTarget.Machine);
        }
    }
}
