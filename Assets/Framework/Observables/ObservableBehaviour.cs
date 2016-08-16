using UnityEngine;

namespace Framework.Observables
{
    public class ObservableBehaviour : MonoBehaviour, IPropertyChanged
    {
        public event PropertyChanged OnPropertyChanged = delegate { };
        
        public virtual void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}