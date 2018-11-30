using System;
using System.Collections.Generic;
using System.Text;

namespace HugeGame.Component {
    //接口
    public interface INodeBody : ITPSerializable{
        //进入节点时调用
        void OnEnter();

        //OnEnter之后或者重新加载节点时调用
        void OnPrepare();

        //节点受刺激时调用,返回是否要进入下个节点
        bool OnStimulate();

        //准备退出节点(确定出口前)时调用
        void OnExit();

        //退出节点(确定出口)后调用
        void OnFinish();
    }

    //默认实现
    public class NodeBody : INodeBody {
        //进入节点时调用
        public virtual void OnEnter() {

        }

        //OnEnter之后或者重新加载节点时调用
        public virtual void OnPrepare() {

        }

        //节点受刺激时调用
        public virtual bool OnStimulate() {
            return false;
        }

        //准备退出节点(确定出口前)时调用
        public virtual void OnExit() {

        }

        //退出节点(确定出口)后调用
        public virtual void OnFinish() {

        }
    }
}
