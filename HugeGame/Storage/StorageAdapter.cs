using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace HugeGame {
    public class StorageAdapter {
        #region 扩展方法
        //数组
        public void Add(string key, List<bool> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        public void Add(string key, List<double> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        public void Add(string key, List<int> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        public void Add(string key, List<long> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        public void Add(string key, List<string> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        public void Add(string key, List<StorageAdapter> valueList) {
            JsonData listJsonData = new JsonData();
            foreach (var value in valueList) {
                listJsonData.Add(value);
            }
            mJsonData[key] = listJsonData;
        }
        
        //构造Adapter
        public static StorageAdapter BuildAdapterFromJson(string json) {
            return new StorageAdapter(JsonMapper.ToObject(json));
        }
        #endregion

        #region 包装适配
        private JsonData mJsonData;

        public StorageAdapter() {
            mJsonData = new JsonData();
        }
        public StorageAdapter(JsonData jsonData) {
            mJsonData = jsonData;
        }
        public StorageAdapter(bool boolean) {
            mJsonData = new JsonData(boolean);
        }
        public StorageAdapter(double number) {
            mJsonData = new JsonData(number);
        }
        public StorageAdapter(int number) {
            mJsonData = new JsonData(number);
        }
        public StorageAdapter(long number) {
            mJsonData = new JsonData(number);
        }
        public StorageAdapter(object obj) {
            mJsonData = new JsonData(obj);
        }
        public StorageAdapter(string str) {
            mJsonData = new JsonData(str);
        }

        public StorageAdapter this[string prop_name] {
            get { return new StorageAdapter(mJsonData[prop_name]); }
            set { mJsonData[prop_name] = value.mJsonData; } }
        public StorageAdapter this[int index] {
            get { return new StorageAdapter(mJsonData[index]) ; }
            set { mJsonData[index] = value.mJsonData; }
        }

        public ICollection<string> Keys { get { return mJsonData.Keys; } }
        public bool IsString { get { return mJsonData.IsString; } }
        public bool IsObject { get { return mJsonData.IsObject; } }
        public bool IsLong { get { return mJsonData.IsLong; } }
        public bool IsInt { get { return mJsonData.IsInt; } }
        public bool IsDouble { get { return mJsonData.IsDouble; } }
        public bool IsBoolean { get { return mJsonData.IsBoolean; } }
        public bool IsArray { get { return mJsonData.IsArray; } }
        public int Count { get { return mJsonData.Count; } }

        public int Add(object value) { return mJsonData.Add(value); }
        public void Clear() { mJsonData.Clear(); }
        public bool Equals(JsonData x) { return mJsonData.Equals(x); }
        public JsonType GetJsonType() { return mJsonData.GetJsonType(); }
        public void SetJsonType(JsonType type) { mJsonData.SetJsonType(type); }
        public string ToJson() { return mJsonData.ToJson(); }
        public void ToJson(JsonWriter writer) { mJsonData.ToJson(writer); }
        public override string ToString() { return mJsonData.ToString(); }

        public static implicit operator StorageAdapter(double data) { return new StorageAdapter((JsonData)data); }
        public static implicit operator StorageAdapter(bool data) { return new StorageAdapter((JsonData)data); }
        public static implicit operator StorageAdapter(string data) { return new StorageAdapter((JsonData)data); }
        public static implicit operator StorageAdapter(long data) { return new StorageAdapter((JsonData)data); }
        public static implicit operator StorageAdapter(int data) { return new StorageAdapter((JsonData)data); }
        public static explicit operator string(StorageAdapter data) { return (string)data.mJsonData; }
        public static explicit operator long(StorageAdapter data) { return (long)data.mJsonData; }
        public static explicit operator int(StorageAdapter data) { return (int)data.mJsonData; }
        public static explicit operator double(StorageAdapter data) { return (double)data.mJsonData; }
        public static explicit operator bool(StorageAdapter data) { return (bool)data.mJsonData; }
        #endregion
    }
}
