using UnityEngine;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/VisibilityBinder")]
    public class RootBinder : MonoBehaviour
    {
        public MonoBehaviour Model;

        void Awake()
        {
            var children = this.GetComponentsInChildren<BinderBase>(true);
            for (int i = 0; i < children.Length; i++)
            {
                children[i].SetModel(Model);
            }
        }
    }
}