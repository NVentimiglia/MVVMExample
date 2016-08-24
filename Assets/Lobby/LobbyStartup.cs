using System.Collections;
using Framework;
using Lobby.Logic;
using Lobby.Views;
using UnityEngine;

namespace Lobby
{

    /// <summary>
    /// Game Startup class
    /// </summary>
    [AddComponentMenu("Lobby/LobbyStartup")]
    public class LobbyStartup : MonoBehaviour
    {
        //Poor mans IOC
        public static LobbyStartup Instance { get; private set; }

        //Game Settings
        public string HostPath = "127.0.0.1";
        public int PortWS = 5000;
        public int PortTcp = 5001;
        public int PortUdp = 5002;

        //Infrastructure Dependencies (none, mock only)

        //Logic Dependencies
        public IErrorService Errors;
        public INoticeService Notices;
        public IAccountService Accounts;
        public IScoreService Scores;
        public IChatService Chats;

        //Views 
        public SignInView SignInView;
        public OptionView OptionView;
        public HelpView HelpView;
        public ScoreView ScoreView;
        public ChatView ChatView;
        public ViewBase GameView;

        public BusyView BusyView;
        public ConfirmView ConfirmView;
        public ErrorView ErrorView;
        public NoticeView NoticeView;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ConfigInfrastructure();
            ConfigLogic();
            ConfigViews();
        }

        void ConfigInfrastructure()
        {
            // None, mock only
            // would wire up http / sockets / repositories here
        }

        void ConfigLogic()
        {
            Notices = InjectController.RegisterSingleton(new MockNoticeService());
            Errors = InjectController.RegisterSingleton(new MockErrorService());
            Accounts = InjectController.RegisterSingleton(new MockAccountService());
            Scores = InjectController.RegisterSingleton(new MockScoreService());
            Chats = InjectController.RegisterSingleton(new MockChatService());
        }

        void ConfigViews()
        {
            // Views


            foreach (var view in GetComponentsInChildren<ViewBase>(true))
            {
                view.Hide(true);
                InjectController.RegisterSingleton(view, view.GetType());
                InjectController.Inject(view);
                view.OnAwake();
            }
        }


        IEnumerator Start()
        {
            //sign in ?.
            
            if (!Accounts.Account.IsAuthenticated)
            {
                yield return StartCoroutine(SignInView.ShowAndWait());
            }
            
            
            //Pass control to MainView
            Instance.GameView.Show();
        }


        /// <summary>
        /// Call to sign out and reset game
        /// </summary>
        public void Reset()
        {
            // Hide Views
            foreach (var view in GetComponentsInChildren<ViewBase>(true))
            {
                view.OnReset();
            }

            //Restart
            StartCoroutine(Start());
        }
    }
}
