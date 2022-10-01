# Kogane Check Box Window

チェックボックス付きのリスト

## 使用例

```csharp
using System.Linq;
using Kogane;
using UnityEditor;
using UnityEngine;

public static class Example
{
    private sealed class Data : ICheckBoxWindowData
    {
        public string Name      { get; }
        public bool   IsChecked { get; set; }

        public Data( string name )
        {
            Name = name;
        }
    }

    [MenuItem( "Tools/Hoge" )]
    public static void Hoge()
    {
        var names = new[]
        {
            "フシギダネ",
            "フシギソウ",
            "フシギバナ",
        };

        var dataArray = names
                .Select( x => new Data( x ) )
                .ToArray()
            ;

        CheckBoxWindow.Open
        (
            title: "Select Characters",
            dataList: dataArray,
            onOk: _ =>
            {
                var values = dataArray
                        .Where( x => x.IsChecked )
                        .Select( x => x.Name )
                    ;

                Debug.Log( string.Join( ", ", values ) );
            }
        );
    }
}
```