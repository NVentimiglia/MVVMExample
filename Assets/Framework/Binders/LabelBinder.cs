using UnityEngine;
using UnityEngine.UI;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/LabelBinder")]
    public class LabelBinder : BinderBase
    {
        Text _control;
        protected Text Control
        {
            get
            {
                if (_control == null)
                    _control = GetComponent<Text>();
                return _control;
            }
        }
        
        
        protected override void OnPropertyChanged()
        {
            if (Model == null)
                return;

            Control.text = ModelContext.Get<string>(MemberName);
        }
    }
}