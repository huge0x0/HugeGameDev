using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeGame {
    //两阶段编码解码，TP：Two Phase
    public interface ITPSerializable {
        //编码1
        StorageAdapter EncodeFirst();
        //解码1
        void DecodeFirst(StorageAdapter data);
        //编码2
        StorageAdapter EncodeSecond();
        //解码2
        void DecodeSecond(StorageAdapter data, List<StateNode> nodeList);
    }
}
