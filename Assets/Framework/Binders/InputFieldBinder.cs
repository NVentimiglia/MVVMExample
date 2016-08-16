using UnityEngine;
using UnityEngine.UI;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/TextBinder")]
    public class InputFieldBinder : BinderBase
    {
        InputField _control;
        protected InputField Control
        {
            get
            {
                if (_control == null)
                    _control = GetComponent<InputField>();
                return _control;
            }
        }

        protected void Awake()
        {
            Control.onValueChanged.AddListener(Call);
        }

        private void Call(string arg0)
        {
            if (ModelContext == null)
                return;

            ModelContext.Set(MemberName, arg0);
        }

        protected override void OnPropertyChanged()
        {
            if (ModelContext == null)
                return;

            Control.text = ModelContext.Get<string>(MemberName);
        }
    }
}