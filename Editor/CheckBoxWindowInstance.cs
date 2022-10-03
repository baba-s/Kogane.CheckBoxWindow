using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class CheckBoxWindowInstance : EditorWindow
    {
        private const int ROW_HEIGHT = 18;

        private IReadOnlyList<ICheckBoxWindowData>         m_dataList = Array.Empty<CheckBoxWindowData>();
        private Action<IReadOnlyList<ICheckBoxWindowData>> m_onOk;

        private SearchField m_searchField;
        private GUIStyle    m_hoverStyle;
        private GUIStyle[]  m_alternatingRowStyleArray;
        private string      m_filteringText = string.Empty;
        private Vector2     m_scrollPosition;

        public void Open
        (
            string                                     title,
            IReadOnlyList<ICheckBoxWindowData>         dataList,
            Action<IReadOnlyList<ICheckBoxWindowData>> onOk
        )
        {
            titleContent   = new GUIContent( title );
            wantsMouseMove = true;
            m_dataList     = dataList;
            m_onOk         = onOk;
        }

        private void OnGUI()
        {
            m_searchField ??= new SearchField();
            m_hoverStyle  ??= CreateGUIStyle( new Color32( 44, 93, 135, 255 ) );

            m_alternatingRowStyleArray ??= new[]
            {
                CreateGUIStyle( new Color32( 63, 63, 63, 255 ) ),
                CreateGUIStyle( new Color32( 56, 56, 56, 255 ) ),
            };

            using ( new EditorGUILayout.HorizontalScope() )
            {
                if ( GUILayout.Button( "Select all", GUILayout.Width( 80 ) ) )
                {
                    foreach ( var x in m_dataList )
                    {
                        x.IsChecked = true;
                    }
                }

                if ( GUILayout.Button( "Deselect all", GUILayout.Width( 80 ) ) )
                {
                    foreach ( var x in m_dataList )
                    {
                        x.IsChecked = false;
                    }
                }

                m_filteringText = m_searchField.OnToolbarGUI( m_filteringText );
            }

            using ( var scrollViewScope = new EditorGUILayout.ScrollViewScope( m_scrollPosition ) )
            {
                var mousePosition = Event.current.mousePosition;
                var y             = 0;

                for ( var i = 0; i < m_dataList.Count; i++ )
                {
                    var data = m_dataList[ i ];
                    var name = data.Name;

                    if ( !name.Contains( m_filteringText, StringComparison.OrdinalIgnoreCase ) ) continue;

                    var isHover = ( ( int )mousePosition.y ) / ROW_HEIGHT == y;
                    var style   = isHover ? m_hoverStyle : m_alternatingRowStyleArray[ i % 2 ];

                    using var hs = new EditorGUILayout.HorizontalScope( style );
                    using var vs = new EditorGUILayout.VerticalScope();

                    GUILayout.FlexibleSpace();
                    data.IsChecked = EditorGUILayout.ToggleLeft( name, data.IsChecked );
                    GUILayout.FlexibleSpace();

                    y++;
                }

                m_scrollPosition = scrollViewScope.scrollPosition;
            }

            using ( new EditorGUILayout.HorizontalScope() )
            {
                if ( GUILayout.Button( "OK" ) )
                {
                    m_onOk( m_dataList );
                    Close();
                }

                if ( GUILayout.Button( "Cancel" ) )
                {
                    Close();
                }
            }
        }

        private static GUIStyle CreateGUIStyle( Color color )
        {
            var background = new Texture2D( 1, 1 );
            background.SetPixel( 0, 0, color );
            background.Apply();

            var style = new GUIStyle();
            style.normal.background = background;
            style.fixedHeight       = ROW_HEIGHT;

            return style;
        }
    }
}