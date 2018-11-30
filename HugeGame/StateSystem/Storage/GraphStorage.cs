using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugeGame {
    public class GraphStorage {
        //存储器工作的目录
        public string Folder { get; set; }

        public StateGraph LoadGraph(string path) {

        }

        public void SaveGraph(string path, StateGraph stateGraph) {
            StorageAdapter storageAdapter = new StorageAdapter();
            storageAdapter.Add(stateGraph.GetType().ToString());
            storageAdapter.Add(stateGraph.Encode());
        }
    }
}
