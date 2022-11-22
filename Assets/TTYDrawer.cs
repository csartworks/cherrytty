using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TTYDrawer : MonoBehaviour
{
    public static int Height => Screen.height;
    public static int Width => Screen.width;
    [SerializeField] private int _fontSize = 36;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _cursorPrefab;
    public static TMP_Text[,] s_cells;
    public static int vc;
    public static int hc;
    private TTY _tty;

    private void Awake()
    {
        vc = Height / _fontSize;
        hc = Width / _fontSize;
        s_cells = new TMP_Text[hc, vc];
        for (int i = 0; i < vc; i++)
        {
            for (int j = 0; j < hc; j++)
            {
                s_cells[j, i] = Instantiate(_cellPrefab, transform).GetComponent<TMP_Text>();
            }
        }
        var cursor = Instantiate(_cursorPrefab, transform.parent);
        _tty = new TTY(cursor.GetComponent<CursorBehaviour>(), s_cells);
    }
    private void OnAlphabets()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            _tty.Write(TTY.sb.ToString());
        }
    }
}
