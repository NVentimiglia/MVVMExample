using System.Collections.Generic;
using Framework.Observables;
using UnityEngine;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/ListBinder")]
    public class ListBinder : BinderBase
    {
        public GameObject Prefab;

        public GameObject Container;

        ObservableCollection<object> collection;

        protected override void OnPropertyChanged()
        {
            if (collection != null)
            {
                collection.OnClear -= OnClear;
                collection.OnItem -= OnItem;
                collection.OnItems -= OnItems;
            }

            if (Model == null)
                return;

            if (Model.GetType() == typeof(ObservableCollection<>))
            {
                collection = (ObservableCollection<object>)Model;

                collection.OnClear += OnClear;
                collection.OnItem += OnItem;
                collection.OnItems += OnItems;
            }
        }

        private void OnItems(ObservableCollection<object>.CollectionEvent arg1, IEnumerable<object> arg2)
        {

        }

        private void OnItem(ObservableCollection<object>.CollectionEvent arg1, object arg2)
        {

        }

        private void OnClear()
        {

        }
    }
}