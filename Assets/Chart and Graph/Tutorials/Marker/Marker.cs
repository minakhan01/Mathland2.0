using UnityEngine;
using System.Collections;
using ChartAndGraph;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{

    public GraphChartBase Chart;
    public RectTransform LastPoint;
    public RectTransform Area;
	DoubleVector3 last;

    private DoubleRect currentRect = new DoubleRect();
	// Use this for initialization
	void Start ()
    {
//        if (Chart != null)
//        {
//            Chart.OnRedraw.AddListener(Redraw);
//        }
    }

	void Update()
    {
        if (Chart == null)
            return;
		if (RewindManager.Instance.isRecording) {
//			Debug.Log ("Marker !isRewinding drawRewinderRectangleAndPoint");
			drawRectangleAndPoint ();
		} else {
			Debug.Log ("Marker isRewinding drawRewinderRectangleAndPoint");
			drawRewinderRectangleAndPoint ();
		}
        


    }

	void drawRewinderRectangleAndPoint() {
		string categoryOneName = Chart.GetComponent<GraphHandler> ().getCategoryOne ();
		Debug.Log("Marker categoryOneName: "+categoryOneName);
		double ratio = RewindManager.Instance.sliderValue;
		ratio = RewindManager.Instance.rewindRatio;
		Debug.Log ("Marker ratio: "+ratio);
		double currentX = (ratio * last.x);
		Vector3 pos;
		if (Chart.PointToWorldSpace (out pos, currentX, 0, categoryOneName)) {
			if (LastPoint != null) {
				Debug.Log ("Marker last point is not null");
				LastPoint.transform.position = new Vector3( pos.x, pos.y, pos.z);
				Area.transform.position = new Vector3(pos.x + 200, pos.y, pos.z);
//				Area.transform.position = pos;
			}
		}
	}

	void drawRectangleAndPoint() {
		string categoryOneName = Chart.GetComponent<GraphHandler> ().getCategoryOne ();
		if (Chart.DataSource.GetLastPoint("VelocityBallOne", out last))
		{
			Vector3 pos;
			if(Chart.PointToWorldSpace(out pos, last.x, 0, categoryOneName))
			{
				if(LastPoint != null)
				{
					LastPoint.transform.position = pos;
//					Debug.Log("Marker last position: "+LastPoint.transform.position);
					Area.transform.position = new Vector3( pos.x + 200, pos.y, pos.z);
//					Debug.Log("Marker area position: "+Area.transform.position);
				}
			}
		}
	}

//	// Update is called once per frame
//	void Update () {
//        double mx, my;
//    }
}
