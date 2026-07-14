using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.Executors;
using AffiseAttributionLib.Internal.Predefined;
using AffiseAttributionLib.Utils;

namespace AffiseAttributionLib.Session
{
    public class SessionManagerImpl : ISessionManager
    {
        private const long TIME_TO_START_SESSION = 15 * 1000L;

        private SessionData _sessionData = new(
            lifetimeSessionCount: PrefUtils.GetLong(ProviderType.LIFETIME_SESSION_COUNT.Provider()),
            affiseSessionCount: PrefUtils.GetLong(ProviderType.AFFISE_SESSION_COUNT.Provider())
        );

        /**
         * Time of start session
         */
        private long? _openAppTime = null;

        /**
         * Last time of user active
         */
        private long? _closeAppTime = null;

        /**
         * Session active status
         */
        private bool _sessionActive = false;

        /**
         * Open app status
         */
        private bool _isOpenApp = false;

        private readonly ICurrentActiveActivityCountProvider _activityCountProvider;
        private readonly IExecutorServiceProvider _executorServiceProvider;

        public SessionManagerImpl(ICurrentActiveActivityCountProvider activityCountProvider)
        {
            _activityCountProvider = activityCountProvider;
            _executorServiceProvider = new ExecutorServiceProviderImpl();
        }

        /**
         * Start manager
         */
        public void Init()
        {
            SubscribeToActivityEvents();
        }

        /**
         * Subscribe to change open activity count
         */
        private void SubscribeToActivityEvents()
        {
            _activityCountProvider.AddActivityCountListener(count =>
            {
                //Check open activity count
                if (count > 0)
                {
                    SessionStart();
                }
                else
                {
                    //Update session status if need
                    CheckSessionToStart();

                    //Save time of user quit or hide app
                    _closeAppTime = GetTimeMillis();

                    //App is close
                    _isOpenApp = false;

                    //Drop session status
                    _sessionActive = false;

                    //Save sessionTime
                    SaveSessionTime();

                    //Drop open app time
                    _openAppTime = null;
                }
            });
        }

        public void SessionStart()
        {
            //App is open
            _isOpenApp = true;

            // Check create open app time
            if (_openAppTime is null)
            {
                //open app time
                _openAppTime = GetTimeMillis();
            }

            _executorServiceProvider.ExecuteWithDelay(TIME_TO_START_SESSION, () =>
            {
                if (SessionTime() == 0L) return;
                new SessionStartInternalEvent(
                    affiseSessionCount: GetSessionCount(),
                    lifetimeSessionCount: GetLifetimeSessionTime()
                ).Send();
            });
        }

        /**
         * Get session active status
         *
         * @return session status
         */
        public bool IsSessionActive()
        {
            //Check session status
            CheckSessionToStart();

            //Return session status
            return _sessionActive;
        }

        /**
         * Getting the last time the user was in the application
         * @return time
         */
        public long? GetLastInteractionTime()
        {
            //Current time if app is open
            if (_isOpenApp) return GetTimeMillis();

            //lastInteractionTime is session is active
            return _closeAppTime;
        }

        /**
         * Get current session time
         */
        public long GetSessionTime()
        {
            if (_openAppTime is null) return 0;

            return GetTimeMillis() - (_openAppTime ?? 0);
        }

        /**
         * Get all sessions time
         */
        public long GetLifetimeSessionTime()
        {
            return GetSaveSessionsTime() + GetSessionTime();
        }

        /**
         * Get session cont
         */
        public long GetSessionCount()
        {
            //Check session status
            if (!_sessionActive)
            {
                CheckSessionToStart();
            }

            return _sessionData.AffiseSessionCount;
        }

        /**
         * Get session start
         */
        public long? GetSessionStartTime()
        {
            return _openAppTime;
        }

        /**
         * Check time of start app and start session
         */
        private void CheckSessionToStart()
        {
            if (_sessionActive) return;

            if (SessionTime() <= 0) return;

            //if session started
            _sessionActive = true;

            //Save new session
            AddNewSession();
        }

        private long SessionTime()
        {
            if (_sessionActive) return 0L;

            //Check open app time
            if (_openAppTime is null) return 0L;

            //Time current session
            var time = GetTimeMillis() - _openAppTime - TIME_TO_START_SESSION ?? 0L;

            if (time <= 0) return 0L;

            //if session started
            return time;
        }

        /**
         * Save session time
         */
        private void SaveSessionTime()
        {
            var lifetimeSessionTime = GetLifetimeSessionTime();
            _sessionData = _sessionData.Copy(lifetimeSessionCount: lifetimeSessionTime);
            PrefUtils.SaveLong(ProviderType.LIFETIME_SESSION_COUNT.Provider(), lifetimeSessionTime);
        }

        /**
         * Get all old sessions time
         */
        private long GetSaveSessionsTime() => _sessionData.LifetimeSessionCount;

        /**
         * Save new session count
         */
        private void AddNewSession()
        {
            var count = _sessionData.AffiseSessionCount + 1;
            _sessionData = _sessionData.Copy(affiseSessionCount: count);
            PrefUtils.SaveLong(ProviderType.AFFISE_SESSION_COUNT.Provider(), count);
        }

        private long GetTimeMillis()
        {
            return Timestamp.New();
        }
    }
}
