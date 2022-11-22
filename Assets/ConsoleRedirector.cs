using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Redirects writes to System.Console to Unity3D's Debug.Log.
/// </summary>
/// <author>
/// Jackson Dunstan, http://jacksondunstan.com/articles/2986
/// </author>
public class ConsoleRedirector : MonoBehaviour
{
    private static TMP_Text s_console;
    public TMP_Text console;
    private void Awake()
    {
        s_console = console;
    }
    private class UnityTextWriter : TextWriter
    {
        private readonly StringBuilder buffer = new();

        public override void Flush()
        {
            s_console.text = buffer.ToString();
            buffer.Length = 0;
        }

        public override void Write(string value)
        {
            buffer.Append(value);
            if (value != null)
            {
                var len = value.Length;
                if (len > 0)
                {
                    var lastChar = value[len - 1];
                    if (lastChar == '\n')
                    {
                        Flush();
                    }
                }
            }
        }

        public override void Write(char value)
        {
            buffer.Append(value);
            if (value == '\n')
            {
                Flush();
            }
        }

        public override void Write(char[] value, int index, int count)
        {
            Write(new string(value, index, count));
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }

    public static void Redirect()
    {
        Console.SetOut(new UnityTextWriter());
    }
}