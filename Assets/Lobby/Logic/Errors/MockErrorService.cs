using System;
using UnityEngine;

namespace Lobby.Logic
{
    /// <summary>
    /// Error mgmt
    /// </summary>
    public class MockErrorService : IErrorService
    {
        /// <summary>
        /// Incoming notice
        /// </summary>
        public event Action<Exception, Action> OnError = delegate { };
   
        /// <summary>
        /// posts an error
        /// </summary>
        /// <param name="ex">the error</param>
        /// <param name="callback">When the user dismisses the error</param>
        public void Post(Exception ex, Action callback = null)
        {
            Debug.LogException(ex);
            OnError(ex, callback);
        }
    }
}
