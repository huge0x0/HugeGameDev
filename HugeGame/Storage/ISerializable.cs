using HugeGame.Component;

namespace HugeGame {
    public interface ISerializable {
        //编码
        StorageAdapter Encode();
        //解码
        void Decode(StorageAdapter data);
    }
}
