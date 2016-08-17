using System;
using UnityEngine;

namespace Lobby.Logic
{
    public class MockAccountService : IAccountService
    {
        public AccountModel Account { get; protected set; }

        public event Action<AccountModel> OnChange = delegate { };

        public MockAccountService()
        {
            //Force New 
            Delete();

            // Load from store (redundent due to delete)
            Load();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("AccountController"))
            {
                Account = JsonUtility.FromJson<AccountModel>(PlayerPrefs.GetString("AccountController"));
                RaiseChange();
            }
            else
            {
                //New 
               Delete();
            }
        }

        /// <summary>
        ///  Save to file
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString("AccountController", JsonUtility.ToJson(Account));
            PlayerPrefs.Save();
            RaiseChange();
        }

        /// <summary>
        /// Resets account to a new instance
        /// </summary>
        public void Delete()
        {
            Account = new AccountModel
            {
                IsAuthenticated = false,
                UserName = string.Empty,
                Id = Guid.NewGuid().ToString()
            };


            PlayerPrefs.DeleteKey("AccountController");
            PlayerPrefs.Save();
            RaiseChange();
        }
        
        /// <summary>
        /// Notify listeners of a change
        /// </summary>
        public void RaiseChange()
        {
            OnChange(Account);
        }
    }
}
