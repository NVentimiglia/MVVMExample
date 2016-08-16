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

            // wire auto save
            OnChange += model =>
            {
                Save();
            };
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey("AccountController"))
            {
                Account = JsonUtility.FromJson<AccountModel>(PlayerPrefs.GetString("AccountController"));
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
        }
    }
}
