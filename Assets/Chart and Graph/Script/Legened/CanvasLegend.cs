using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ChartAndGraph.Legened
{
    /// <summary>
    /// class for canvas legned. this class basiically creates the legned prefab for each category in the chart
    /// </summary>
    [ExecuteInEditMode]
    class CanvasLegend : MonoBehaviour
    {
        [SerializeField]
        private int fontSize;

        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                PropertyChanged();
            }
        }

        [SerializeField]
        private CanvasLegendItem legendItemPrefab;

        public CanvasLegendItem LegenedItemPrefab
        {
            get { return legendItemPrefab; }
            set
            {
                legendItemPrefab = value;
                PropertyChanged();
            }
        }

        [SerializeField]
        private AnyChart chart;

        List<UnityEngine.Object> mToDispose = new List<UnityEngine.Object>();
        bool mGenerateNext = false;
        public AnyChart Chart
        {
            get { return chart; }
            set
            {
                if (chart != null)
                    ((IInternalUse)chart).Generated -= CanvasLegend_Generated;
                chart = value;
                if(chart != null)
                    ((IInternalUse)chart).Generated += CanvasLegend_Generated;
                PropertyChanged();
            }
        }
        void Start()
        {
            if (chart != null)
                ((IInternalUse)chart).Generated += CanvasLegend_Generated;
            InnerGenerate();
        }
        void OnEnable()
        {
            if (chart != null)
                ((IInternalUse)chart).Generated += CanvasLegend_Generated;
            InnerGenerate();
        }
        void OnDisable()
        {
            if (chart != null)
                ((IInternalUse)chart).Generated -= CanvasLegend_Generated;
        //    Clear();
        }
        void OnDestory()
        {
            if (chart != null)
                ((IInternalUse)chart).Generated -= CanvasLegend_Generated;
            Clear();
        }
        private void CanvasLegend_Generated()
        {
           InnerGenerate();
        }
        protected void OnValidate()
        {
            if (chart != null)
                ((IInternalUse)chart).Generated += CanvasLegend_Generated;
            Generate();
        }
        protected void PropertyChanged()
        {
            Generate();
        }

        public void Clear()
        {
            CanvasLegendItem[] items = gameObject.GetComponentsInChildren<CanvasLegendItem>();
            for(int i=0; i<items.Length; i++)
            {
                if (items[i] == null || items[i].gameObject == null)
                    continue;
                ChartCommon.SafeDestroy(items[i].gameObject);
            }
            for(int i=0; i<mToDispose.Count; i++)
            {
                UnityEngine.Object obj = mToDispose[i];
                if (obj != null)
                    ChartCommon.SafeDestroy(obj);
            }
            mToDispose.Clear();
        }

        bool isGradientShader(Material mat)
        {
            if (mat.HasProperty("_ColorFrom") && mat.HasProperty("_ColorTo"))
                return true;
            return false;
        }

        Sprite CreateSpriteFromTexture(Texture2D t)
        {
            Sprite sp = Sprite.Create(t, new Rect(0f, 0f, (float)t.width, (float)t.height), new Vector2(0.5f, 0.5f));
            sp.hideFlags = HideFlags.DontSave;
            mToDispose.Add(sp);
            return sp;
        }

        Material CreateCanvasGradient(Material mat)
        {
            Material grad = new Material((Material)Resources.Load("Chart And Graph/Legend/CanvasGradient"));
            grad.hideFlags = HideFlags.DontSave;
            Color from = mat.GetColor("_ColorFrom");
            Color to = mat.GetColor("_ColorTo");
            grad.SetColor("_ColorFrom", from);
            grad.SetColor("_ColorTo", to);
            mToDispose.Add(grad);
            return grad;
        }

        public void Generate()
        {
            mGenerateNext = true;
        }

        void Update()
        {

            if (mGenerateNext == true)
                InnerGenerate();
        }

        private void InnerGenerate()
        {
            if (enabled == false || gameObject.activeInHierarchy == false)
                return;
            mGenerateNext = false;
            Clear();
            if (chart == null || legendItemPrefab == null)
                return;
            LegenedData inf = ((IInternalUse)chart).InternalLegendInfo;
            if (inf == null)
                return;
            foreach(LegenedData.LegenedItem item in inf.Items)
            {
                GameObject prefab =  (GameObject)GameObject.Instantiate(legendItemPrefab.gameObject);
                prefab.transform.SetParent(transform, false);
                ChartCommon.HideObject(prefab, true);
                CanvasLegendItem legendItemData = prefab.GetComponent<CanvasLegendItem>();
                if (legendItemData.Image != null)
                {
                    if (item.Material == null)
                        legendItemData.Image.material = null;
                    else
                    {
                        if (isGradientShader(item.Material))
                        {
                            legendItemData.Image.material = CreateCanvasGradient(item.Material);
                        }
                        else
                        {
                            legendItemData.Image.material = null;
                            Texture2D tex = item.Material.mainTexture as Texture2D;
                            if (tex != null)
                                legendItemData.Image.sprite = CreateSpriteFromTexture(tex);
                            legendItemData.Image.color = item.Material.color;
                        }
                    }
                }
                if (legendItemData.Text != null)
                {
					const string VELOCITY_BALL_ONE = "VelocityBallOne";
					const string VELOCITY_BALL_TWO = "VelocityBallTwo";
					const string ACCL_BALL_ONE = "AccelerationBallOne";
					const string ACCL_BALL_TWO = "AccelerationBallTwo";
					const string VELOCITY_HORIZONTAL = "VelocityHorizontal";
					const string VELOCITY_VERTICAL = "VelocityVertical";
					const string ACCL_HORIZONTAL = "AccelerationHorizontal";
					const string ACCL_VERTICAL = "AccelerationVertical";

					string VELOCITY_BALL_ONE_NAME = "Velocity (Ball One)";
					string VELOCITY_BALL_TWO_NAME = "Velocity (Ball Two)";
					string ACCL_BALL_ONE_NAME = "Acceleration (Ball One)";
					string ACCL_BALL_TWO_NAME = "Acceleration (Ball Two)";
					string VELOCITY_HORIZONTAL_NAME = "Velocity (Horizontal)";
					string VELOCITY_VERTICAL_NAME = "Velocity (Vertical)";
					string ACCL_HORIZONTAL_NAME = "Acceleration (Horizontal)";
					string ACCL_VERTICAL_NAME = "Acceleration (Vertical)";

					Color VELOCITY_BALL_ONE_COLOR = ColorManager.Instance.BallOneVelocityColor;
					Color VELOCITY_BALL_TWO_COLOR = ColorManager.Instance.BallTwoVelocityColor;
					Color ACCL_BALL_ONE_COLOR = ColorManager.Instance.BallOneAccelerationColor;
					Color ACCL_BALL_TWO_COLOR = ColorManager.Instance.BallOneAccelerationColor;
					Color VELOCITY_HORIZONTAL_COLOR = ColorManager.Instance.BallHorizontalVelocityColor;
					Color VELOCITY_VERTICAL_COLOR = ColorManager.Instance.BallVerticalVelocityColor;
					Color ACCL_HORIZONTAL_COLOR = ColorManager.Instance.BallHorizontalAccelerationColor;
					Color ACCL_VERTICAL_COLOR = ColorManager.Instance.BallVerticalAccelerationColor;

					string itemName = item.Name;
					Color textColor = Color.white;

					switch (itemName) {
					case VELOCITY_BALL_ONE:
						itemName = VELOCITY_BALL_ONE_NAME;
						textColor = VELOCITY_BALL_ONE_COLOR;
						break;
					case VELOCITY_BALL_TWO:
						itemName = VELOCITY_BALL_TWO_NAME;
						textColor = VELOCITY_BALL_TWO_COLOR;
						break;
					case ACCL_BALL_ONE:
						itemName = ACCL_BALL_ONE_NAME;
						textColor= ACCL_BALL_ONE_COLOR;
						break;
					case ACCL_BALL_TWO:
						itemName = ACCL_BALL_TWO_NAME;
						textColor = ACCL_BALL_TWO_COLOR;
						break;
					case VELOCITY_HORIZONTAL:
						itemName = VELOCITY_HORIZONTAL_NAME;
						textColor = VELOCITY_HORIZONTAL_COLOR;
						break;
					case VELOCITY_VERTICAL:
						itemName = VELOCITY_VERTICAL_NAME;
						textColor = VELOCITY_VERTICAL_COLOR;
						break;
					case ACCL_HORIZONTAL:
						itemName = ACCL_HORIZONTAL_NAME;
						textColor = ACCL_HORIZONTAL_COLOR;
						break; 
					case ACCL_VERTICAL:
						itemName = ACCL_VERTICAL_NAME;
						textColor = ACCL_VERTICAL_COLOR;
						break;
						
					}
					legendItemData.Text.text = itemName;

                    legendItemData.Text.fontSize = 14;
					legendItemData.Text.color = textColor;
                }
            }
        }
    }
}
