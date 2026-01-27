#nullable enable
using System;
using AffiseAttributionLib.Session;
using AffiseAttributionLib.Utils;

namespace AffiseAttributionLib.Usecase
{
    internal class FirstAppOpenUseCase
    {
        private readonly ICurrentActiveActivityCountProvider _activityCountProvider;
        private bool _firstRun = false;
        private bool _isFirstOpenValue = PrefUtils.GetBoolean(FIRST_OPENED, true);

        public FirstAppOpenUseCase(ICurrentActiveActivityCountProvider activityCountProvider)
        {
            _activityCountProvider = activityCountProvider;
        }

        private const string FIRST_OPENED = "FIRST_OPENED";
        internal const string FIRST_OPENED_DATE_KEY = "FIRST_OPENED_DATE_KEY";

        /**
         * Check preferences for have first opened date and generate properties if no data
         */
        public void OnAppCreated()
        {
            if (PrefUtils.GetLong(FIRST_OPENED_DATE_KEY) == 0L)
            {
                OnAppFirstOpen();
            }
            
            _firstRun = PrefUtils.GetBoolean(FIRST_OPENED, true);

            //init session observer
            _activityCountProvider.Init();
        }
        
        /**
         * Generate properties on app first open
         */
        private void OnAppFirstOpen()
        {
            //Create first open date
            var firstOpenDate = Timestamp.New();
            
            //Save properties
            PrefUtils.SaveLong(FIRST_OPENED_DATE_KEY, firstOpenDate);
            PrefUtils.SaveBoolean(FIRST_OPENED, true);
        }
        

        /**
         * Get first open
         * @return is first open
         */
        public bool IsFirstOpen()
        {
            return _isFirstOpenValue;
        }
        
        /**
         * First open completed
         */
        public void CompleteFirstOpen()
        {
            _isFirstOpenValue = false;
            PrefUtils.SaveBoolean(FIRST_OPENED, _isFirstOpenValue);
        }

        public bool IsFirstRun()
        {
            return _firstRun;
        }

        /**
         * Get first open date
         * @return first open date
         */
        public DateTime? GetFirstOpenDate()
        {
            var time = PrefUtils.GetLong(FIRST_OPENED_DATE_KEY, 0);
            if (time == 0L) return null;
            
            return DateTimeOffset.FromUnixTimeMilliseconds(time).UtcDateTime;
        }
    }
}