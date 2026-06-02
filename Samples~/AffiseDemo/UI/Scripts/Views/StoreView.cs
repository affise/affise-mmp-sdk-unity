#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib;
using AffiseAttributionLib.Module.Subscription;
using UnityEngine;
using UnityEngine.UIElements;

namespace AffiseAppDemo
{
    [ExecuteAlways]
    [RequireComponent(typeof(UIDocument))]
    [RequireComponent(typeof(AlertMessage))]
    public class StoreView : MonoBehaviour
    {
        [SerializeField]
        private string storeViewName = "StoreView";

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

        private readonly List<(Button Button, Action Callback)> _buttons = new();
        private readonly List<AffiseProduct> _products = new();

        private UIDocument? _document;
        private VisualElement? _view;
        private ScrollView? _productsView;
        private AlertMessage? _alertMessage;

        private void Awake()
        {
            _document = GetComponent<UIDocument>();
            _alertMessage = GetComponent<AlertMessage>();
        }
        
        private void OnEnable()
        {
            RebuildView();
        }

        private void OnDisable()
        {
            ClearButtons();
            _view = null;
            _productsView = null;
        }

        private void OnValidate()
        {
            if (isActiveAndEnabled)
            {
                RebuildView();
            }
        }

        private void RebuildView()
        {
            UiToolkitUtils.RunWhenActiveAndEnabled(this, FillView);
        }

        private void FillView()
        {
            _document ??= GetComponent<UIDocument>();
            if (_document == null)
            {
                return;
            }

            var root = _document.rootVisualElement;
            if (root == null)
            {
                return;
            }

            _view = root.Q<VisualElement>(storeViewName);
            if (_view == null)
            {
                Debug.LogWarning($"{storeViewName} element is not found.");
                return;
            }

            ClearButtons();
            _view.Clear();

            var fetchButton = CreateButton("Fetch Products", () => FetchProducts(false));
            fetchButton.AddToClassList("store-fetch-button");
            _view.Add(fetchButton);

            _productsView = new ScrollView(ScrollViewMode.Vertical);
            _productsView.AddToClassList("events-list");
            _productsView.AddToClassList("store-products-list");
            UiToolkitUtils.ConfigureScrollView(_productsView);
            _view.Add(_productsView);

            UpdateProductsView();
        }

        private void UpdateProductsView()
        {
            if (_productsView == null)
            {
                return;
            }

            _productsView.Clear();

            foreach (var product in _products)
            {
                AddProductView(_productsView, product);
            }
        }

        private void AddProductView(VisualElement parent, AffiseProduct product)
        {
            var container = new VisualElement();
            container.AddToClassList("store-product-card");

            var description = new VisualElement();
            description.AddToClassList("store-product-content");

            var title = new Label(product.Title);
            title.AddToClassList("store-product-title");
            description.Add(title);

            var productDescription = new Label(product.Description);
            productDescription.AddToClassList("store-product-description");
            description.Add(productDescription);

            if (product.Subscription != null)
            {
                var subscription = new Label($"{product.Subscription.NumberOfUnits} {product.Subscription.TimeUnit?.ToValue()}");
                subscription.AddToClassList("store-product-subscription");
                description.Add(subscription);
            }

            container.Add(description);

            var price = new Label(product.Price?.FormattedPrice ?? string.Empty);
            price.AddToClassList("store-product-price");
            container.Add(price);

            container.Add(CreateButton("Buy", () => Purchase(product), "store-buy-button"));
            parent.Add(container);
        }

        private Button CreateButton(string title, Action action, string? styleClass = null)
        {
            var button = new Button
            {
                text = title,
            };
            button.AddToClassList("event-button");

            if (!string.IsNullOrWhiteSpace(styleClass))
            {
                button.AddToClassList(styleClass);
            }

            button.clicked += action;
            _buttons.Add((button, action));
            return button;
        }

        private void FetchProducts(bool skipCheck = true)
        {
            Affise.Module.Subscription.FetchProducts(_storeData.Keys.ToList(), result =>
            {
                if (result.IsSuccess)
                {
                    var value = result.AsSuccess;
                    _products.Clear();
                    _products.AddRange(value.Products);
                    UpdateProductsView();

                    var invalidIds = value.InvalidIds;
                    if (!skipCheck && invalidIds.Count > 0)
                    {
                        Alert("Invalid Ids", string.Join("\n", invalidIds));
                    }
                }
                else
                {
                    Alert("Error", result.AsFailure);
                }
            });
        }

        private void Purchase(AffiseProduct product)
        {
            var type = _storeData.GetValueOrDefault(product.ProductId);
            Affise.Module.Subscription.Purchase(product, type, result =>
            {
                if (result.IsSuccess)
                {
                    Debug.Log($"Purchase {product.ProductId} success: {result.AsSuccess}");
                }
                else
                {
                    Alert("Error", result.AsFailure);
                }
            });
        }

        private void Alert(string title, string message)
        {
            _alertMessage?.Show(title, message);
        }

        private void ClearButtons()
        {
            foreach (var (button, callback) in _buttons)
            {
                button.clicked -= callback;
            }

            _buttons.Clear();
        }
    }
}
