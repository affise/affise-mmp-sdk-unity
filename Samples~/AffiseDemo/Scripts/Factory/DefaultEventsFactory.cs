#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using AffiseAttributionLib.Ad;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Events.Parameters;
using AffiseAttributionLib.Events.Predefined;
using AffiseAttributionLib.Events.Subscription;
using AffiseAttributionLib.Utils;
using SimpleJSON;
using TouchType = AffiseAttributionLib.Events.Predefined.TouchType;

namespace AffiseAppDemo
{
    public class DefaultEventsFactory : IEventsFactory
    {
        public List<AffiseEvent> CreateEvents()
        {
            return new List<AffiseEvent>()
            {
                CreateAchieveLevelEvent(),
                CreateAddPaymentInfoEvent(),
                CreateAddToCartEvent(),
                CreateAddToWishlistEvent(),
                CreateAdRevenueEvent(),
                CreateClickAdvEvent(),
                CreateCompleteRegistrationEvent(),
                CreateCompleteStreamEvent(),
                CreateCompleteTrialEvent(),
                CreateCompleteTutorialEvent(),
                CreateContactEvent(),
                CreateContentItemsViewEvent(),
                CreateCustomId01Event(),
                CreateCustomId02Event(),
                CreateCustomId03Event(),
                CreateCustomId04Event(),
                CreateCustomId05Event(),
                CreateCustomId06Event(),
                CreateCustomId07Event(),
                CreateCustomId08Event(),
                CreateCustomId09Event(),
                CreateCustomId10Event(),
                CreateCustomizeProductEvent(),
                CreateDeepLinkedEvent(),
                CreateDonateEvent(),
                CreateFindLocationEvent(),
                CreateInitiateCheckoutEvent(),
                CreateInitiatePurchaseEvent(),
                CreateInitiateStreamEvent(),
                CreateInviteEvent(),
                CreateLastAttributedTouchEvent(),
                CreateLeadEvent(),
                CreateListViewEvent(),
                CreateLoginEvent(),
                CreateOpenedFromPushNotificationEvent(),
                CreateOrderEvent(),
                CreateOrderItemAddedEvent(),
                CreateOrderItemRemoveEvent(),
                CreateOrderCancelEvent(),
                CreateOrderReturnRequestEvent(),
                CreateOrderReturnRequestCancelEvent(),
                CreatePurchaseEvent(),
                CreateRateEvent(),
                CreateReEngageEvent(),
                CreateReserveEvent(),
                CreateScheduleEvent(),
                CreateSalesEvent(),
                CreateSearchEvent(),
                CreateShareEvent(),
                CreateSpendCreditsEvent(),
                CreateStartRegistrationEvent(),
                CreateStartTrialEvent(),
                CreateStartTutorialEvent(),
                CreateSubmitApplicationEvent(),
                CreateSubscribeEvent(),
                CreateTravelBookingEvent(),
                CreateUnlockAchievementEvent(),
                CreateUnsubscribeEvent(),
                CreateUpdateEvent(),
                CreateViewAdvEvent(),
                CreateViewContentEvent(),
                CreateViewCartEvent(),
                CreateViewItemEvent(),
                CreateViewItemsEvent(),

                CreateInitialSubscriptionEvent(),
                CreateInitialTrialEventEvent(),
                CreateInitialOfferEvent(),
                CreateFailedTrialEvent(),
                CreateFailedOfferiseEvent(),
                CreateFailedSubscriptionEvent(),
                CreateFailedTrialFromRetryEvent(),
                CreateFailedOfferFromRetryEvent(),
                CreateFailedSubscriptionFromRetryEvent(),
                CreateTrialInRetryEvent(),
                CreateOfferInRetryEvent(),
                CreateSubscriptionInRetryEvent(),
                CreateConvertedTrialEvent(),
                CreateConvertedOfferEvent(),
                CreateReactivatedSubscriptionEvent(),
                CreateRenewedSubscriptionEvent(),
                CreateConvertedTrialFromRetryEvent(),
                CreateConvertedOfferFromRetryEvent(),
                CreateRenewedSubscriptionFromRetryEvent(),
                CreateUnsubscriptionEvent(),
            };
        }

        private AffiseEvent CreateAchieveLevelEvent()
        {
            var level = new JSONObject
            {
                ["old_level"] = 69,
                ["new_level"] = 70
            };
            var affiseEvent = new AchieveLevelEvent(
                userData: "warlock"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.DEEP_LINK, "https://new-game.lt")
                .AddPredefinedParameter(PredefinedLong.SCORE, 25013L)
                .AddPredefinedParameter(PredefinedLong.LEVEL, 70L)
                .AddPredefinedParameter(PredefinedString.SUCCESS, "true")
                .AddPredefinedParameter(PredefinedString.TUTORIAL_ID, "12")
                .AddPredefinedParameter(PredefinedObject.CONTENT, level);

            return affiseEvent;
        }

        private AffiseEvent CreateAddPaymentInfoEvent()
        {
            var paymentInfo = new JSONObject
            {
                ["card"] = 4138,
                ["type"] = "phone"
            };

            var affiseEvent = new AddPaymentInfoEvent(
                userData: "taxi"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PURCHASE_CURRENCY, "USD")
                .AddPredefinedParameter(PredefinedObject.CONTENT, paymentInfo);

            return affiseEvent;
        }

        private AffiseEvent CreateAddToCartEvent()
        {
            var addToCartObject = new JSONObject
            {
                ["items"] = "milk",
            };

            var affiseEvent = new AddToCartEvent(
                userData: "cart"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.DESCRIPTION, "best before 2029")
                .AddPredefinedParameter(PredefinedObject.CONTENT, addToCartObject);

            return affiseEvent;
        }

        private AffiseEvent CreateAddToWishlistEvent()
        {
            var wishList = new JSONObject
            {
                ["items"] = "car",
            };

            var affiseEvent = new AddToWishlistEvent(
                userData: "next year"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.COUNTRY, "Russia")
                .AddPredefinedParameter(PredefinedString.CITY, "Voronezh")
                .AddPredefinedParameter(PredefinedFloat.LAT, 42)
                .AddPredefinedParameter(PredefinedFloat.LONG, 24)
                .AddPredefinedParameter(PredefinedObject.CONTENT, wishList);

            return affiseEvent;
        }

        private AffiseEvent CreateAdRevenueEvent()
        {
            var affiseEvent = new AdRevenueEvent(
                userData: "next year"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.SOURCE, AffiseAdSource.ADMOB.Type())
                .AddPredefinedParameter(PredefinedFloat.REVENUE, 42.0f)
                .AddPredefinedParameter(PredefinedString.CURRENCY, "USD")
                .AddPredefinedParameter(PredefinedString.NETWORK, "test")
                .AddPredefinedParameter(PredefinedString.UNIT, "m")
                .AddPredefinedParameter(PredefinedString.PLACEMENT, "end");

            return affiseEvent;
        }

        private AffiseEvent CreateClickAdvEvent()
        {
            var affiseEvent = new ClickAdvEvent(
                timeStampMillis: Timestamp.New(),
                userData: "header"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "PARAM_01")
                .AddPredefinedParameter(PredefinedString.PARAM_02, "PARAM_02")
                .AddPredefinedParameter(PredefinedString.PARAM_03, "PARAM_03")
                .AddPredefinedParameter(PredefinedString.PARAM_04, "PARAM_04")
                .AddPredefinedParameter(PredefinedString.PARAM_05, "PARAM_05")
                .AddPredefinedParameter(PredefinedString.PARAM_06, "PARAM_06")
                .AddPredefinedParameter(PredefinedString.PARAM_07, "PARAM_07")
                .AddPredefinedParameter(PredefinedString.PARAM_08, "PARAM_08")
                .AddPredefinedParameter(PredefinedString.PARAM_09, "PARAM_09")
                .AddPredefinedParameter(PredefinedString.PARAM_10, "PARAM_10");

            return affiseEvent;
        }

        private AffiseEvent CreateCompleteRegistrationEvent()
        {
            var registration = new JSONObject
            {
                ["email"] = "dog@gmail.com",
            };

            var affiseEvent = new CompleteRegistrationEvent(
                userData: "promo"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.VALIDATED, "02.11.2021")
                .AddPredefinedParameter(PredefinedString.REVIEW_TEXT, "approve")
                .AddPredefinedParameter(PredefinedString.CUSTOMER_SEGMENT, "DOG")
                .AddPredefinedParameter(PredefinedObject.CONTENT, registration);

            return affiseEvent;
        }

        private AffiseEvent CreateCompleteStreamEvent()
        {
            var stream = new JSONObject
            {
                ["streamer"] = "cat",
                ["max_viewers"] = "29",
            };

            var affiseEvent = new CompleteStreamEvent(
                userData: "23 hours"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f)
                .AddPredefinedParameter(PredefinedObject.CONTENT, stream);

            return affiseEvent;
        }

        private AffiseEvent CreateCompleteTrialEvent()
        {
            var trial = new JSONObject
            {
                ["player"] = "ghost",
            };

            var affiseEvent = new CompleteTrialEvent(
                userData: "time"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.REGISTRATION_METHOD, "SMS")
                .AddPredefinedParameter(PredefinedObject.CONTENT, trial);

            return affiseEvent;
        }

        private AffiseEvent CreateCompleteTutorialEvent()
        {
            var tutorial = new JSONObject
            {
                ["name"] = "intro",
            };

            var affiseEvent = new CompleteTutorialEvent(
                userData: "intro"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.REGISTRATION_METHOD, "SMS")
                .AddPredefinedParameter(PredefinedObject.CONTENT, tutorial);

            return affiseEvent;
        }

        private AffiseEvent CreateContactEvent()
        {
            var affiseEvent = new ContactEvent(
                userData: "contact"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.REGISTRATION_METHOD, "SMS");

            return affiseEvent;
        }

        private AffiseEvent CreateContentItemsViewEvent()
        {
            var affiseEvent = new ContentItemsViewEvent(
                userData: "intro"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedObject.CONTENT, new JSONObject
                {
                    ["collection"] = "Greatest Hits"
                })
                .AddPredefinedParameter(PredefinedString.CONTENT_ID, "2561")
                .AddPredefinedParameter(PredefinedListObject.CONTENT_LIST, new List<JSONObject>
                {
                    new()
                    {
                        ["content"] = "songs",
                    },
                    new()
                    {
                        ["type"] = "videos",
                    },
                })
                .AddPredefinedParameter(PredefinedListString.CONTENT_IDS, new List<string>
                {
                    "id_1", "id_2"
                })
                .AddPredefinedParameter(PredefinedString.CONTENT_TYPE, "MATURE")
                .AddPredefinedParameter(PredefinedString.CURRENCY, "USD")
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 6L)
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f);

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId01Event()
        {
            var affiseEvent = new CustomId01Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId02Event()
        {
            var affiseEvent = new CustomId02Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId03Event()
        {
            var affiseEvent = new CustomId03Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId04Event()
        {
            var affiseEvent = new CustomId04Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId05Event()
        {
            var affiseEvent = new CustomId05Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId06Event()
        {
            var affiseEvent = new CustomId06Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId07Event()
        {
            var affiseEvent = new CustomId07Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId08Event()
        {
            var affiseEvent = new CustomId08Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId09Event()
        {
            var affiseEvent = new CustomId09Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }

        private AffiseEvent CreateCustomId10Event()
        {
            var affiseEvent = new CustomId10Event(
                userData: "custom"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1 Hits");

            return affiseEvent;
        }


        private AffiseEvent CreateCustomizeProductEvent()
        {
            var affiseEvent = new CustomizeProductEvent(
                userData: "Customize"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1");

            return affiseEvent;
        }

        private AffiseEvent CreateDeepLinkedEvent()
        {
            var affiseEvent = new DeepLinkedEvent(
                userData: "referrer: google"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ADREV_AD_TYPE, "interstitial")
                .AddPredefinedParameter(PredefinedString.REGION, "ASIA")
                .AddPredefinedParameter(PredefinedString.CLASS, "student")
                .AddPredefinedParameter(PredefinedObject.CONTENT, new JSONObject
                {
                    ["isLinked"] = true
                });

            return affiseEvent;
        }

        private AffiseEvent CreateDonateEvent()
        {
            var affiseEvent = new DonateEvent(
                userData: "Donate"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateFindLocationEvent()
        {
            var affiseEvent = new FindLocationEvent(
                userData: "location"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateInitiateCheckoutEvent()
        {
            var affiseEvent = new InitiateCheckoutEvent(
                userData: "checkout"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateInitiatePurchaseEvent()
        {
            var purchaseData = new JSONObject
            {
                ["payment"] = "card",
            };

            var affiseEvent = new InitiatePurchaseEvent(
                userData: "best price"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L)
                .AddPredefinedParameter(PredefinedObject.CONTENT, purchaseData);

            return affiseEvent;
        }

        private AffiseEvent CreateInitiateStreamEvent()
        {
            var stream = new JSONObject
            {
                ["streamer"] = "car",
                ["date"] = "02.03.2021",
            };
            var affiseEvent = new InitiateStreamEvent(
                userData: "shooter"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.COUPON_CODE, "25XLKM")
                .AddPredefinedParameter(PredefinedString.VIRTUAL_CURRENCY_NAME, "BTC")
                .AddPredefinedParameter(PredefinedObject.CONTENT, stream);

            return affiseEvent;
        }

        private AffiseEvent CreateInviteEvent()
        {
            var invite = new JSONObject
            {
                ["invitation"] = "mail",
                ["date"] = "02.03.2021",
            };
            var affiseEvent = new InviteEvent(
                userData: "dancing"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, invite);

            return affiseEvent;
        }

        private AffiseEvent CreateLastAttributedTouchEvent()
        {
            var touchData = new JSONObject
            {
                ["header"] = 2,
                ["touchType"] = TouchType.WEB_TO_APP_AUTO_REDIRECT.ToValue(),
            };

            var affiseEvent = new LastAttributedTouchEvent(
                userData: "tablet"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.SUBSCRIPTION_ID, "lasAK22")
                .AddPredefinedParameter(PredefinedObject.CONTENT, touchData);

            return affiseEvent;
        }

        private AffiseEvent CreateLeadEvent()
        {
            var affiseEvent = new LeadEvent(
                userData: "lead"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PAYMENT_INFO_AVAILABLE, "card")
                .AddPredefinedParameter(PredefinedString.SEARCH_STRING, "best car wash");

            return affiseEvent;
        }

        private AffiseEvent CreateListViewEvent()
        {
            var list = new JSONObject
            {
                ["clothes"] = 2,
            };

            var affiseEvent = new ListViewEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PAYMENT_INFO_AVAILABLE, "card")
                .AddPredefinedParameter(PredefinedString.SEARCH_STRING, "best car wash")
                .AddPredefinedParameter(PredefinedString.SUGGESTED_DESTINATIONS, "crete, spain")
                .AddPredefinedParameter(PredefinedString.SUGGESTED_HOTELS, "beach resort, marina spa")
                .AddPredefinedParameter(PredefinedObject.CONTENT, list);

            return affiseEvent;
        }

        private AffiseEvent CreateLoginEvent()
        {
            var login = new JSONObject
            {
                ["email"] = "cat@gmail.com"
            };

            var affiseEvent = new LoginEvent(
                userData: "web"
            );

            affiseEvent.AddPredefinedParameter(PredefinedObject.CONTENT, login);

            return affiseEvent;
        }

        private AffiseEvent CreateOpenedFromPushNotificationEvent()
        {
            var affiseEvent = new OpenedFromPushNotificationEvent(
                userData: "active"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, new JSONObject
                {
                    ["details"] = "silent"
                });

            return affiseEvent;
        }

        private AffiseEvent CreateOrderEvent()
        {
            var affiseEvent = new OrderEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            // PredefinedGroup example
            // affiseEvent.AddPredefinedListGroup(new List<PredefinedGroup>
            // {
            //     new PredefinedGroup()
            //         .AddPredefinedParameter(PredefinedString.CUSTOMER_USER_ID, "KDCJHB10834rJHG")
            //         .AddPredefinedParameter(PredefinedString.CONTENT_ID, "334923062984")
            //         .AddPredefinedParameter(PredefinedString.DESCRIPTION,
            //             "SevenFriday Men's M1B-01 Urban Explorer 47.6 Automatic Watch")
            //         .AddPredefinedParameter(PredefinedLong.QUANTITY, 3)
            //         .AddPredefinedParameter(PredefinedFloat.PRICE, 499f)
            //         .AddPredefinedParameter(PredefinedString.CURRENCY, "USD")
            //         .AddPredefinedParameter(PredefinedFloat.REVENUE, 1497f)
            //         .AddPredefinedParameter(PredefinedString.ORDER_ID, "ID_34923"),
            //
            //     new PredefinedGroup()
            //         .AddPredefinedParameter(PredefinedString.CUSTOMER_USER_ID, "KDCJHB10834rJHG")
            //         .AddPredefinedParameter(PredefinedString.CONTENT_ID, "383791923777")
            //         .AddPredefinedParameter(PredefinedString.DESCRIPTION, "2021 Apple iPad 9th Gen 64/256GB WiFi 10.2")
            //         .AddPredefinedParameter(PredefinedLong.QUANTITY, 1)
            //         .AddPredefinedParameter(PredefinedFloat.PRICE, 299f)
            //         .AddPredefinedParameter(PredefinedString.CURRENCY, "USD")
            //         .AddPredefinedParameter(PredefinedFloat.REVENUE, 299f)
            //         .AddPredefinedParameter(PredefinedString.ORDER_ID, "ID_83792")
            // });

            return affiseEvent;
        }

        private AffiseEvent CreateOrderItemAddedEvent()
        {
            var affiseEvent = new OrderItemAddedEvent(
                userData: "apple"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }
        
        private AffiseEvent CreateOrderItemRemoveEvent()
        {
            var affiseEvent = new OrderItemRemoveEvent(
                userData: "apple"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }
        

        private AffiseEvent CreateOrderCancelEvent()
        {
            var affiseEvent = new OrderCancelEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateOrderReturnRequestEvent()
        {
            var affiseEvent = new OrderReturnRequestEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateOrderReturnRequestCancelEvent()
        {
            var affiseEvent = new OrderReturnRequestCancelEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreatePurchaseEvent()
        {
            var purchaseData = new JSONObject
            {
                ["phone"] = 1,
                ["case"] = 2,
            };

            var affiseEvent = new PurchaseEvent(
                userData: "items"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L)
                .AddPredefinedParameter(PredefinedObject.CONTENT, purchaseData);

            return affiseEvent;
        }

        private AffiseEvent CreateRateEvent()
        {
            var rate = new JSONObject
            {
                ["rating"] = 5,
            };

            var affiseEvent = new RateEvent(
                userData: "no bugs"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PREFERRED_NEIGHBORHOODS, "2")
                .AddPredefinedParameter(PredefinedLong.PREFERRED_NUM_STOPS, 4L)
                .AddPredefinedParameter(PredefinedFloat.PREFERRED_PRICE_RANGE, 10.22f)
                .AddPredefinedParameter(PredefinedLong.PREFERRED_STAR_RATINGS, 6L)
                .AddPredefinedParameter(PredefinedObject.CONTENT, rate);

            return affiseEvent;
        }

        private AffiseEvent CreateReEngageEvent()
        {
            var affiseEvent = new ReEngageEvent(
                userData: "web"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.CUSTOMER_USER_ID, "4")
                .AddPredefinedParameter(PredefinedString.VALIDATED, "data");

            return affiseEvent;
        }

        private AffiseEvent CreateReserveEvent()
        {
            var reserve = new List<JSONObject>()
            {
                new()
                {
                    ["item"] = "book",
                },
                new()
                {
                    ["item"] = "book",
                },
            };

            var affiseEvent = new ReserveEvent(
                userData: "web"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.CUSTOMER_USER_ID, "4")
                .AddPredefinedParameter(PredefinedListObject.CONTENT_LIST, reserve);

            return affiseEvent;
        }

        private AffiseEvent CreateScheduleEvent()
        {
            var affiseEvent = new ScheduleEvent(
                userData: "schedule"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L);

            return affiseEvent;
        }

        private AffiseEvent CreateSalesEvent()
        {
            var salesData = new JSONObject
            {
                ["phone"] = 1,
                ["case"] = 1,
            };

            var affiseEvent = new SalesEvent(
                userData: "apple"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.ORDER_ID, "23123")
                .AddPredefinedParameter(PredefinedFloat.PRICE, 2.19f)
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 1L)
                .AddPredefinedParameter(PredefinedObject.CONTENT, salesData);

            return affiseEvent;
        }

        private AffiseEvent CreateSearchEvent()
        {
            var arr = new JSONArray();
            arr.Add("eco milk");
            arr.Add("grass");

            var data = new JSONObject
            {
                ["data"] = arr
            };

            var affiseEvent = new SearchEvent(
                userData: "browser"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, data);

            return affiseEvent;
        }

        private AffiseEvent CreateShareEvent()
        {
            var share = new JSONObject
            {
                ["post_id"] = "252242",
                ["post_img"] = "img.webp",
            };

            var affiseEvent = new ShareEvent(
                userData: "telegram"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.RECEIPT_ID, "22")
                .AddPredefinedParameter(PredefinedObject.CONTENT, share);

            return affiseEvent;
        }

        private AffiseEvent CreateSpendCreditsEvent()
        {
            var affiseEvent = new SpendCreditsEvent(
                userData: "boosters"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedLong.QUANTITY, 2142);

            return affiseEvent;
        }

        private AffiseEvent CreateStartRegistrationEvent()
        {
            var registration = new JSONObject
            {
                ["time"] = "19:22:11",
            };

            var affiseEvent = new StartRegistrationEvent(
                userData: "referrer"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, registration);

            return affiseEvent;
        }

        private AffiseEvent CreateStartTrialEvent()
        {
            var trial = new JSONObject
            {
                ["time"] = "19:22:11",
            };

            var affiseEvent = new StartTrialEvent(
                userData: "30-days"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, trial);

            return affiseEvent;
        }

        private AffiseEvent CreateStartTutorialEvent()
        {
            var tutorial = new JSONObject
            {
                ["time"] = "19:22:11",
                ["reward"] = "22",
            };

            var affiseEvent = new StartTutorialEvent(
                userData: "video-feature"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, tutorial);

            return affiseEvent;
        }

        private AffiseEvent CreateSubmitApplicationEvent()
        {
            var affiseEvent = new SubmitApplicationEvent(
                userData: "submit"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1");

            return affiseEvent;
        }

        private AffiseEvent CreateSubscribeEvent()
        {
            var subscribe = new JSONObject
            {
                ["streamer"] = "cat",
            };

            var affiseEvent = new SubscribeEvent(
                userData: "wire"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, subscribe);

            return affiseEvent;
        }

        private AffiseEvent CreateTravelBookingEvent()
        {
            var arr = new JSONArray();
            arr.Add("may");
            arr.Add("august");

            var data = new JSONObject
            {
                ["months"] = arr
            };

            var affiseEvent = new TravelBookingEvent(
                userData: "booking"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.NUM_ADULTS, 1L)
                .AddPredefinedParameter(PredefinedLong.NUM_CHILDREN, 2L)
                .AddPredefinedParameter(PredefinedLong.NUM_INFANTS, 1L)
                .AddPredefinedParameter(PredefinedLong.DATE_A, "30.12.2020".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.DATE_B, "01.01.2021".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.DEPARTING_ARRIVAL_DATE, "01.01.2021".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.DEPARTING_DEPARTURE_DATE, "30.12.2020".ToTimeStamp())
                .AddPredefinedParameter(PredefinedString.DESTINATION_A, "usa")
                .AddPredefinedParameter(PredefinedString.DESTINATION_B, "mexico")
                .AddPredefinedParameter(PredefinedString.DESTINATION_LIST, "usa, mexico")
                .AddPredefinedParameter(PredefinedLong.HOTEL_SCORE, 5L)
                .AddPredefinedParameter(PredefinedLong.TRAVEL_START, "01.12.2020".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.TRAVEL_END, "01.12.2021".ToTimeStamp())
                .AddPredefinedParameter(PredefinedObject.CONTENT, data);

            return affiseEvent;
        }

        private AffiseEvent CreateUnlockAchievementEvent()
        {
            var achievement = new JSONObject
            {
                ["achievement"] = "new level",
            };

            var affiseEvent = new UnlockAchievementEvent(
                userData: "best damage"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.USER_SCORE, 12552L)
                .AddPredefinedParameter(PredefinedString.ACHIEVEMENT_ID, "1334-1225-ASDZ")
                .AddPredefinedParameter(PredefinedObject.CONTENT, achievement);

            return affiseEvent;
        }

        private AffiseEvent CreateUnsubscribeEvent()
        {
            var unsubscribe = new JSONObject
            {
                ["reason"] = "span",
            };

            var affiseEvent = new UnsubscribeEvent(
                userData: "02.01.2021"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, unsubscribe);

            return affiseEvent;
        }

        private AffiseEvent CreateUpdateEvent()
        {
            var data = new JSONArray();
            data.Add("com.facebook");

            var affiseEvent = new UpdateEvent(
                userData: "firmware"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.EVENT_START, "01.02.2021".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.EVENT_END, "01.01.2022".ToTimeStamp())
                .AddPredefinedParameter(PredefinedString.NEW_VERSION, "8")
                .AddPredefinedParameter(PredefinedString.OLD_VERSION, "1.8")
                .AddPredefinedParameter(PredefinedObject.CONTENT, new JSONObject
                {
                    ["data"] = data,
                });

            return affiseEvent;
        }

        private AffiseEvent CreateViewAdvEvent()
        {
            var ad = new JSONObject
            {
                ["source"] = "amazon",
            };

            var affiseEvent = new ViewAdvEvent(
                userData: "skip"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.RETURNING_ARRIVAL_DATE, "01.12.2021".ToTimeStamp())
                .AddPredefinedParameter(PredefinedLong.RETURNING_DEPARTURE_DATE, "01.12.2020".ToTimeStamp())
                .AddPredefinedParameter(PredefinedObject.CONTENT, ad);

            return affiseEvent;
        }

        private AffiseEvent CreateViewContentEvent()
        {
            var affiseEvent = new ViewContentEvent(
                userData: "ViewContent"
            );

            affiseEvent.AddPredefinedParameter(PredefinedString.PARAM_01, "param1");

            return affiseEvent;
        }

        private AffiseEvent CreateViewCartEvent()
        {
            var objects = new JSONObject
            {
                ["cart_value"] = "25.22$",
                ["cart_items"] = "2",
            };

            var affiseEvent = new ViewCartEvent(
                userData: "main"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedString.PARAM_01, "param1")
                .AddPredefinedParameter(PredefinedObject.CONTENT, objects);

            return affiseEvent;
        }

        private AffiseEvent CreateViewItemEvent()
        {
            var item = new JSONObject
            {
                ["section_name"] = "header",
            };

            var affiseEvent = new ViewItemEvent(
                userData: "main"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.MAX_RATING_VALUE, 5L)
                .AddPredefinedParameter(PredefinedLong.RATING_VALUE, 9L)
                .AddPredefinedParameter(PredefinedObject.CONTENT, item);

            return affiseEvent;
        }

        private AffiseEvent CreateViewItemsEvent()
        {
            var data = new List<JSONObject>
            {
                new() { ["section_name"] = "header" },
                new() { ["section_name"] = "section-1" },
                new() { ["section_name"] = "section-2" },
                new() { ["section_name"] = "footer" },
            };


            var affiseEvent = new ViewItemsEvent(
                userData: "main"
            );

            affiseEvent
                .AddPredefinedParameter(PredefinedLong.MAX_RATING_VALUE, 5L)
                .AddPredefinedParameter(PredefinedLong.RATING_VALUE, 9L)
                .AddPredefinedParameter(PredefinedListObject.CONTENT_LIST, data);

            return affiseEvent;
        }

        private AffiseEvent CreateInitialSubscriptionEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new InitialSubscriptionEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateInitialTrialEventEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new InitialTrialEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateInitialOfferEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new InitialOfferEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedTrialEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedTrialEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedOfferiseEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedOfferiseEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedSubscriptionEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedSubscriptionEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedTrialFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedTrialFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedOfferFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedOfferFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateFailedSubscriptionFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new FailedSubscriptionFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateTrialInRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new TrialInRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateOfferInRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new OfferInRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateSubscriptionInRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new SubscriptionInRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateConvertedTrialEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new ConvertedTrialEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateConvertedOfferEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new ConvertedOfferEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateReactivatedSubscriptionEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new ReactivatedSubscriptionEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateRenewedSubscriptionEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new RenewedSubscriptionEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateConvertedTrialFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new ConvertedTrialFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateConvertedOfferFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new ConvertedOfferFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateRenewedSubscriptionFromRetryEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var affiseEvent = new RenewedSubscriptionFromRetryEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }

        private AffiseEvent CreateUnsubscriptionEvent()
        {
            var data = new JSONObject
            {
                ["affise_event_revenue"] = 2.99,
                ["affise_event_currency"] = "USD",
                ["affise_event_product_id"] = 278459628375,
            };

            var t = new UnsubscriptionEvent(new JSONObject { });
            var affiseEvent = new UnsubscriptionEvent(
                data: data,
                userData: "Subscription Plus"
            );

            affiseEvent.AddPredefinedParameter(PredefinedFloat.REVENUE, 225522f);

            return affiseEvent;
        }
    }

    internal static class DateTimeExt
    {
        public static long ToTimeStamp(this string dateString)
        {
            var date = DateTime.ParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return date.GetTimeInMillis();
        }
    }
}
