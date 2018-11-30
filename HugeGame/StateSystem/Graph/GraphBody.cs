using System;
using System.Collections.Generic;
using System.Text;

namespace HugeGame.Component {
    class GraphBody : INodeBody {
        //图的开始节点列表
        private List<StateNode> mStartNodeList;
        //当前图激活的节点列表
        private List<StateNode> mActiveNodeList;
        //构造
        public GraphBody(List<StateNode> startNodeList) {
            mStartNodeList = startNodeList;
        }
        public virtual void OnEnter() {
            foreach(StateNode node in mStartNodeList) {
                node.Enter();
            }
        }

        public virtual void OnExit() {

        }

        public virtual void OnFinish() {

        }

        public virtual void OnPrepare() {

        }

        public virtual bool OnStimulate() {
            foreach (StateNode node in mActiveNodeList) {
                if (node.Stimulate())
                    node.Exit();
            }
            return false;
        }
    }
}
