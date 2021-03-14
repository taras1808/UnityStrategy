using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycastProvider : MonoBehaviour
{
    
    public class RaycastStruct<T> where T : class
    {
        public delegate void Callback(T obj, Vector3 point);
        public delegate void EmptyCallback();
            
        public float RaycastDistance { get; set; }
        public LayerMask RaycastLayer { get; set; }
        public bool GetParrent { get; set; }
        

        public Callback onStart { get; set; }
        public Callback onChange { get; set; }
        public EmptyCallback onEnd { get; set; }

        private T _lastTransform = null;
        private Vector3 _lastPoint;

        public RaycastStruct(float raycastDistance, LayerMask raycastLayer, bool getParrent)
        {
            RaycastDistance = raycastDistance;
            RaycastLayer = raycastLayer;
            GetParrent = getParrent;
        }

        public void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, RaycastDistance, RaycastLayer, QueryTriggerInteraction.Ignore))
            {
                Transform t = hit.transform;

                if (GetParrent)
                {
                    while (t.parent != null)
                    {
                        t = t.parent;
                    }
                }

                T obj = t.GetComponent<T>();

                if (obj != null && _lastTransform == null)
                {
                    _lastTransform = obj;
                    _lastPoint = hit.point;
                    onStart(obj, _lastPoint);
                }
                else if (obj == null && _lastTransform != null)
                {
                    _lastTransform = null;
                    onEnd();
                }
                else if(obj != null && _lastTransform != null && hit.point != _lastPoint)
                {
                    _lastTransform = obj;
                    _lastPoint = hit.point;
                    onChange(_lastTransform, _lastPoint);
                }
            }
        }
    }

    public ArrayList Structures { get; set; }
    
    void Update()
    {
        foreach (RaycastStruct<Object> structure in Structures)
        {
            structure.Update();
        }
    }
}
