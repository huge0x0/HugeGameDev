using System;
using System.Collections.Generic;
using System.Text;

namespace HugeGame {
    public class EventCenter<EventSignalEnum> {
        #region public
        //提供的4种事件触发类型
        public delegate void ReceiverWithNoPara();
        public delegate void ReceiverWithPara1<T>(T para);
        public delegate void ReceiverWithPara2<T1, T2>(T1 para1, T2 para2);
        public delegate void ReceiverWithParaS<T>(T[] paras);

        #region 事件的注册与信号的发送
        /// <summary>
        /// 无参数事件注册
        /// </summary>
        /// <param name="eventSignal">需要响应的信号</param>
        /// <param name="tag">接收器的标记</param>
        /// <param name="receiverWithNoPara">无参接收器</param>
        public void RegisterEventWithNoPara(EventSignalEnum eventSignal, Object tag, ReceiverWithNoPara receiverWithNoPara) {
            RegisterEvent(mEventDict0, eventSignal, tag, receiverWithNoPara);
        }
        /// <summary>
        /// 发送无参数事件的信号
        /// </summary>
        /// <param name="eventSignal">发送的信号</param>
        public void SendSignalWithNoPara(EventSignalEnum eventSignal) {
            ExecEvent(mEventDict0, eventSignal, (ReceiverWithNoPara receiver) => { receiver(); });
        }

        /// <summary>
        /// 单参数事件注册
        /// </summary>
        /// <typeparam name="T">接收器参数类型</typeparam>
        /// <param name="eventSignal">需要响应的信号</param>
        /// <param name="tag">接收器的标记</param>
        /// <param name="receiverWithPara1">单参接收器</param>
        public void RegisterEventWithPara1<T>(EventSignalEnum eventSignal, Object tag, ReceiverWithPara1<T> receiverWithPara1) {
            RegisterEvent(mEventDict1, eventSignal, tag, receiverWithPara1);
        }
        /// <summary>
        /// 发送单参数事件的信号
        /// </summary>
        /// <typeparam name="T">接收器参数类型</typeparam>
        /// <param name="eventSignal">发送的信号</param>
        /// <param name="para">接收器参数</param>
        public void SendSignalWithPara1<T>(EventSignalEnum eventSignal, T para) {
            ExecEvent(mEventDict1, eventSignal, (ReceiverWithPara1<T> receiverWithPara1) => { receiverWithPara1(para); });
        }

        ///<summary>
        /// 双参数事件注册
        /// </summary>
        /// <typeparam name="T1">接收器第一参数类型</typeparam>
        /// <typeparam name="T2">接收器第二参数类型</typeparam>
        /// <param name="eventSignal">需要响应的信号</param>
        /// <param name="tag">接收器的标记</param>
        /// <param name="receiverWithPara2">双参接收器</param>
        public void RegisterEventWithPara2<T1, T2>(EventSignalEnum eventSignal, Object tag, ReceiverWithPara2<T1, T2> receiverWithPara2) {
            RegisterEvent(mEventDict2, eventSignal, tag, receiverWithPara2);
        }
        /// <summary>
        /// 发送双参数事件的信号
        /// </summary>
        /// <typeparam name="T1">接收器第一参数类型</typeparam>
        /// <typeparam name="T2">接收器第二参数类型</typeparam>
        /// <param name="eventSignal">发送的信号</param>
        /// <param name="para1">接收器第一参数</param>
        /// <param name="para2">接收器第二参数</param>
        public void SendSignalWithPara2<T1, T2>(EventSignalEnum eventSignal, T1 para1, T2 para2) {
            ExecEvent(mEventDict2, eventSignal, (ReceiverWithPara2<T1, T2> receiverWithPara2) => { receiverWithPara2(para1, para2); });
        }

        /// <summary>
        /// 任意参数事件注册
        /// </summary>
        /// <param name="eventSignal">需要响应的信号</param>
        /// <param name="tag">接收器的标记</param>
        /// <param name="receiverWithParaS">任意参接收器</param>
        public void RegisterEventWithParaS<T>(EventSignalEnum eventSignal, Object tag, ReceiverWithParaS<T> receiverWithParaS) {
            RegisterEvent(mEventDictS, eventSignal, tag, receiverWithParaS);
        }
        /// <summary>
        /// 发送任意参数事件的信号
        /// </summary>
        /// <typeparam name="T">接收器参数类型</typeparam>
        /// <param name="eventSignal">发送的信号</param>
        /// <param name="paras">接收器的参数数组</param>
        public void SendSignalWithParaS<T>(EventSignalEnum eventSignal, params T[] paras) {
            ExecEvent(mEventDictS, eventSignal, (ReceiverWithParaS<T> receiverWithParaS) => { receiverWithParaS(paras); });
        }
        #endregion

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="tag">事件对应的标记</param>
        public void RemoveEvent(Object tag) {
            if (mReceiverTagDict.ContainsKey(tag)) {
                foreach (ReceiverHolder receiverHolder in mReceiverTagDict[tag]) {
                    receiverHolder.RemoveReceiver();
                }
                mReceiverTagDict.Remove(tag);
            }
        }

        /// <summary>
        /// 清理空闲索引
        /// </summary>
        public void CleanIndex() {
            //依次情况所有字典
            CleanDictIndex(mEventDict0);
            CleanDictIndex(mEventDict1);
            CleanDictIndex(mEventDict2);
            CleanDictIndex(mEventDictS);
        }
        #endregion

        #region private
        //注册的事件
        private Dictionary<EventSignalEnum, List<ReceiverHolder>> mEventDict0 = new Dictionary<EventSignalEnum, List<ReceiverHolder>>();
        private Dictionary<EventSignalEnum, List<ReceiverHolder>> mEventDict1 = new Dictionary<EventSignalEnum, List<ReceiverHolder>>();
        private Dictionary<EventSignalEnum, List<ReceiverHolder>> mEventDict2 = new Dictionary<EventSignalEnum, List<ReceiverHolder>>();
        private Dictionary<EventSignalEnum, List<ReceiverHolder>> mEventDictS = new Dictionary<EventSignalEnum, List<ReceiverHolder>>();

        //根据tag记录的所有接收器
        private Dictionary<Object, List<ReceiverHolder>> mReceiverTagDict = new Dictionary<object, List<ReceiverHolder>>();

        //用于保存事件的接收器
        private class ReceiverHolder {
            //保存本接收器的列表,用于移除事件
            private List<ReceiverHolder> mReceiverHolderList;
            //保存的接收器
            private Object mReceiver;
            //获取接收器
            public T GetReceiver<T>() where T : class {
                return mReceiver as T;
            }
            //构造
            public ReceiverHolder(Object receiver, List<ReceiverHolder> receiverHolderList) {
                mReceiver = receiver;
                mReceiverHolderList = receiverHolderList;
            }
            //无效化本接收器
            public void RemoveReceiver() {
                mReceiver = null;
                mReceiverHolderList.Remove(this);
            }
        }


        //清理索引的辅助函数
        private void CleanDictIndex(Dictionary<EventSignalEnum, List<ReceiverHolder>> eventDict) {
            //用于保存需要清理的信号索引
            List<EventSignalEnum> listToRemove = new List<EventSignalEnum>();
            //遍历所有的索引，找到没有接收器的索引并放入带待处理列表中
            foreach (KeyValuePair<EventSignalEnum, List<ReceiverHolder>> eventPair in eventDict) {
                if (eventPair.Value.Count == 0) {
                    listToRemove.Add(eventPair.Key);
                }
            }
            //把所有待处理的索引移除
            foreach (EventSignalEnum eventSignal in listToRemove) {
                eventDict.Remove(eventSignal);
            }
        }

        //记录对应Tag的接收器
        private void AddToTagList(Object tag, ReceiverHolder receiverHolder) {
            List<ReceiverHolder> receiverHolderList = TryGetFromDict(mReceiverTagDict, tag);
            receiverHolderList.Add(receiverHolder);
        }

        //注册辅助函数
        private void RegisterEvent(Dictionary<EventSignalEnum, List<ReceiverHolder>> eventDict, EventSignalEnum eventSignal, Object tag, Object receiver) {
            List<ReceiverHolder> receiverList = TryGetFromDict(eventDict, eventSignal);
            //将接收器封装到Holder中
            ReceiverHolder receiverHolder = new ReceiverHolder(receiver, receiverList);
            receiverList.Add(receiverHolder);
            //将相应接收器按tag进行记录
            AddToTagList(tag, receiverHolder);
        }

        //执行事件辅助函数,helperReceiver仅仅用来传递逻辑，没有实际含义
        private void ExecEvent<T>(Dictionary<EventSignalEnum, List<ReceiverHolder>> eventDict, EventSignalEnum eventSignal, ReceiverWithPara1<T> helperReceiver) where T : class {
            if (eventDict.ContainsKey(eventSignal)) {
                //这里对要执行的接收器进行了一次copy，避免执行接收器的过程中数组变动
                List<ReceiverHolder> receiverList = new List<ReceiverHolder>(eventDict[eventSignal]);
                foreach (ReceiverHolder receiverHolder in receiverList) {
                    T receiver = receiverHolder.GetReceiver<T>();
                    //检测接收器是否是激活的
                    if (receiver != null)
                        helperReceiver(receiver);
                }

            }
        }

        //从字典中尝试读取，如果字典中不存在，那么就会构造一个新的并添加入字典
        private V TryGetFromDict<K, V>(Dictionary<K, V> dict, K key) where V : new() {
            if (dict.ContainsKey(key)) {
                //若存在，则直接返回
                return dict[key];
            } else {
                //否则构造一个新的，添加入字典并返回
                V value = new V();
                dict.Add(key, value);
                return value;
            }
        }
        #endregion

        #region test
        public static void Main() {
            EventCenter<EventSignalEnum2> eventCenter = new EventCenter<EventSignalEnum2>();
            Object testTag1 = new Object();
            Object testTag2 = new Object();
            //注册
            eventCenter.RegisterEventWithNoPara(EventSignalEnum2.Default, testTag1, test0);
            eventCenter.RegisterEventWithPara1<int>(EventSignalEnum2.Default, testTag1, test1);
            eventCenter.RegisterEventWithPara1<string>(EventSignalEnum2.Default, testTag1, test11);
            eventCenter.RegisterEventWithPara2<float, float>(EventSignalEnum2.Default, testTag1, test2);
            eventCenter.RegisterEventWithParaS<string>(EventSignalEnum2.Default, testTag1, test3);
            eventCenter.RegisterEventWithParaS<string>(EventSignalEnum2.Test, testTag2, test3);

            //发送信号
            eventCenter.SendSignalWithNoPara(EventSignalEnum2.Default);
            eventCenter.SendSignalWithPara1(EventSignalEnum2.Default, 1);
            eventCenter.SendSignalWithPara1(EventSignalEnum2.Default, "test11");
            eventCenter.SendSignalWithPara2(EventSignalEnum2.Default, 2f, 3f);
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Default, "testTag1");
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Test, "This is ", "testTag2");

            //取消注册
            eventCenter.RemoveEvent(testTag1);
            //测试
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Default, "testTag1");
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Test, "This is ", "testTag2");

            //取消注册
            eventCenter.RemoveEvent(testTag2);
            //测试
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Default, "testTag1");
            eventCenter.SendSignalWithParaS(EventSignalEnum2.Test, "This is ", "testTag2");
        }

        private static void test0() {
            Console.WriteLine("test0");
        }

        private static void test1(int a) {
            Console.WriteLine("test1 " + a);
        }

        private static void test11(string a) {
            Console.WriteLine("test11 " + a);
        }

        private static void test2(float a, float b) {
            Console.WriteLine("tset2 " + a + " - " + b);
        }

        private static void test3(params string[] s) {
            StringBuilder result = new StringBuilder("test3 ");
            foreach (string ss in s) {
                result.Append(ss).Append(" - ");
            }
            Console.WriteLine(result.ToString());
        }
        #endregion
    }
}
