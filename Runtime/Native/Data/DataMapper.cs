#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Module;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.SimpleJSON;
using UnityEngine;

namespace AffiseAttributionLib.Native.Data
{
    internal static class DataMapper
    {
        private delegate T Transformer<out T>(JSONNode? node);

        public static string ToNonNullString(JSONNode? json)
        {
            return json?.Value ?? "";
        }
        
        public static string? ToStringOrNull(JSONNode? json)
        {
            return json?.Value;
        }

        public static List<AffiseKeyValue> ToAffiseKeyValueList(JSONNode? json)
        {
            var result = new List<AffiseKeyValue>();
            if (json is null) return result;
            var jsonArray = json.AsArray;
            if (jsonArray is null) return result;
            foreach (var (_, jsonNode) in jsonArray)
            {
                var jsonObject = jsonNode?.AsObject;
                if (jsonObject is null) continue;
                result.Add(new AffiseKeyValue(jsonObject));
            }

            return result;
        }

        public static DeeplinkValue ToDeeplinkValue(JSONNode? from)
        {
            var json = from?.AsObject;
            var parameters = new Dictionary<string, List<string>>();
            var paramObject = json?[DataName.PARAMETERS]?.AsObject;
            if (paramObject is not null)
            {
                foreach (var (key, value) in paramObject)
                {
                    parameters[key] = value?.AsArray?.ToListOfStrings() ?? new List<string>();
                }
            }

            return new DeeplinkValue(
                deeplink: json?[DataName.DEEPLINK]?.Value ?? "",
                scheme: json?[DataName.SCHEME]?.Value,
                host: json?[DataName.HOST]?.Value,
                path: json?[DataName.PATH]?.Value,
                parameters: parameters
            );
        }

        public static Dictionary<string, object?> FromProduct(AffiseProduct product)
        {
            return new Dictionary<string, object?>
            {
                { DataName.PRODUCT_ID, product.ProductId },
                { DataName.TITLE, product.Title },
                { DataName.DESCRIPTION, product.Description },
                { DataName.PRODUCT_TYPE, product.ProductType.ToValue() },
                { DataName.PRICE, FromPrice(product.Price) },
                { DataName.SUBSCRIPTION, FromSubscription(product.Subscription) },
            };
        }

        private static Dictionary<string, object>? FromPrice(AffiseProductPrice? price)
        {
            if (price == null) return null;

            return new Dictionary<string, object>
            {
                { DataName.VALUE, price.Value },
                { DataName.CURRENCY_CODE, price.CurrencyCode },
                { DataName.FORMATTED_PRICE, price.FormattedPrice },
            };
        }

        private static Dictionary<string, object?>? FromSubscription(AffiseProductSubscriptionDetail? subscription)
        {
            if (subscription == null) return null;

            return new Dictionary<string, object?>
            {
                { DataName.OFFER_TOKEN, subscription.OfferToken },
                { DataName.OFFER_ID, subscription.OfferId },
                { DataName.TIME_UNIT, subscription.TimeUnit?.ToValue() },
                { DataName.NUMBER_OF_UNITS, subscription.NumberOfUnits },
            };
        }

        public static AffiseResult<AffiseProductsResult> ToResultAffiseProductsResult(JSONNode? from)
        {
            return ToResult(from, ToAffiseProductsResult);
        }

        public static AffiseResult<AffisePurchasedInfo> ToResultAffisePurchasedInfo(JSONNode? from)
        {
            return ToResult(from, ToAffisePurchasedInfo);
        }

        private static AffiseResult<T> ToResult<T>(JSONNode? from, Transformer<T?> transformer)
        {
            var json = from?.AsObject;

            var error = json?[DataName.ERROR]?.ToString();
            var success = json?[DataName.SUCCESS];

            if (string.IsNullOrEmpty(error))
            {
                return AffiseResult<T>.Failure(error ?? "error");
            }

            var data = transformer(success);
            if (data == null)
            {
                return AffiseResult<T>.Failure($"{typeof(T)} serialization error. Data: {from}");
            }

            return AffiseResult<T>.Success(data);
        }

        private static AffiseProductsResult? ToAffiseProductsResult(JSONNode? from)
        {
            if (from == null) return null;
            var data = from.AsObject;       
            if (data == null) return null;
            
            var invalidIds = data[DataName.INVALID_IDS]?.AsArray?.ToListOfStrings();
            
            return new AffiseProductsResult(
                products: ToProductsList(data[DataName.PRODUCTS]),
                invalidIds: invalidIds ?? new List<string>()
            );
        }

        private static AffisePurchasedInfo? ToAffisePurchasedInfo(JSONNode? from)
        {
            if (from == null) return null;
            var data = from.AsObject;       
            if (data == null) return null;
            
            return new AffisePurchasedInfo(
                product: ToProduct(data[DataName.PRODUCT]),
                orderId: data[DataName.ORDER_ID]?.Value,
                originalOrderId: data[DataName.ORIGINAL_ORDER_ID]?.Value,
                purchase: data[DataName.PURCHASE]?.Value
            );
        }

        private static List<AffiseProduct> ToProductsList(JSONNode? from)
        {
            var result = new List<AffiseProduct>();

            var jsonArray = from?.AsArray;
            if (jsonArray is null) return result;
            foreach (var (_, jsonNode) in jsonArray)
            {
                var product = ToProduct(jsonNode);
                if (product is null) continue;
                result.Add( product);
            }
            
            return result;
        }
        
        private static AffiseProduct? ToProduct(JSONNode? from)
        {
            if (from == null) return null;
            var data = from.AsObject;       
            if (data == null) return null;
            
            var productType = AffiseProductTypeExt.From(data[DataName.PRODUCT_TYPE]);

            var productId = data[DataName.PRODUCT_ID]?.Value;
            var title = data[DataName.TITLE]?.Value ?? "";
            var description = data[DataName.DESCRIPTION]?.Value ?? "";

            if (productId == null) {
                return null;
            }

            return new AffiseProduct(
                productId: productId,
                title: title,
                description: description,
                productType: productType ?? AffiseProductType.CONSUMABLE,
                price: ToPrice(data[DataName.PRICE]),
                subscription: ToSubscriptionDetail(data[DataName.SUBSCRIPTION])
                // productDetails: data[DataName.PRODUCT_DETAILS]?.Value,
            );
        }

        private static AffiseProductPrice? ToPrice(JSONNode? from)
        {
            if (from == null) return null;
            var data = from.AsObject;       
            if (data == null) return null;
            
            return new AffiseProductPrice(
                value: data[DataName.VALUE]?.AsFloat ?? 0.0f,
                currencyCode: data[DataName.CURRENCY_CODE]?.Value ?? "",
                formattedPrice: data[DataName.FORMATTED_PRICE]?.Value ?? ""
            );
        }
        
        private static AffiseProductSubscriptionDetail? ToSubscriptionDetail(JSONNode? from)
        {
            if (from == null) return null;
            var data = from.AsObject;
            if (data == null) return null;

            return new AffiseProductSubscriptionDetail(
                offerToken: data[DataName.OFFER_TOKEN]?.Value,
                offerId: data[DataName.OFFER_ID]?.Value,
                timeUnit: TimeUnitTypeExt.From(data[DataName.TIME_UNIT]),
                numberOfUnits: data[DataName.NUMBER_OF_UNITS]?.AsInt
            );
        }
    }
}