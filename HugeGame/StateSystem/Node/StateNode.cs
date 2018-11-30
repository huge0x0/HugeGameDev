using System;
using System.Collections.Generic;
using System.Text;
using HugeGame.Component;

namespace HugeGame {
    public class StateNode : ITPSerializable {
        #region protected
        #endregion


        //身体
        public INodeBody Body;
        //出口
        public IOutport Outport;
        //所在的图
        private StateGraph mGraph;

        //编号
        private int mIDNumber = -1;
        public int IDNumber { get { return mIDNumber; } set { mIDNumber = value; } }

        //构造
        public StateNode() {
            Body = new NodeBody();
        }

        public StateNode(INodeBody nodeBody) {
            Body = nodeBody;
        }

        //进入节点
        public void Enter() {
            Body.OnEnter();
        }

        //退出节点，返回下个节点
        public StateNode Exit() {
            Body.OnExit();
            return Outport.ChooseOutport();
        }

        //迁移到下个节点
        public void Translate() {
            mGraph.Translate(this, Exit());
        }

        #region 存储相关
        protected virtual StorageAdapter EncodeContent() {
            return null;
        }

        protected virtual void DecodeContent(StorageAdapter data) {

        }

        protected virtual StorageAdapter EncodeRef() {
            return null;
        }

        protected virtual void DecodeRef(StorageAdapter data, List<StateNode> nodeList) {

        }


        public StorageAdapter EncodeFirst() {
            StorageAdapter storageAdapter = new StorageAdapter();
            storageAdapter.Add(Body.EncodeFirst());
            storageAdapter.Add(mOutport.EncodeFirst());
            storageAdapter.Add(EncodeContent());
            return storageAdapter;

        }

        public void DecodeFirst(StorageAdapter data) {
            Body.DecodeFirst(data[0]);
            mOutport.DecodeFirst(data[1]);
            DecodeContent(data[2]);
        }

        public StorageAdapter EncodeSecond() {
            StorageAdapter storageAdapter = new StorageAdapter();
            storageAdapter.Add(Body.EncodeSecond());
            storageAdapter.Add(mOutport.EncodeSecond());
            storageAdapter.Add(EncodeRef());
            return storageAdapter;
        }

        public void DecodeSecond(StorageAdapter data, List<StateNode> nodeList) {
            Body.DecodeSecond(data[0], nodeList);
            Body.DecodeSecond(data[0], nodeList);
            DecodeRef(data[2], nodeList);
            Body.OnPrepare();
        }
        #endregion
    }
}
