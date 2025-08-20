#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib;
using AffiseAttributionLib.Events.Subscription;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Referrer;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseDemo
{
    public class AffiseDemo : BaseUi
    {
        private readonly DefaultEventsFactory _eventsFactory = new ();
        private readonly AffiseApiFactory _apiFactory = new();

        private VisualElement? _productsView;
        
        protected override void Init()
        {
            _apiFactory.Output = Output;
            _apiFactory.Alert = Alert;
        
            AffiseInit();
        }

        private void AffiseInit()
        {
            // Initialize https://github.com/affise/affise-mmp-sdk-unity#manual
            // For manual init delete "Affise Settings.asset" or disable [isActive] flag  
            // Affise
            //     .Settings(
            //         affiseAppId: "129",
            //         secretKey: "93a40b54-6f12-443f-a250-ebf67c5ee4d2"
            //     )
            //     .SetConfigValue(AffiseConfig.FB_APP_ID, "1111111111111111")
            //     .SetProduction(false) //To enable debug methods set Production to false
            //     .Start(); // Start Affise SDK

            // Debug: network request/response
            Affise.Debug.Network((request, response) =>
            {
                // Debug.Log($"Affise: {request}");
                Debug.Log($"Affise: {response}");
            });

            // Deeplinks https://github.com/affise/affise-mmp-sdk-unity#deeplinks
            Affise.RegisterDeeplinkCallback(value =>
            {
                var list = value.Parameters.Select(h => $"{h.Key}=[{string.Join(", ", h.Value)}]").ToList();
                var parameters = string.Join(", ", list);
                Alert("Deeplink", $"{value.Deeplink}\n\n" +
                       $"scheme={value.Scheme}\n" +
                       $"host={value.Host}\n" +
                       $"path={value.Path}\n" +
                       $"parameters=[{parameters}]"
                );
            });
        }

        protected override void ApiView(VisualElement view)
        {
            view.Clear();
            
            foreach (var (apiName, apiCall) in _apiFactory.Create())
            {
                if (apiName == "Get Referrer Value")
                {
                    AddDropdown(view, "", ReferrerValues(), value =>
                    {
                        _apiFactory.ReferrerValue = value;
                    });
                }

                AddButton(view, apiName,null, apiCall);
            }
        }

        protected override void EventsView(VisualElement view)
        {
            view.Clear();
            
            foreach (var affiseEvent in _eventsFactory.CreateEvents())
            {
                var styleClass = (affiseEvent is BaseSubscriptionEvent) 
                    ? "affise-subscription-event"
                    : null;

                AddButton(view, ToCamelCase(affiseEvent.GetName()), styleClass, () => { 
                    // Events tracking https://github.com/affise/affise-mmp-sdk-unity#events-tracking
                    // Send event
                    affiseEvent.Send();
                    // or
                    // Affise.SendEvent(affiseEvent);
                    // or
                    // affiseEvent.SendNow(() =>
                    // {
                    //     Output($"Send {affiseEvent.GetName()} success");
                    // }, (status) =>
                    // {
                    //     Output($"Send {affiseEvent.GetName()} failed {status}");
                    // });
                });
            }
        }

        #region Store

        protected override void OnTabShow(int index)
        {
            if (index == 2)
            {
                FetchProducts();
                UpdateProductsView(_products);
            }
        }

        private readonly Dictionary<string, AffiseProductType> _storeData = new()
        {
            { "com.test.invalid", AffiseProductType.CONSUMABLE },
            { "com.test.prod_1", AffiseProductType.CONSUMABLE },
            { "com.test.prod_2", AffiseProductType.CONSUMABLE },
            { "com.test.prod_3", AffiseProductType.NON_CONSUMABLE },
            { "com.test.sub_1", AffiseProductType.NON_RENEWABLE_SUBSCRIPTION },
            { "com.test.sub_2", AffiseProductType.RENEWABLE_SUBSCRIPTION },
            { "com.test.sub_3", AffiseProductType.RENEWABLE_SUBSCRIPTION },
        };

        private List<AffiseProduct> _products = new();
        
        protected override void StoreView(VisualElement view)
        {
            view.Clear();
            
            AddButton(view, "Fetch Products",null, () =>
            {
                FetchProducts(false);
            });
            
            _productsView = new ScrollView();
            view.Add(_productsView);
        }

        private void UpdateProductsView(List<AffiseProduct> affiseProducts)
        {
            if (_productsView is null) return;
            _productsView.Clear();
        
            foreach (var product in affiseProducts)
            {
                AddProductView(_productsView, product, () =>
                {
                    Purchase(product);
                });
            }
        }

        private void AddProductView(VisualElement scrollView, AffiseProduct product, Action buy)
        {
            AddView(scrollView, (elem) =>
            {
                var container = new VisualElement
                {
                    style =
                    {
                        paddingLeft = 8,
                        paddingRight = 8,
                        paddingTop = 8,
                        paddingBottom = 8,
                        marginBottom = 8,
                        borderBottomLeftRadius = 8,
                        borderBottomRightRadius = 8,
                        borderTopLeftRadius = 8,
                        borderTopRightRadius = 8,
                        flexDirection = FlexDirection.Row,
                        alignItems = Align.Center,
                        backgroundColor = new StyleColor(new Color(0.0f, 0.0f, 0.0f, 0.1f))
                    }
                };

                var desc = new VisualElement
                {
                    style =
                    {
                        flexGrow = 1
                    }
                };

                desc.Add(new Label(product.Title)
                {
                    style =
                    {
                        color = new StyleColor(Color.white)
                    }
                });
                desc.Add(new Label(product.Description)
                {
                    style =
                    {
                        color = new StyleColor(Color.gray)
                    }
                });

                if (product.Subscription is not null)
                {
                    desc.Add(new Label($"{product.Subscription.NumberOfUnits} {product.Subscription.TimeUnit?.ToValue()}")
                    {
                        style =
                        {
                            color = new StyleColor(Color.white)
                        }
                    });
                }

                container.Add(desc);
                
                container.Add(new Label(product.Price?.FormattedPrice)
                {
                    style =
                    {
                        color = new StyleColor(Color.white)
                    }
                });
                
                AddButton(container, "Buy",null, buy);
                
                elem.Add(container);
            });
        }

        private void FetchProducts(bool skipCheck = true)
        {
            Affise.Module.Subscription.FetchProducts(_storeData.Keys.ToList(), (result) =>
            {
                    if (result.IsSuccess)
                    {
                        var value = result.AsSuccess;
                        _products = value.Products;
                        UpdateProductsView(_products);
                        var invalidIds = value.InvalidIds;
                        
                        if (!skipCheck && invalidIds.Count > 0)
                        {
                            Alert("Invalid Ids", string.Join("\n", invalidIds));
                        }
                    }
                    else
                    {
                        var error = result.AsFailure;
                        Alert("Error", error);
                    }
            });
        }

        private void Purchase(AffiseProduct product)
        {
            var type = _storeData.GetValueOrDefault(product.ProductId);
            
            Affise.Module.Subscription.Purchase(product, type, (result) =>
            {
                if (result.IsSuccess)
                {
                    var info = result.AsSuccess;
                }
                else
                {
                    var error = result.AsFailure;
                    Alert("Error", error);
                }
            });
        }
        
        #endregion

        private static List<string> ReferrerValues()
        {
            return Enum.GetValues(typeof(ReferrerKey))
                .Cast<ReferrerKey>()
                .Select(s => s.ToString())
                .ToList();
        }
    }
}
