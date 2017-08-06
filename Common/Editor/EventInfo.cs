using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARPG.Common
{
    /// <summary>动画事件信息</summary>
    [System.Serializable]
    public class EventInfo
    {
        public string functionName;                          //方法名称
        public float time;                                            //事件插入的时间
        public string parameterValue;                       //事件参数值
        public ParameterType parameterType;         //事件参数数据类型
    }
}
