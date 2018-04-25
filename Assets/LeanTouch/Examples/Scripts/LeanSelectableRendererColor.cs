using UnityEngine;
using System.Collections.Generic;

namespace Lean.Touch
{
	// This script allows you to change the color of the Renderer attached to the current GameObject
	[RequireComponent(typeof(Renderer))]
	public class LeanSelectableRendererColor : LeanSelectableBehaviour
	{
		[Tooltip("Automatically read the DefaultColor from the material?")]
		public bool AutoGetDefaultColor;

		[Tooltip("The default color given to the materials")]
		public Color DefaultColor = Color.white;

		[Tooltip("The color given to the materials when selected")]
		public Color SelectedColor = Color.green;

		[Tooltip("Should the materials get cloned at the start?")]
		public bool CloneMaterials = true;

		[System.NonSerialized]
		private Renderer cachedRenderer;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
			if (cachedRenderer == null) cachedRenderer = GetComponent<Renderer>();

			if (AutoGetDefaultColor == true)
			{
				var material0 = cachedRenderer.sharedMaterial;

				if (material0 != null)
				{
					DefaultColor = material0.color;
				}
			}

			if (CloneMaterials == true)
			{
				cachedRenderer.sharedMaterials = cachedRenderer.materials;
			}
		}

		public void OnSelectObject()
		{
			//GameStateManager.currentGameState = GameStateManager.gameState.PLAY;
			//GameStateManager.switchDisplayState ();
			GameStateManager.switchGameStateMode(GameStateManager.gameState.BUILD);
			ChangeColor(SelectedColor);
		}

		protected override void OnSelect(LeanFinger finger)
		{
			//GameStateManager.currentGameState = GameStateManager.gameState.PLAY;
			//GameStateManager.switchDisplayState ();
            GameStateManager.switchGameStateMode(GameStateManager.gameState.BUILD);
			ChangeColor(SelectedColor);
		}

		protected override void OnDeselect()
		{
			//GameStateManager.currentGameState = GameStateManager.gameState.BUILD;
			//GameStateManager.switchDisplayState ();
            GameStateManager.switchGameStateMode(GameStateManager.gameState.PLAY);
			ChangeColor(DefaultColor);
		}

		private void ChangeColor(Color color)
		{
			if (cachedRenderer == null) cachedRenderer = GetComponent<Renderer>();

			var materials = cachedRenderer.sharedMaterials;

			for (var i = materials.Length - 1; i >= 0; i--)
			{
				materials[i].color = color;
			}
		}
	}
}