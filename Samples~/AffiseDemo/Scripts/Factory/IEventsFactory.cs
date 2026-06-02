#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Events;

namespace AffiseAppDemo
{
    public interface IEventsFactory
    {
        public List<AffiseEvent> CreateEvents();
    }
}
