using UnityEngine;
using UnityEngine.UI;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/ButtonBinder")]
    public class ButtonBinder : BinderBase
    {
        Button _control;
        protected Button Control
        {
            get
            {
                if (_control == null)
                    _control = GetComponent<Button>();
                return _control;
            }
        }

        void Awake()
        {
            Control.onClick.AddListener(Call);
        }

        private void Call()
        {
            if (ModelContext == null)
                return;

            ModelContext.Invoke(MemberName);
        }
    }
}