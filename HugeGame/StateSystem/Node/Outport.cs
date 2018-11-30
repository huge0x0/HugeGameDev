using System.Collections.Generic;

namespace HugeGame.Component {
    //接口
    public interface IOutport : ITPSerializable {
        //选择出口
        StateNode ChooseOutport();
    }

    //默认实现
    public class Outport : IOutport {
        public StateNode ChooseOutport() {
            return null ;
        }

        public virtual void DecodeFirst(StorageAdapter data) {
        }

        public virtual void DecodeSecond(StorageAdapter data) {
        }

        public void DecodeSecond(StorageAdapter data, List<StateNode> nodeList) {
        }

        public virtual StorageAdapter EncodeFirst() {
            return new StorageAdapter();
        }

        public virtual StorageAdapter EncodeSecond() {
            return new StorageAdapter();
        }
    }
}
