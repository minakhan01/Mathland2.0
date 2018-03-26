using UnityEngine;
using System.Collections;
using ChartAndGraph;
using UnityEngine.UI;

public class RewindMarker : MonoBehaviour {

	public GraphChartBase Chart;
	public RectTransform LastPoint;
	public RectTransform Area;
	DoubleVector3 last;


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
		if (!RewindManager.Instance.isRewinding) {
			if (Chart.IsRectVisible (currentRect) == false) {

				double endX = (float)(Chart.HorizontalScrolling + Chart.DataSource.HorizontalViewSize);
				double x = endX - 1f;
				double y = (float)Chart.VerticalScrolling;
				double endY = (float)Chart.DataSource.GetMaxValue (1, false);
				currentRect = new DoubleRect (x, y, endX - x, endY - y);
			}

			DoubleRect trimRect;
			if (Chart.TrimRect (currentRect, out trimRect)) {
				Chart.RectToCanvas (Area, trimRect);
			}



			if (Chart.DataSource.GetLastPoint ("VelocityBallOne", out last)) {
				Vector3 pos;
				if (Chart.PointToWorldSpace (out pos, last.x, 0, "VelocityBallOne")) {
					if (LastPoint != null) {
						LastPoint.transform.position = pos;
						Area.transform.position = pos;
					}
				}
			}
		} else {
			double currentX = (RewindManager.Instance.sliderValue * last.x);
			Vector3 pos;
			if (Chart.PointToWorldSpace (out pos, currentX, 0, "VelocityBallOne")) {
				if (LastPoint != null) {
					LastPoint.transform.position = pos;
					Area.transform.position = pos;
				}
			}
//			DoubleVector3 current;
//			if (Chart.DataSource.GetPoint ("VelocityBallOne", last, out current)) {
//				Vector3 pos;
//				if (Chart.PointToWorldSpace (out pos, currentX, 0, "VelocityBallOne")) {
//					if (LastPoint != null) {
//						LastPoint.transform.position = pos;
//						Area.transform.position = pos;
//					}
//				}
//			}
		}
		 

	}

	void startRewinding() {
		
	}

	// Update is called once per frame
	void Update () {
		double mx, my;
	}
}
