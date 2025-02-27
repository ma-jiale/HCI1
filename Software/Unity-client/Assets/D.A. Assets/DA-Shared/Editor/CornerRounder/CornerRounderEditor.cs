﻿using DA_Assets.DAI;
using UnityEditor;
using UnityEngine;

namespace DA_Assets.CR
{
    [CustomEditor(typeof(CornerRounder)), CanEditMultipleObjects]
    public class CornerRounderEditor : DAEditor<CornerRounderEditor, CornerRounder, BlackInspector>
    {
        public override void OnInspectorGUI()
        {
            gui.DrawGroup(new Group
            {
                GroupType = GroupType.Vertical,
                Style = gui.ColoredStyle.Background,
                Body = () =>
                {
                    monoBeh.independent = gui.CheckBox(new GUIContent("Independent"), monoBeh.independent);

                    gui.Space15();

                    if (monoBeh.independent)
                    {
                        gui.DrawGroup(new Group
                        {
                            GroupType = GroupType.Horizontal,
                            Body = () =>
                            {
                                DrawField(0, true);
                                DrawField(1, false);
                            }
                        });

                        gui.Space15();

                        gui.DrawGroup(new Group
                        {
                            GroupType = GroupType.Horizontal,
                            Body = () =>
                            {
                                DrawField(3, true);
                                DrawField(2, false);
                            }
                        });
                    }
                    else
                    {
                        gui.DrawGroup(new Group
                        {
                            GroupType = GroupType.Horizontal,
                            Body = () =>
                            {
                                Rect rect = DrawIcon(4);

                                int val = (int)monoBeh.radiiSerialized[0];
                                val = gui.IntField(new GUIContent(""), val);

                                gui.DragZoneInt(rect, ref val);

                                if (val < 0)
                                    val = 0;

                                monoBeh.radiiSerialized[0] = val;
                                monoBeh.radiiSerialized[1] = val;
                                monoBeh.radiiSerialized[2] = val;
                                monoBeh.radiiSerialized[3] = val;
                            }
                        });
                    }

                    monoBeh.Refresh();
                }
            });
        }

        private Rect DrawIcon(int index)
        {
            int iconOffset = 4;
            Rect rect = EditorGUILayout.GetControlRect(false, GUILayout.Width(25), GUILayout.Height(25));
            rect.y -= iconOffset;
            EditorGUI.LabelField(rect, new GUIContent(gui.Data.Resources.CornerIcons[index]));
            return rect;
        }

        private void DrawField(int index, bool state)
        {
            int val = (int)monoBeh.radiiSerialized[index];

            if (!state)
                val = gui.IntField(new GUIContent(""), val);

            Rect rect = DrawIcon(index);

            if (state)
                val = gui.IntField(new GUIContent(""), val);

            gui.DragZoneInt(rect, ref val);

            if (val < 0)
                val = 0;

            monoBeh.radiiSerialized[index] = (float)val;
        }
    }
}
