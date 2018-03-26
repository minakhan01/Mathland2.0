using UnityEngine;
using System.Collections;
using ChartAndGraph;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{

    public GraphChartBase Chart;
    public RectTransform LastPoint;
    public RectTransform Area;

    private DoubleRect currentRect = new DoubleRect();
	// Use this for initialization
	void Start ()
    {
        if (Chart != null)
        {
            Chart.OnRedraw.AddListener(Redraw);
        }
    }

	void Redraw()
    {
        if (Chart == null)
            return;

//        if(Chart.IsRectVisible(currentRect) == false)
//        {
//            
//            double endX = (float)(Chart.HorizontalScrolling + Chart.DataSource.HorizontalViewSize);
//			double x = (endX - 1f);
//            double y = (float)Chart.VerticalScrolling;
//            double endY = (float)Chart.DataSource.GetMaxValue(1, false);
//            currentRect = new DoubleRect(x, y, 200, endY - y);
//			Debug.Log ("Marker endX: " + endX + " x: " + x);
//        }
//
//        DoubleRect trimRect;
//        if (Chart.TrimRect(currentRect, out trimRect))
//        {
//            Chart.RectToCanvas(Area, trimRect);
//        }


        DoubleVector3 last;
		if (Chart.DataSource.GetLastPoint("VelocityBallOne", out last))
        {
            Vector3 pos;
			if(Chart.PointToWorldSpace(out pos, last.x, 0, "VelocityBallOne"))
            {
                if(LastPoint != null)
                {
                    LastPoint.transform.position = pos;
					Debug.Log("Marker last position: "+LastPoint.transform.position);
					Area.transform.position = new Vector3( pos.x + 200, pos.y, pos.z);
					Debug.Log("Marker area position: "+Area.transform.position);
                }
            }
        }

    }
	// Update is called once per frame
	void Update () {
        double mx, my;
    }
}
