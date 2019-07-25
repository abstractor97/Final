using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Points))] //指定要编辑的脚本对象
public class PointsEditor : Editor
{
    private Points m_Target;

    private EventEmitter eventEmitter;

    //重写OnInspectorGUI方法，当激活此面板区域时调用
    public override void OnInspectorGUI()
    {
        //加入此句，不影响原在Inspector绘制的元素
         base.OnInspectorGUI();

        //获取指定脚本对象
        m_Target = target as Points;

       // m_Target.eventSend = EditorGUILayout.ObjectField("事件发送器", this.eventEmitter, typeof(EventEmitter), true) as EventEmitter;
       




    }
}