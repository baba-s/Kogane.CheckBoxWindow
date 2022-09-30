using System;
using System.Collections.Generic;
using Kogane.Internal;
using UnityEngine;

namespace Kogane
{
    public static class CheckBoxWindow
    {
        public static void Open
        (
            string                                     title,
            IReadOnlyList<ICheckBoxWindowData>         dataList,
            Action<IReadOnlyList<ICheckBoxWindowData>> onOk
        )
        {
            // var window = EditorWindow.GetWindow<CheckBoxWindowInstance>();
            var window = ScriptableObject.CreateInstance<CheckBoxWindowInstance>();
            window.ShowAuxWindow();
            window.Open( title, dataList, onOk );
        }
    }
}