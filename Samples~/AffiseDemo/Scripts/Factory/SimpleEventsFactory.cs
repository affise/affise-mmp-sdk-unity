#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Events.Predefined;
using AffiseAttributionLib.Events.Subscription;
using SimpleJSON;

namespace AffiseAppDemo
{
    public class SimpleEventsFactory : IEventsFactory
    {
        public List<AffiseEvent> CreateEvents()
        {
            return new List<AffiseEvent>
            {
                new AchieveLevelEvent(),
                new AddPaymentInfoEvent(),
                new AddToCartEvent(),
                new AddToWishlistEvent(),
                new AdRevenueEvent(),
                new ClickAdvEvent(),
                new CompleteRegistrationEvent(),
                new CompleteStreamEvent(),
                new CompleteTrialEvent(),
                new CompleteTutorialEvent(),
                new ContactEvent(),
                new ContentItemsViewEvent(),
                new CustomId01Event(),
                new CustomId02Event(),
                new CustomId03Event(),
                new CustomId04Event(),
                new CustomId05Event(),
                new CustomId06Event(),
                new CustomId07Event(),
                new CustomId08Event(),
                new CustomId09Event(),
                new CustomId10Event(),
                new CustomizeProductEvent(),
                new DeepLinkedEvent(),
                new DonateEvent(),
                new FindLocationEvent(),
                new InitiateCheckoutEvent(),
                new InitiatePurchaseEvent(),
                new InitiateStreamEvent(),
                new InviteEvent(),
                new LastAttributedTouchEvent(),
                new LeadEvent(),
                new ListViewEvent(),
                new LoginEvent(),
                new OpenedFromPushNotificationEvent(),
                new OrderEvent(),
                new OrderItemAddedEvent(),
                new OrderItemRemoveEvent(),
                new OrderCancelEvent(),
                new OrderReturnRequestEvent(),
                new OrderReturnRequestCancelEvent(),
                new PurchaseEvent(),
                new RateEvent(),
                new ReEngageEvent(),
                new ReserveEvent(),
                new ScheduleEvent(),
                new SalesEvent(),
                new SearchEvent(),
                new ShareEvent(),
                new SpendCreditsEvent(),
                new StartRegistrationEvent(),
                new StartTrialEvent(),
                new StartTutorialEvent(),
                new SubmitApplicationEvent(),
                new SubscribeEvent(),
                new TravelBookingEvent(),
                new UnlockAchievementEvent(),
                new UnsubscribeEvent(),
                new UpdateEvent(),
                new ViewAdvEvent(),
                new ViewContentEvent(),
                new ViewCartEvent(),
                new ViewItemEvent(),
                new ViewItemsEvent(),

                new InitialSubscriptionEvent(new JSONObject { }),
                new InitialTrialEvent(new JSONObject { }),
                new InitialOfferEvent(new JSONObject { }),
                new FailedTrialEvent(new JSONObject { }),
                new FailedOfferiseEvent(new JSONObject { }),
                new FailedSubscriptionEvent(new JSONObject { }),
                new FailedTrialFromRetryEvent(new JSONObject { }),
                new FailedOfferFromRetryEvent(new JSONObject { }),
                new FailedSubscriptionFromRetryEvent(new JSONObject { }),
                new TrialInRetryEvent(new JSONObject { }),
                new OfferInRetryEvent(new JSONObject { }),
                new SubscriptionInRetryEvent(new JSONObject { }),
                new ConvertedTrialEvent(new JSONObject { }),
                new ConvertedOfferEvent(new JSONObject { }),
                new ReactivatedSubscriptionEvent(new JSONObject { }),
                new RenewedSubscriptionEvent(new JSONObject { }),
                new ConvertedTrialFromRetryEvent(new JSONObject { }),
                new ConvertedOfferFromRetryEvent(new JSONObject { }),
                new RenewedSubscriptionFromRetryEvent(new JSONObject { }),
                new UnsubscriptionEvent(new JSONObject { }),
            };
        }
    }
}
