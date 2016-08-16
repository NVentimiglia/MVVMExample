using UnityEngine;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/VisibilityBinder")]
    public class VisibilityBinder : BinderBase
    {
        protected override void OnPropertyChanged()
        {
            gameObject.SetActive(ModelContext.Get<bool>(MemberName));
        }
    }
}