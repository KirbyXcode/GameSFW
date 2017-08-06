using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

namespace ARPG.Common
{
    /// <summary>
    /// 动画事件注册类
    /// </summary>
    //[System.Serializable]
    public class AnimationBehaviour : Editor
    {
        //public AnimationClip animationClip;             //动画片段
        //public EventInfo[] eventsInfo;
        /// <summary>动态为动画添加事件 </summary>
        [MenuItem("Animation/BuildAnimationEvent")]
        static void Build()
        {
            EditorUtility.DisplayDialog("Warning", "target.name", "OK");
            //if (animationClip != null)
            //{
            //    int index = 0;
            //    AnimationEvent[] animEvents = new AnimationEvent[eventsInfo.Length];
            //    foreach (var item in eventsInfo)
            //    {
            //        AnimationEvent animEvent = new AnimationEvent();
            //        animEvent.functionName = item.functionName;
            //        animEvent.time = item.time;
            //        SetEventParameter(animEvent, item.parameterValue, item.parameterType);
            //        //animationClip.AddEvent(animEvent);
            //        animEvents[index] = animEvent;
            //    }
            //    AnimationUtility.SetAnimationEvents(animationClip, animEvents);
            //}
        }

        //private void SetEventParameter(AnimationEvent anEvent, string paramValue, ParameterType pType)
        //{
        //    switch (pType)
        //    {
        //        case ParameterType.INT:
        //            anEvent.intParameter = int.Parse(paramValue);
        //            break;
        //        case ParameterType.STRING:
        //            anEvent.stringParameter = paramValue;
        //            break;
        //        case ParameterType.FLOAT:
        //            anEvent.floatParameter = float.Parse(paramValue);
        //            break;
        //    }
        //}
    }
}
