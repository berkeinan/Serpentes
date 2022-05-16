using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphicCode : MonoBehaviour
{
    public float xSize, ySize;
    public float dist;
    public float labelSize;
    public float arrowSize;
    public float dotSize;
    public float lineThickness;
    public string[] verlabel, horlabel;
    public float[] points;
    public string[] dotTexts;
    public GameObject lineobject;
    public GameObject circleobject;
    public GameObject textObject;
    public Color lineColor;
    public Color nodeColor;
    public Color edgeColor;
    public Color arrowColor;
    public Color labelColor;
    public Color textColor;
    public float labelThickness;
    public float textSize;
    public string year;
    public string unit;
    public float maxval;
    public List<GameObject> circles;
    bool load = false;
    private void Start()
    {
        DrawLine(0, 0, 0, ySize, 5, edgeColor);
        DrawLine(0, 0, xSize, 0, 5, edgeColor);
        DrawLine(xSize, 0, xSize - arrowSize, arrowSize, 5, arrowColor);
        DrawLine(xSize, 0, xSize - arrowSize, -arrowSize, 5, arrowColor);
        DrawLine(0, ySize, -arrowSize, ySize - arrowSize, 5, arrowColor);
        DrawLine(0, ySize, arrowSize, ySize - arrowSize, 5, arrowColor);
        CreateText(xSize + 20, 0, "Aylar", textSize * 1.5f, Color.white, 50);
        CreateText(0, ySize + 20, unit, textSize * 1.5f, Color.white, 0);
        if (verlabel.Length == 1)
        {
            DrawLine(-labelSize / 2, (ySize - dist) / 2, labelSize / 2, (ySize - dist) / 2, labelThickness, labelColor);
            CreateText(-labelSize / 2 - 10, (ySize - dist) / 2, verlabel[0], textSize, textColor, 0);
        }
        else
        {
            for (int i = 0; i < verlabel.Length; i++)
            {
                DrawLine(labelSize / 2, ((float)i / (verlabel.Length - 1)) * (ySize - dist), -labelSize / 2, ((float)i / (verlabel.Length - 1)) * (ySize - dist), labelThickness, labelColor);
                CreateText(-labelSize / 2 - 10, ((float)i / (verlabel.Length - 1)) * (ySize - dist), verlabel[i], textSize, textColor, 0);
            }
        }
        if (horlabel.Length == 1)
        {
            DrawLine((xSize - dist) / 2, -labelSize / 2, (xSize - dist) / 2, labelSize / 2, labelThickness, labelColor);
            CreateText((xSize - dist) / 2, -labelSize / 2 - 10, horlabel[0], textSize, textColor, 50);
        }
        else
        {
            for (int i = 0; i < horlabel.Length; i++)
            {
                DrawLine(((float)i / (horlabel.Length - 1)) * (xSize - dist), labelSize / 2, ((float)i / (horlabel.Length - 1)) * (xSize - dist), -labelSize / 2, labelThickness, labelColor);
                CreateText(((float)i / (horlabel.Length - 1)) * (xSize - dist), -labelSize / 2 - 10, horlabel[i], textSize, textColor, 50);
            }
        }
        if (points.Length == 1)
        {
            float stpos = 0;
            DrawCircle((xSize - dist) / 2, points[0] * 500 / maxval, dotSize, nodeColor, horlabel[Mathf.FloorToInt(stpos)], points[0], -1, "");
        }
        else
        {
            for (int i = 1; i < points.Length; i++)
            {
                DrawLine(((float)(i - 1) / (points.Length - 1)) * (xSize - dist), 500 / maxval * points[i - 1], ((float)i / (points.Length - 1)) * (xSize - dist), 500 / maxval * points[i], lineThickness, lineColor);
            }
            for (int i = 0; i < points.Length; i++)
            {
                int stpos = i;
                string month;
                switch (stpos % 12)
                {
                    case 0: month = "Ocak"; break;
                    case 1: month = "Þubat"; break;
                    case 2: month = "Mart"; break;
                    case 3: month = "Nisan"; break;
                    case 4: month = "Mayýs"; break;
                    case 5: month = "Haziran"; break;
                    case 6: month = "Temmuz"; break;
                    case 7: month = "Aðustos"; break;
                    case 8: month = "Eylül"; break;
                    case 9: month = "Ekim"; break;
                    case 10: month = "Kasým"; break;
                    default: month = "Aralýk"; break;
                }
                string y = year;
                stpos /= 12;
                if (year == "3")
                {
                    y = (stpos + 2020).ToString();
                }
                else if (year == "5")
                {
                    y = (stpos + 2018).ToString();
                }
                DrawCircle(((float)i / (points.Length - 1)) * (xSize - dist), points[i] * 500 / maxval, dotSize, nodeColor, month, points[i], (i == 0) ? -1 : points[i - 1], y);
            }
        }
        gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1);
        load = true;
    }
    void DrawLine(float p1x, float p1y, float p2x, float p2y, float thickness, Color color)
    {
        GameObject line = Instantiate(lineobject);
        Vector2 fpos = new Vector2(p1x + transform.position.x, p1y + transform.position.y);
        Vector2 spos = new Vector2(p2x + transform.position.x, p2y + transform.position.y);
        float angle = Mathf.Atan2(spos.y - fpos.y, spos.x - fpos.x) * 180 / Mathf.PI;
        line.GetComponent<RectTransform>().position = fpos;
        line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
        float length = Mathf.Sqrt(Mathf.Abs(fpos.x - spos.x) * Mathf.Abs(fpos.x - spos.x) + Mathf.Abs(fpos.y - spos.y) * Mathf.Abs(fpos.y - spos.y));
        line.GetComponent<RectTransform>().localScale = new Vector3(length / 100, thickness / 100, 1);
        line.GetComponent<Image>().color = color;
        line.transform.parent = gameObject.transform;
    }
    void DrawCircle(float x, float y, float r, Color color, string month, float value, float oldvalue, string ye)
    {
        GameObject circle = Instantiate(circleobject);
        char ch = '+';
        if (oldvalue > value) ch = '-';
        string delta = (Mathf.Abs(value - oldvalue)).ToString();
        if (oldvalue == -1) circle.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = month + " " + ye + "\n" + value + unit + " (-)";
        else circle.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = month + " " + ye + "\n" + value + unit + " (" + ch + delta + unit + ")";
        circle.GetComponent<RectTransform>().position = new Vector2(transform.position.x + x, transform.position.y + y);
        circle.GetComponent<RectTransform>().localScale = new Vector3(r, r, 1);
        circle.GetComponent<Image>().color = color;
        circle.transform.parent = gameObject.transform;
        circles.Add(circle.transform.GetChild(0).gameObject);
    }
    void CreateText(float xpos, float ypos, string textstr, float size, Color color, float angle)
    {
        GameObject text = Instantiate(textObject);
        text.GetComponent<TextMeshProUGUI>().text = textstr;
        text.GetComponent<TextMeshProUGUI>().color = color;
        text.GetComponent<TextMeshProUGUI>().fontSize = size;
        text.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
        text.GetComponent<RectTransform>().position = new Vector2(transform.position.x + xpos, transform.position.y + ypos);
        text.transform.parent = gameObject.transform;
    }
    private void Update()
    {
        if (load)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                if (circles[i])
                    circles[i].SetActive(false);
            }
            for (int i = 0; i < Input.touchCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[i].position), Vector2.zero);
                if (hit.collider != null && hit.collider.tag == "circle") hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}