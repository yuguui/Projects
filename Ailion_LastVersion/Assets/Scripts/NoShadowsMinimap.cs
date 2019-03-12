using UnityEngine;
using System.Collections;

public class ScriptName : MonoBehaviour {

    private float previousShadowDistance;

    void OnPreRender() {
        previousShadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    void OnPostRender() {
        QualitySettings.shadowDistance = previousShadowDistance;
    }
}
