using UnityEngine;
using System;

namespace C4PhasMod
{
    class Drawing
    {
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

        public static void DrawString(Vector2 position, string label, Color color, int fontSize, bool centered = true)
        {
            GUI.color = color;
            StringStyle.fontSize = fontSize;
            StringStyle.normal.textColor = color;
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);
            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content, StringStyle);
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            GUI.depth = 0;
            var rad2deg = 360 / (Math.PI * 2);
            Vector2 d = end - start;
            float a = (float)rad2deg * Mathf.Atan(d.y / d.x);
            
            if (d.x < 0)
                a += 180;

            int width2 = (int)Mathf.Ceil(width / 2);

            GUIUtility.RotateAroundPivot(a, start);
            GUI.color = color;
            GUI.DrawTexture(new Rect(start.x, start.y - width2, d.magnitude, width), Texture2D.whiteTexture, ScaleMode.StretchToFill);
            GUIUtility.RotateAroundPivot(-a, start);
        }

        public static void DrawBox(Vector2 position, Vector2 size, Color color, bool centered = true)
        {
            var upperLeft = centered ? position - size / 2f : position;
            GUI.color = color;
            GUI.DrawTexture(new Rect(position, size), Texture2D.whiteTexture, ScaleMode.StretchToFill);
        }

        public static void DrawBoxOutline(Vector2 center, float width, float height, Color color)
        {
            Vector2 upperLeft = center - new Vector2(width / 2, height / 2);
            Vector2 lowerLeft = center + new Vector2(-(width / 2), height / 2);
            Vector2 upperRight = center + new Vector2(width / 2, -(height / 2));
            Vector2 lowerRight = center + new Vector2(width / 2, height / 2);
            DrawLine(upperLeft, lowerLeft, color, 2);
            DrawLine(lowerLeft, lowerRight, color, 2);
            DrawLine(lowerRight, upperRight, color, 2);
            DrawLine(upperRight, upperLeft, color, 2);
        }
    }
}
