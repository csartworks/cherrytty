using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public struct Position
{
    public int x, y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static Position operator +(Position p1, Position p2)
    {
        return new(p1.x + p2.x, p1.y + p2.y);
    }
    public static implicit operator (int, int)(Position p) => (p.x, p.y);
    public static implicit operator Position((int, int) i) => new(i.Item1, i.Item2);
}
public class CursorBehaviour : MonoBehaviour
{
    private Image _image;
    private RectTransform _rt;
    [SerializeField] private float _interval = 0.5f;
    private Position _pos;
    public Position Pos
    {
        get => _pos;
        set
        {
            if (value.x >= TTYDrawer.hc)
            {
                value.x -= TTYDrawer.hc;
                value.y++;
            }
            _pos = value;
            _rt.anchoredPosition = new(36 * _pos.x, -36 * _pos.y);
        }
    }

    internal void Advance()
    {
        Pos += (1, 0);
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();
        StartCoroutine(BlinkCursor());
    }
    private IEnumerator BlinkCursor()
    {
        while (true)
        {
            _image.enabled = true;
            yield return new WaitForSeconds(_interval);
            _image.enabled = false;
            yield return new WaitForSeconds(_interval);
        }
    }
}
