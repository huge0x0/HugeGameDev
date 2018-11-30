using System;
using System.Collections.Generic;
using System.Text;
using HugeGame.Component;
using System.IO;

namespace HugeGame {
    public class StorageHelper {
        #region 单阶段序列化
        //保存到文件
        public static void SaveToStorage(string filePath, ISerializable target) {
            StorageAdapter storageAdapter = target.Encode();
            WriteToFile(filePath, storageAdapter.ToJson());
        }

        //从文件中恢复对象
        public static void LoadFromStorage(string filePath, ISerializable target) {
            StorageAdapter storageAdapter = ReadFromFile(filePath);
            target.Decode(storageAdapter);
        }
        #endregion

        #region 双阶段序列化
        //保存到文件
        public static void SaveToStorage(string filePath, ITPSerializable target) {
            StorageAdapter storageAdapter = new StorageAdapter();
            storageAdapter.Add(target.EncodeFirst());
            storageAdapter.Add(target.EncodeSecond());
            WriteToFile(filePath, storageAdapter.ToJson());
        }

        //从文件中恢复对象
        public static void LoadFromStorage(string filePath, ITPSerializable target) {
            StorageAdapter storageAdapter = ReadFromFile(filePath);
            target.DecodeFirst(storageAdapter[0]);
            target.DecodeSecond(storageAdapter[1]);
        }
        #endregion


        //把数据写入文件
        private static void WriteToFile(string filePath, string json) {
            File.WriteAllText(filePath, json);
        }

        //从文件中读取数据
        private static StorageAdapter ReadFromFile(string filePath) {
            string json = File.ReadAllText(filePath);
            return StorageAdapter.BuildAdapterFromJson(json);
        }
    }
}
