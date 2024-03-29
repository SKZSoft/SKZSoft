﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKZSoft.Common.Queueing
{
    public class QueueItem<T>
    {
        public T Type { get; set; }
        public object Item { get; set; }

        public QueueItem(T type, object item)
        {
            Type = type;
            Item = item;
        }


        /// <summary>
        /// Queue items are "equals" only if an object of the same TYPE is queued.
        /// It is quite possible to 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            QueueItem<T> castObj = (QueueItem<T>)obj;
            T thatType = castObj.Type;
            return thatType.Equals(Type);
            //return (castObj.Type == Type);
        }

        /// <summary>
        /// Hash code override
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

    }
}
