using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TTY
{
    private CursorBehaviour _cursor;
    private TMP_Text[,] _cells;
    private ProcessStartInfo bash;
    public static StringBuilder sb = new();

    public TTY(CursorBehaviour cursor, TMP_Text[,] cells)
    {
        _cursor = cursor;
        _cells = cells;

        Process p = new();
        bash = new("cmd.exe")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardInput = true,
            RedirectStandardError = true,
        };
        p.StartInfo = bash;
        p.OutputDataReceived += OnData;
        p.Start();
        p.BeginOutputReadLine();
        p.StandardInput.WriteLine("dir");
        p.StandardInput.Flush();
    }

    private void OnData(object sender, DataReceivedEventArgs e)
    {
        // Write(e.Data);
        // UnityEngine.Debug.Log(e.Data);
        sb.Append(e.Data);
    }

    public void Write(string s)
    {
        int len = s.Length;
        for (int i = 0; i < len; i++)
        {
            WriteSingle(s[i].ToString());
        }
    }
    private void WriteSingle(string s)
    {
        _cells[_cursor.Pos.x, _cursor.Pos.y].text = s;
        _cursor.Advance();
    }
}