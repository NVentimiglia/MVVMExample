using System;
using System.Collections;
using Framework;
using Lobby.Logic;
using UnityEngine;

namespace Lobby.Views
{
    [AddComponentMenu("Lobby/Internal/ViewBase")]
    public class ViewBase : MonoBehaviour
    {
        #region  Dependencies

        [Inject]
        protected BusyView BusyView;
        [Inject]
        protected IErrorService ErrorrService;
        [Inject]
        protected INoticeService NoticeService;

        protected bool isRegistered;

        #endregion

        #region LifeCycle

        /// <summary>
        /// Application level awake
        /// </summary>
        public virtual void OnAwake()
        {
            if (!isRegistered)
            {
                InjectController.RegisterSingleton(this);
                isRegistered = true;
            }

            InjectController.Inject(this);
        }

        /// <summary>
        /// Application level reset
        /// </summary>
        public virtual void OnReset()
        {
            Hide(true);
        }

        #endregion

        #region Show / Hide

        public GameObject ContentRoot;


        /// <summary>
        /// Can be seen
        /// </summary>
        public bool IsVisible { get; protected set; }

        public void Show()
        {
            if (IsVisible)
                return;

            if (ContentRoot)
                ContentRoot.SetActive(true);

            IsVisible = true;
            OnShow();
        }

        public void Hide(bool force = false)
        {
            if (!IsVisible && !force)
                return;

            if (ContentRoot)
                ContentRoot.SetActive(false);

            IsVisible = false;
            OnHide();
        }

        /// <summary>
        /// Open and wait for close
        /// </summary>
        /// <returns></returns>
        public IEnumerator ShowAndWait()
        {
            Show();

            while (IsVisible)
            {
                yield return 1;
            }
        }
        /// <summary>
        /// On Show override
        /// </summary>
        protected virtual void OnShow()
        {

        }

        /// <summary>
        /// OnHide override
        /// </summary>
        protected virtual void OnHide()
        {

        }
        #endregion

     
        #region Helper Methods

        // Dialog Helpers

        public void ShowBusy(string message)
        {
            BusyView.Message = message;
            BusyView.Show();
        }

        public void HideBusy()
        {
            BusyView.Hide();
        }

        public void ShowError(string message)
        {
            ErrorrService.Post(new Exception(message));
        }

        public void ShowError(string message, Action callback)
        {
            ErrorrService.Post(new Exception(message), callback);
        }

        // Notice Helpers

        public void Notice(string message)
        {
            NoticeService.PostLocal(message);
        }

        public void NoticeWarning(string message)
        {
            NoticeService.PostLocal(message, NoticeColor.Warning);
        }

        public void NoticeError(string message)
        {
            NoticeService.PostLocal(message, NoticeColor.Error);
        }

        public void NoticeSuccess(string message)
        {
            NoticeService.PostLocal(message, NoticeColor.Success);
        }

        #endregion

    }
}
