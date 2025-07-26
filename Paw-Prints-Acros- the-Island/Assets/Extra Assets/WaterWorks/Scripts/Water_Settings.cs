using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Water_Settings : MonoBehaviour
{
    Material waterVolume;
    Material waterMaterial;

    void LateUpdate()
    {
        if (waterVolume == null)
        {
            waterVolume = (Material)Resources.Load("Water_Volume");
        }

        if (waterMaterial == null)
        {
            waterMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        }

        if (waterVolume != null && waterMaterial != null)
        {
            Vector4 posValue = new Vector4(
                0,
                (waterVolume.GetVector("bounds").y / -2) + transform.position.y + (waterMaterial.GetFloat("_Displacement_Amount") / 3),
                0,
                0
            );

            waterVolume.SetVector("pos", posValue);

#if UNITY_EDITOR
            // Verificaci�n opcional para confirmar que se est� actualizando:
            Debug.Log("Water Volume Pos actualizado a: " + posValue);
#endif
        }
    }
}
