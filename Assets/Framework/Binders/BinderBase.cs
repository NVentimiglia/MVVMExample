using System;
using Framework.Observables;
using UnityEngine;

namespace Framework.Binders
{
    [AddComponentMenu("Framework/Binders/BinderBase")]
    public class BinderBase : MonoBehaviour
    {
        #region Unity

        /// <summary>
        /// Name of the property to observe
        /// </summary>
        public string MemberName;

        #endregion

        //viewmodel
        public object Model { get; set; }
        //Wrapped with reflection
        public ModelReflector ModelContext { get; set; }

        /// <summary>
        /// Init method
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(object model)
        {
            //1) cleanup
            Model = null;
            if (ModelContext != null)
            {
                ModelContext.OnPropertyChanged -= OnPropertyChanged;
                ModelContext.Dispose();
                ModelContext = null;
            }

            OnModelRemoved();

            //cleanup
            OnPropertyChanged();

            if (model == null)
                return;

            //2) set
            Model = model;
            ModelContext = new ModelReflector(Model);
            ModelContext.OnPropertyChanged += OnPropertyChanged;

            OnModelSet();

            //3) init
            OnPropertyChanged();
        }

        protected virtual void OnModelRemoved()
        {
            // TODO handle cleanup
        }

        protected virtual void OnModelSet()
        {
            // TODO handle init
        }


        protected virtual void OnPropertyChanged(string memberName)
        {
            if (MemberName == memberName)
            {
                OnPropertyChanged();
            }
        }


        protected virtual void OnPropertyChanged()
        {
            // TODO handle change
        }
    }
}