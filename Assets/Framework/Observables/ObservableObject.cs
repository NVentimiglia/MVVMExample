namespace Framework.Observables
{
    public class ObservableObject : IPropertyChanged
    {
        public event PropertyChanged OnPropertyChanged = delegate { };

        public virtual void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}