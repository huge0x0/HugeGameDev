using System;
using System.Collections.Generic;
using System.Text;
using HugeGame.Component;

namespace HugeGame {
    public class StateGraph : StateNode {

        //开始节点
        private List<StateNode> mStartNodeList;

        //结束节点
        private StateNode mEndNode;

        //图内的节点
        private List<StateNode> mNodeList;

        //激活中的节点
        private List<StateNode> mActiveNodeList;

        //迁移节点
        public void Translate(StateNode source, StateNode target) {
            if(Contains(source) && Contains(target) && mActiveNodeList.Contains(source)) {
                mActiveNodeList[mActiveNodeList.IndexOf(source)] = target;
            }
        }

        private bool Contains(StateNode stateNode) {
            int idNum = stateNode.IDNumber;
            return idNum >= 0 && idNum < mNodeList.Count && mNodeList[idNum] == stateNode;
        }
    }
}
