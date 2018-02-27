using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Singleton<MenuManager> {
    GameToolManager gameToolManager;
    GameObject[] menuItems;

    int numOfElementsInaRow = 1;
    float maxDimension = 0.3f;

	// Use this for initialization
	void Start () {
        gameToolManager = GameToolManager.Instance;
        menuItems = gameToolManager.GameToolPrefabs;
        placeAllMenuItems();
        GameObject.Find("AudioManager").GetComponent<Martana>().Sayit("The menu has been loaded");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void placeAllMenuItems() {
        int row = 0;
        int column = 0;
        for (int i = 0; i < menuItems.Length; i++) {
            if (i > 0 && i % numOfElementsInaRow == 0) {
                row++;
                column = 0;
            }
            
            placeMenuItem(menuItems[i], row, column);
            column++;
        }
    }

    void placeMenuItem(GameObject menuItemGameObject, int row, int column) {
        Debug.Log("Placing menu item: " + menuItemGameObject.name); 
        float spacing = 0.1f;
        float dimension = 0.3f;
        float camerayoffset = Camera.main.transform.rotation.eulerAngles.y;
        Vector3 position = new Vector3 (row*(spacing+dimension), -column * (spacing+dimension), 0);
        Quaternion rot180degrees = Quaternion.Euler(new Vector3(0,180f+camerayoffset,0));
        GameObject menuItem = Instantiate(menuItemGameObject, position,rot180degrees , transform);
        menuItem.name = menuItemGameObject.name;
        Rigidbody rb = menuItem.GetComponent<Rigidbody>();
        menuItem.AddComponent<GazeAnimationHandler>();
        menuItem.AddComponent<CameraFacing>().isBackwards = true; 

        if (rb != null) {
            rb.isKinematic = true;
			Destroy(rb);
        }
        rescaleGameObject(menuItem);

    }

    void rescaleGameObject(GameObject rescaleObject) {
        Debug.Log("rescale gameObjectName: " + rescaleObject.name);
        Renderer renderer = rescaleObject.GetComponent<Renderer>();
        Collider collider = rescaleObject.GetComponent<Collider>();
        Vector3 size = new Vector3(1f,1f,1f);
        if (renderer == null && collider == null)
        {
            Debug.Log("nothing exists");
            return;
        }
        else if (renderer == null)
        {
            Debug.Log("collider exists");
            size = collider.bounds.size;
        }
        else
        {
            Debug.Log("renderer exists");
            size = renderer.bounds.size;
        }
        Debug.Log("size: " + size);
        float x = size.x;
        float y = size.y;
        float z = size.z;
        float maxWidthHeight = Mathf.Max(x, y, z);
        float scale = maxDimension / maxWidthHeight;
        Debug.Log("scale: " + scale);
        Debug.Log("localScale BEFORE: " + rescaleObject.transform.localScale);
        //rescaleObject.transform.localScale = Vector3.one * scale;
        
        rescaleObject.transform.localScale *= scale;
        Debug.Log("localScale AFTER: " + rescaleObject.transform.localScale);

        if (renderer == null)
        {
            Debug.Log("collider exists");
            size = collider.bounds.size;
        }
        else
        {
            Debug.Log("renderer exists");
            size = renderer.bounds.size;
        }

        Debug.Log("new size: "+size);
    }
}
