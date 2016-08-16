using System.Collections;
using System.Collections.Generic;
using Framework.Observables;
using UnityEngine;

namespace Lobby.Views
{
    /// <summary>
    /// Extends View with automatic support for lists. 
    /// Data Driven Binding, so logic can determine add / remove, and views can focus on animation and presentation
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TView"></typeparam>
    [AddComponentMenu("Lobby/Internal/ViewBase")]
    public abstract class ListViewBase<TModel, TView> : ViewBase where TView : MonoBehaviour
    {
        [HideInInspector]
        public Dictionary<TModel, TView> Items = new Dictionary<TModel, TView>();

        [HideInInspector]
        public Dictionary<TView, TModel> Views = new Dictionary<TView, TModel>();

        public GameObject ItemPrefab;
        public Transform ItemContainer;

        public virtual void Awake()
        {
            ItemPrefab.SetActive(false);
        }

        /// <summary>
        /// adds a single item
        /// </summary>
        /// <param name="model"></param>
        public virtual void Add(TModel model)
        {
            Remove(model);

            var inst = Instantiate(ItemPrefab);
            var view = inst.GetComponent<TView>();

            Bind(view, model);
            
            Items.Add(model, view);
            Views.Add(view, model);

            inst.transform.SetParent(ItemContainer); 
            inst.SetActive(true);
        }
        
        /// <summary>
        /// removes a single item
        /// </summary>
        /// <param name="model"></param>
        public virtual void Remove(TModel model)
        {
            if (Items.ContainsKey(model))
            {
                var view = Items[model];
                UnBind(view, model);
                Items.Remove(model);
                Views.Remove(view);
                Destroy(view.gameObject);
            }
        }

        /// <summary>
        /// removes a single item
        /// </summary>
        public virtual void Remove(TView view)
        {
            if (Views.ContainsKey(view))
            {

                var model = Views[view];
                UnBind(view, model);
                Items.Remove(model);
                Views.Remove(view);
                Destroy(view.gameObject);
            }
        }

        /// <summary>
        /// adds a collection
        /// </summary>
        /// <param name="items"></param>
        public virtual void RemoveRange(IEnumerable<TModel> items)
        {
            StopCoroutine("RemoveRangeAsync");
            StartCoroutine(RemoveRangeAsync(items));
        }

        /// <summary>
        /// adds a single item
        /// </summary>
        public virtual IEnumerator RemoveRangeAsync(IEnumerable<TModel> items)
        {
            yield return 1;
            foreach (var item in items)
            {
                Remove(item);
                yield return 1;
            }
        }

        /// <summary>
        /// adds a collection
        /// </summary>
        /// <param name="items"></param>
        public virtual void AddRange(IEnumerable<TModel> items)
        {
            StopCoroutine("AddRangeAsync");
            StartCoroutine(AddRangeAsync(items));
        }

        /// <summary>
        /// adds a single item
        /// </summary>
        public virtual IEnumerator AddRangeAsync(IEnumerable<TModel> items)
        {
            yield return 1;
            foreach (var item in items)
            {
                Add(item);
                yield return 1;
            }
        }


        /// <summary>
        /// removes all children
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Items)
            {
                var view = item.Value;
                var model = item.Key;
                UnBind(view, model);
                Destroy(view.gameObject);
            }
            Items.Clear();
            Views.Clear();
        }

        /// <summary>
        /// Cleanup disposal logic here
        /// </summary>
        /// <param name="view"></param>
        /// <param name="model"></param>
        public virtual void UnBind(TView view, TModel model)
        {

        }

        /// <summary>
        /// Hydrate view here
        /// </summary>
        /// <param name="view"></param>
        /// <param name="model"></param>
        public abstract void Bind(TView view, TModel model);

        /// <summary>
        /// Binds to a event driven collection
        /// </summary>
        /// <param name="collection"></param>
        public virtual void Subscribe(ObservableCollection<TModel> collection)
        {
            collection.OnItem += OnItem;
            collection.OnItems += OnItems;
            collection.OnClear += OnClear;
            AddRange(collection);
        }


        /// <summary>
        /// unbinds from an event driven collection
        /// </summary>
        public virtual void UnSubscribe(ObservableCollection<TModel> collection)
        {
            collection.OnItem -= OnItem;
            collection.OnItems -= OnItems;
            collection.OnClear -= OnClear;
            Clear();
        }

        void OnItem(ObservableCollection<TModel>.CollectionEvent eventType, TModel item)
        {
            switch (eventType)
            {
                case ObservableCollection<TModel>.CollectionEvent.Add:
                    Add(item);
                    break;
                case ObservableCollection<TModel>.CollectionEvent.Remove:
                    Remove(item);
                    break;
                case ObservableCollection<TModel>.CollectionEvent.Replace:
                    Remove(item);
                    Add(item);
                    break;
            }
        }

        void OnItems(ObservableCollection<TModel>.CollectionEvent eventType, IEnumerable<TModel> item)
        {
            switch (eventType)
            {
                case ObservableCollection<TModel>.CollectionEvent.Add:
                    AddRange(item);
                    break;
                case ObservableCollection<TModel>.CollectionEvent.Remove:
                    RemoveRange(item);
                    break;
                case ObservableCollection<TModel>.CollectionEvent.Replace:
                    AddRange(item);
                    break;
            }
        }

        void OnClear()
        {
            Clear();
        }
    }
}
