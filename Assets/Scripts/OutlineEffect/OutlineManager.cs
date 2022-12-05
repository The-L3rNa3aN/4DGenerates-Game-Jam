using UnityEngine;

namespace cakeslice
{
    public class OutlineManager : MonoBehaviour
    {
        public Outline[] Outlines;

        private void Start() => Outlines = transform.GetComponentsInChildren<Outline>();

        private void OnEnable() => SetEnable();

        private void OnDisable() => SetDisable();


        public void SetEnable() {
            foreach (Outline o in Outlines)
            {
                o.enabled = true;
            }
        }

        public void SetDisable() {
            foreach (Outline o in Outlines)
            {
                o.enabled = false;
            }
        }
    }
}