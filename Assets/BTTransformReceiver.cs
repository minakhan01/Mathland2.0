using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BTTransformReceiver {

	void updateRotation (Quaternion rotation);

	void updateAcceleration (Vector3 acceleration);

	void updateStretch (int stretch);
}
