using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YBZ.Algorithm {
    public class PriorityQueue<T> where T : new ()
    {
        public int size;
        public int capacity;
        private T[] elements;

        // 是否为空
        public bool IsEmpty { get => size == 0; }

        /// 返回顶部元素
        public T Top { get => elements[0]; }

        /// 优先队列的模式
        private PriorityQueueMode _comparator;

        public enum PriorityQueueMode {
            less = -1, // 最小优先队列
            equal = 0, // 相等的排在一起
            greater = 1 // 最大优先队列
        }

        /// <summary>
        /// 以CMP(a,b) 为例：
        /// 当a>b时，返回1，表示放右边
        /// 当a==b时，返回0，表示不变
        ///  当a<b时，返回-1，表示放左边
        /// </summary>
        private Func<T,T,int> CMP;
        
        /// <summary>
        /// 构造函数, 必须实现
        /// </summary>
        /// <param name="CMP"></param>
        /// <param name="capacity"></param>
        /// <param name="priorityQueueMode"></param>
        public PriorityQueue(Func<T,T,int> CMP, PriorityQueueMode priorityQueueMode = PriorityQueueMode.less, int capacity = 1) {
            this.CMP = CMP;
            this.size = 0; // 数组索引从0开始
            this.capacity = capacity;
            this.elements = new T[capacity];
            this._comparator = priorityQueueMode;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value) {
            if (size == capacity) {
                ExpandCapacity();
            }
            elements[size++] = value;

            ShiftUp();
        }

        /// <summary>
        /// 出队
        /// </summary>
        public void Pop() {
            if(size == 0) {
                return;
            }
            size--;
            Swap(ref elements[0], ref elements[size]);

            ShiftDown();
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear() {
            size = 0;
        }

        /// <summary>
        /// 返回位于Queue开始处的对象但不将其移除。
        /// </summary>
        /// <returns>返回第一个队列中元素</returns>
        public T Peek() {
            return Top;
        }

        /// <summary>
        /// 扩展队列的容量
        /// </summary>
        private void ExpandCapacity() {
            capacity = Mathf.CeilToInt(capacity * 1.5f);
            T[] temp = new T[capacity];

            for (int i = 0; i < elements.Length; i++) {
                temp[i] = elements[i];
            }
            elements = temp;
        }
        
        // 从下到上 重排序 
        private void ShiftUp() {
            int cur = size - 1 ;
            int parent = ( cur -1 ) >> 2;
            while (cur > 0) 
            {
                if (CMP(elements[cur],elements[parent]) == (int)_comparator) {
                    Swap(ref elements[cur], ref elements[parent]);
                    cur = parent;
                    parent = (cur - 1) >> 2;
                } else break;
            }
        }

        // 从上到下 重排序
        private void ShiftDown() {
            int cur = 0;
            int child = 1;
            while (child < size) {
                if (child + 1 < size && CMP(elements[child +1], elements[child]) == (int)_comparator) {
                    child++;
                }

                if (CMP(elements[child], elements[cur]) == (int)_comparator){

                    Swap(ref elements[child], ref elements[cur]);
                    cur = child;
                    child = cur << 1 + 1;
                } else break;
            }
        }

        /// <summary>
        /// 交换传入的两个元素
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        private void Swap(ref T lhs,ref T rhs) {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        /// <summary>
        /// 返回队列中的所有元素,对于ToString()函数，值类型会返回值，引用类型会返回数据类型
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            string result = "";
            foreach (var v in elements) {
                result += v.ToString();
            }
            return result;
        }
    }
}

