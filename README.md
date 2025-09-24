# Affise Unity package

[Change Log](CHANGELOG.md)

| Artifact      | Version               |
|---------------|-----------------------|
| `attribution` | [`1.6.46`](/releases/tag/1.6.46) |

- [Affise Unity package](#affise-unity-package)
- [Description](#description)
  - [Quick start](#quick-start)
    - [SDK compatibility](#sdk-compatibility)
  - [Integration](#integration)
    - [Integrate unity package](#integrate-unity-package)
    - [Integrate unitypackage file](#integrate-unitypackage-file)
    - [Initialize](#initialize)
      - [Unity asset](#unity-asset)
        - [Domain](#domain)
      - [Manual](#manual)
        - [Domain](#domain-1)
      - [Before application is published](#before-application-is-published)
    - [Requirements](#requirements)
      - [iOS](#ios)
    - [Modules](#modules)
      - [Android](#android)
      - [iOS](#ios-1)
      - [Installed active modules](#installed-active-modules)
      - [Manual exclude modules](#manual-exclude-modules)
      - [Module AdService](#module-adservice)
      - [Module Advertising](#module-advertising)
        - [iOS](#ios-2)
      - [Module Huawei](#module-huawei)
      - [Module AppsFlyer](#module-appsflyer)
      - [Module Link](#module-link)
      - [Module Meta](#module-meta)
      - [Module Status](#module-status)
      - [Module Subscription](#module-subscription)
      - [Module TikTok](#module-tiktok)
  - [Build](#build)
    - [iOS](#ios-3)
- [Features](#features)
  - [ProviderType identifiers collection](#providertype-identifiers-collection)
    - [Attribution](#attribution)
    - [AdService](#adservice)
    - [Advertising](#advertising)
    - [AndroidId](#androidid)
    - [Huawei](#huawei)
    - [Meta](#meta)
    - [Network](#network)
    - [Phone](#phone)
  - [Event send control](#event-send-control)
  - [Events tracking](#events-tracking)
    - [Custom events tracking](#custom-events-tracking)
  - [Predefined event parameters](#predefined-event-parameters)
    - [PredefinedString](#predefinedstring)
    - [PredefinedLong](#predefinedlong)
    - [PredefinedFloat](#predefinedfloat)
    - [PredefinedObject](#predefinedobject)
    - [PredefinedListObject](#predefinedlistobject)
    - [PredefinedListString](#predefinedliststring)
  - [Events buffering](#events-buffering)
  - [Advertising Identifier (google) tracking](#advertising-identifier-google-tracking)
  - [Open Advertising Identifier (huawei) tracking](#open-advertising-identifier-huawei-tracking)
  - [Push token tracking](#push-token-tracking)
    - [Firebase Flutter Plugin](#firebase-flutter-plugin)
  - [Links](#links)
    - [Config](#config)
    - [Deeplinks manual](#deeplinks-manual)
      - [Android](#android-1)
      - [iOS](#ios-4)
    - [Applinks manual](#applinks-manual)
      - [Android](#android-2)
      - [iOS](#ios-5)
    - [Get deferred deeplink](#get-deferred-deeplink)
    - [Get deferred deeplink value](#get-deferred-deeplink-value)
  - [Offline mode](#offline-mode)
  - [Disable tracking](#disable-tracking)
  - [Disable background tracking](#disable-background-tracking)
  - [Get random user Id](#get-random-user-id)
  - [Get random device Id](#get-random-device-id)
  - [Get providers](#get-providers)
  - [Is first run](#is-first-run)
  - [Get referrer](#get-referrer)
  - [Get referrer value](#get-referrer-value)
  - [Referrer keys](#referrer-keys)
  - [Get module state](#get-module-state)
  - [Platform specific](#platform-specific)
    - [StoreKit Ad Network](#storekit-ad-network)
- [SDK to SDK integrations](#sdk-to-sdk-integrations)
- [Debug](#debug)
  - [Validate credentials](#validate-credentials)
  - [Version](#version)
  - [Version native](#version-native)
- [Troubleshoots](#troubleshoots)
  - [iOS](#ios-6)
  - [Android](#android-3)

# Description

Affise SDK is a software you can use to collect app usage statistics, device identifiers, deeplink usage, track install
referrer.

## Quick start

### SDK compatibility

- `Xcode`   `14.2+`  
- `iOS`     `12+`
- `Android` `24+`

## Integration

### Integrate unity package

Open Package Manager(`Window / Package Manager`)

Add package from git url `https://github.com/affise/affise-mmp-sdk-unity.git`

<div align=center>
    <img src="https://github.com/affise/affise-mmp-sdk-unity/blob/assets/package_git.png?raw=true" alt="package_manager">
</div>

### Integrate unitypackage file

Download latest Affise SDK [`attribution-1.6.46.unitypackage`](https://github.com/affise/affise-mmp-sdk-unity/releases/download/1.6.46/attribution-1.6.46.unitypackage)
from [releases page](https://github.com/affise/affise-mmp-sdk-unity/releases) and drop this file to unity editor

### Initialize

#### Unity asset

After package is added to unity project, initialize affise settings.

Open Project Settings(`Edit / Project Settings`)

On Affise tab click `Create` button.

![affise_settings_new](https://github.com/affise/affise-mmp-sdk-unity/blob/assets/affise_settings_new.png?raw=true)

This will create `Affise Settings.asset` in `Assets / Affise / Resources` directory.

> [!NOTE] 
>
> 🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦
>
> Settings set in `Edit / Project Settings / Affise` are marked as `Active Settings`
>
> Affise is using settings marked as `Active Settings`
>
> located in root of folder `Resources` which can be located in any folder
>
> Example `<Any folder> / Resources / <Your affise settings>.asset`
>
> 🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦🟦

> [!CAUTION]
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧
>
> Settings located in `Editor` folder are **invalid**.
>
> Example: `Editor / Resources / <Your affise settings>.asset`
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧

Fill all required fields

![affise_settings](https://github.com/affise/affise-mmp-sdk-unity/blob/assets/affise_settings.png?raw=true)

##### Domain

Set SDK server domain:

1. Open `Edit / Project Settings / Affise`
2. Unfold `Optional` section at the bottom
3. Fill `Domain` field

#### Manual

> Demo app [AffiseDemo.cs](Samples~/AffiseDemoApp/Scripts/AffiseDemo.cs)

```c#
Affise
    .Settings(
        affiseAppId: "Your appId", //Change to your app id
        secretKey: "Your SDK secretKey" //Change to your SDK secretKey
    )
    .Start(); // Start Affise SDK
```

##### Domain

Set SDK server domain:

```c#
Affise
    .Settings(
        affiseAppId: "Your appId",
        secretKey: "Your SDK secretKey"
    )
    .setDomain("https://YoureCustomDomain/") // Set custom domain
    .Start(); // Start Affise SDK
```

#### Before application is published

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Please make sure your credentials are valid
>
> Visit section [validation credentials](#validate-credentials)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

### Requirements

#### iOS

Affise Advertising module uses `AppTrackingTransparency` framework to get `advertisingIdentifier`
For working functionality your app needs to declare [`NSUserTrackingUsageDescription` permission](https://developer.apple.com/documentation/bundleresources/information_property_list/nsusertrackingusagedescription):

Key `NSUserTrackingUsageDescription` value is set in `Edit / Project Settings / Affise / Modules`

Key added automatically then module `Advertising` is selected

Default value is empty string ""

### Modules

Open `Edit / Project Settings / Affise`

On `Modules` tab select all required

![affise_modules](https://github.com/affise/affise-mmp-sdk-unity/blob/assets/affise_modules.png?raw=true)

#### Android

All affise modules is updated automatically on build

> [!CAUTION]
>
> **No manual editing is required**

| Module         | Version                                                                                                                                                                      |
|----------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Advertising`  | [![module-advertising](https://img.shields.io/maven-central/v/com.affise/module-advertising?label=latest)](https://mvnrepository.com/artifact/com.affise/module-advertising) |
| `AndroidId`    | [![module-androidid](https://img.shields.io/maven-central/v/com.affise/module-androidid?label=latest)](https://mvnrepository.com/artifact/com.affise/module-androidid)       |
| `AppsFlyer`    | [![module-appsflyer](https://img.shields.io/maven-central/v/com.affise/module-appsflyer?label=latest)](https://mvnrepository.com/artifact/com.affise/module-appsflyer)       |
| `Link`         | [![module-link](https://img.shields.io/maven-central/v/com.affise/module-link?label=latest)](https://mvnrepository.com/artifact/com.affise/module-link)                      |
| `Network`      | [![module-network](https://img.shields.io/maven-central/v/com.affise/module-network?label=latest)](https://mvnrepository.com/artifact/com.affise/module-network)             |
| `Phone`        | [![module-phone](https://img.shields.io/maven-central/v/com.affise/module-phone?label=latest)](https://mvnrepository.com/artifact/com.affise/module-phone)                   |
| `Status`       | [![module-status](https://img.shields.io/maven-central/v/com.affise/module-status?label=latest)](https://mvnrepository.com/artifact/com.affise/module-status)                |
| `Subscription` | [![module-status](https://img.shields.io/maven-central/v/com.affise/module-subscription?label=latest)](https://mvnrepository.com/artifact/com.affise/module-subscription)    |
| `RuStore`      | [![module-rustore](https://img.shields.io/maven-central/v/com.affise/module-rustore?label=latest)](https://mvnrepository.com/artifact/com.affise/module-rustore)             |
| `Huawei`       | [![module-huawei](https://img.shields.io/maven-central/v/com.affise/module-huawei?label=latest)](https://mvnrepository.com/artifact/com.affise/module-huawei)                |
| `Meta`         | [![module-meta](https://img.shields.io/maven-central/v/com.affise/module-meta?label=latest)](https://mvnrepository.com/artifact/com.affise/module-meta)                      |
| `TikTok`       | [![module-tiktok](https://img.shields.io/maven-central/v/com.affise/module-tiktok?label=latest)](https://mvnrepository.com/artifact/com.affise/module-tiktok)                |

Dependencies located in Android project gradle file `build.gradle`

```gradle
final affise_version = '1.6.65'

dependencies {
    // ...
    // Affise modules
    implementation "com.affise:module-advertising:$affise_version"
    implementation "com.affise:module-androidid:$affise_version"
    implementation "com.affise:module-link:$affise_version"
    implementation "com.affise:module-network:$affise_version"
    implementation "com.affise:module-phone:$affise_version"
    implementation "com.affise:module-status:$affise_version"
    implementation "com.affise:module-subscription:$affise_version"
    implementation "com.affise:module-meta:$affise_version"
    implementation "com.affise:module-rustore:$affise_version"
    implementation "com.affise:module-huawei:$affise_version"
    implementation "com.affise:module-appsflyer:$affise_version"
    implementation "com.affise:module-tiktok:$affise_version"
}
```

#### iOS

All affise modules is updated automatically on build

> [!CAUTION]
>
> **No manual editing is required**

| Module         |                                       Version                                        |
|----------------|:------------------------------------------------------------------------------------:|
| `AdService`    | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `Advertising`  | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `AppsFlyer`    | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `Link`         | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `Persistent`   | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `Status`       | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `Subscription` | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |
| `TikTok`       | [`1.6.55`](https://github.com/CocoaPods/Specs/tree/master/Specs/0/3/d/AffiseModule/) |

Dependencies located in XCode project folder `Podfile`

```rb
platform :ios, '12.0'

target 'UnityFramework' do
  pod 'AffiseInternal', '1.6.55'

  # Affise Modules
  pod 'AffiseModule/AdService', '1.6.55'
  pod 'AffiseModule/Advertising', '1.6.55'
  pod 'AffiseModule/AppsFlyer', '1.6.55'
  pod 'AffiseModule/Link', '1.6.55'
  pod 'AffiseModule/Persistent', '1.6.55'
  pod 'AffiseModule/Status', '1.6.55'
  pod 'AffiseModule/Subscription', '1.6.55'
  pod 'AffiseModule/TikTok', '1.6.55'
end

target 'Unity-iPhone' do
end

use_frameworks! :linkage => :static
```

#### Installed active modules

Get list of installed modules:

```c#
Affise.Module.GetModulesInstalled()
```

#### Manual exclude modules

To manually stop modules from starting use `Affise.Settings.SetDisableModules`:

```c#
Affise
    .Settings(
        affiseAppId: "Your appId", //Change to your app id
        secretKey: "Your SDK secretKey", //Change to your SDK secretKey
    )
    .SetDisableModules(new List<AffiseModules> {
        // Exclude modules from start
        AffiseModules.Advertising,
        AffiseModules.Subscription,
    })
    .Start(); // Start Affise SDK
```

#### Module AdService

`iOS 14.3+`

Sends `attributionToken` from `AdServices` framework `AAAttribution.attributionToken()` to Affise server

#### Module Advertising

##### iOS

This module required to Use [`IDFA`](https://developer.apple.com/documentation/adsupport/asidentifiermanager/advertisingidentifier) (Identifier for advertisers)

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Module Advertising requires `NSUserTrackingUsageDescription` key in `info.plist`
>
> Application **will crash** if key not present
>
> Key `NSUserTrackingUsageDescription` value is set in `Edit / Project Settings / Affise / Modules`
>
> Key added automatically then module `Advertising` is selected
>
> Default value is empty string ""
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

Or Manual open `info.plist` and add key `NSUserTrackingUsageDescription` with string value. For more information [read requirements](#requirements)

#### Module Huawei

> [!NOTE] 
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Use `Android` [Huawei Module](#modules) to get OAID (Open Advertising Identifier)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

```c#
var providers = Affise.GetProviders();
var key = ProviderType.OAID;
if (providers.ContainsKey(key)) {
    var oaid = providers[key];
}
```

#### Module AppsFlyer

Send AppsFlyer event data to Affise

[AppsFlyer In-App Events Docs](https://dev.appsflyer.com/hc/docs/inappevents#send-event)

```C#
//AppsFlyer event data
const string eventName = "af_add_to_wishlist";
Dictionary<string, string> eventValues = new()
{
    {AFInAppEvents.PRICE, "1234.56"},
    {AFInAppEvents.CONTENT_ID, "1234567"}
};

// Send AppsFlyer event
AppsFlyer.sendEvent(eventName, eventValues);

// Send AppsFlyer data to Affise
Affise.Module.AppsFlyer.LogEvent(eventName, eventValues);
```

Is Module present:

```C#
Affise.Module.AppsFlyer.HasModule();
```

#### Module Link

Return last url in chan of redirection

🟥Support MAX 10 redirections🟥

```C#
Affise.Module.Link.Resolve("SITE_WITH_REDIRECTION", (redirectUrl) => {  
    // handle redirect url
});
```

Is Module present:

```C#
Affise.Module.Link.HasModule();
```

#### Module Meta

- [Meta Install Referrer Docs](https://developers.facebook.com/docs/app-ads/meta-install-referrer)
- [Google's Install Referral Docs](https://developer.android.com/google/play/installreferrer)

1. Add `queries` to your `AndroidManifest.xml`

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
    <queries>
        <package android:name="com.facebook.katana" />
        <package android:name="com.instagram.android" />
        <package android:name="com.facebook.lite" />
    </queries>

    <application>
      ...
    </application>
</manifest>
```

2. Add your `Facebook App Id` as config value `AffiseConfig.FB_APP_ID` in `Affise.settings`

```c#
Affise
    .Settings(
        affiseAppId: "Your appId", //Change to your app id
        secretKey: "Your SDK secretKey", //Change to your SDK secretKey
    )
    .setConfigValue(AffiseConfig.FB_APP_ID, "Your Facebook App Id")
    .Start(); // Start Affise SDK
```

#### Module Status

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> If `getStatus` return an error or working more than 2 minutes
>
> Please see section [validation credentials](#validate-credentials)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

```C#
Affise.Module.GetStatus(AffiseModules.Status, response => {
    // handle response
});
```

#### Module Subscription

Get products by ids:

```C#
var ids = new List<AffiseProduct> {
    "exampple.product.id_1", 
    "exampple.product.id_2",
};

Affise.Module.Subscription.FetchProducts(ids, (result) =>
{
    if (result.IsSuccess)
    {
        var value = result.AsSuccess;
        var products = value.Products;
        var invalidIds = value.InvalidIds;
    }
    else
    {
        var error = result.AsFailure;
    }
});
```

Purchase product:

```C#
// Specify product type for correct affise event
Affise.Module.Subscription.Purchase(product, AffiseProductType.CONSUMABLE, (result) =>
{
    if (result.IsSuccess)
    {
        AffisePurchasedInfo purchasedInfo = result.AsSuccess;
    }
    else
    {
        var error = result.AsFailure;
    }
});
```

Is Module present:

```C#
Affise.Module.Subscription.HasModule();
```

#### Module TikTok

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Module TikTok requires [TikTok Business Unity SDK](https://github.com/tiktok/tiktok-business-unity-sdk)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

Send TikTok event data to Affise [TikTok Docs](https://business-api.tiktok.com/gateway/docs/index?identify_key=c0138ffadd90a955c1f0670a56fe348d1d40680b3c89461e09f78ed26785164b&language=ENGLISH&doc_id=1824036608950273#item-link-3.2.2%20Content%20parameters%20for%20standard%20events)

```c#
var tiktokEvent = new TikTokAddToCartEvent("eventID");
tiktokEvent.SetDescription("Description of the product");
tiktokEvent.SetCurrency(TTCurrency.TTCurrencyUSD);
tiktokEvent.SetValue(4.99);
tiktokEvent.SetContentType("Type of the product");
tiktokEvent.SetContentId("ABC");

var eventContent = new TikTokContentParams();
eventContent.price = 4.99;
eventContent.quantity = 1;
eventContent.brand = "Brand of the product";
eventContent.contentName = "Name of the product";
TikTokContentParams[] contents = {eventContent};

tiktokEvent.SetContents(contents);

// Send TikTok event
TikTokBusinessSDK.TrackTTEvent(cartEvent);

// Prepare TikTok event data to Affise
var eventName = tiktokEvent.EventParams["eventName"];
var eventId = tiktokEvent.EventParams["eventId"];
var jsonString = tiktokEvent.EventParams["properties"];
var properties = Utils.JsonToDictionary(jsonString);
// Send TikTok event data to Affise
Affise.Module.TikTok.SendEvent(eventName, properties, eventId);
```

Is Module present:

```c#
Affise.Module.TikTok.HasModule();
```

## Build

### iOS

SDK is using Cocoapods

1. In `Build setting` select iOS platform and press `Build`
2. Select build folder (unity will exported iOS project to build folder)
3. Build folder should contain `Podfile`
4. In Terminal open build folder and run commend `pod install`
5. Open generated `*.worksapce` to build your unity project

> [!CAUTION]
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧
>
> If command `pod install` returns error `CocoaPods could not find compatible versions for pod "AffiseInternal"`
>
> Run `pod repo update` and then run `pod install` again
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧

If Podfile hasn't generated you can create it manually using this [Podfile](Editor/Resources/iOS/AffisePodfile.rb) as template

Podfile:

```rb
platform :ios, '11.0'

target 'UnityFramework' do
  pod 'AffiseInternal', '1.6.55'

  # Affise Modules
  # pod 'AffiseModule', `1.6.55`
end

target 'Unity-iPhone' do
end

use_frameworks! :linkage => :static
```

# Features

## ProviderType identifiers collection

To match users with events and data library is sending, these `ProviderType` identifiers are collected:

### Attribution

- `AFFISE_APP_ID`
- `AFFISE_PKG_APP_NAME`
- `AFF_APP_NAME_DASHBOARD`
- `APP_VERSION`
- `APP_VERSION_RAW`
- `STORE`
- `TRACKER_TOKEN`
- `TRACKER_NAME`
- `FIRST_TRACKER_TOKEN`
- `FIRST_TRACKER_NAME`
- `LAST_TRACKER_TOKEN`
- `LAST_TRACKER_NAME`
- `OUTDATED_TRACKER_TOKEN`
- `INSTALLED_TIME`
- `FIRST_OPEN_TIME`
- `INSTALLED_HOUR`
- `FIRST_OPEN_HOUR`
- `INSTALL_FIRST_EVENT`
- `INSTALL_BEGIN_TIME`
- `INSTALL_FINISH_TIME`
- `REFERRER_INSTALL_VERSION`
- `REFERRAL_TIME`
- `REFERRER_CLICK_TIME`
- `REFERRER_CLICK_TIME_SERVER`
- `REFERRER_GOOGLE_PLAY_INSTANT`
- `CREATED_TIME`
- `CREATED_TIME_MILLI`
- `CREATED_TIME_HOUR`
- `UNINSTALL_TIME`
- `REINSTALL_TIME`
- `LAST_SESSION_TIME`
- `CPU_TYPE`
- `HARDWARE_NAME`
- `DEVICE_MANUFACTURER`
- `DEEPLINK_CLICK`
- `DEVICE_ATLAS_ID`
- `AFFISE_DEVICE_ID`
- `AFFISE_ALT_DEVICE_ID`
- `REFTOKEN`
- `REFTOKENS`
- `REFERRER`
- `USER_AGENT`
- `MCCODE`
- `MNCODE`
- `REGION`
- `COUNTRY`
- `LANGUAGE`
- `DEVICE_NAME`
- `DEVICE_TYPE`
- `OS_NAME`
- `PLATFORM`
- `SDK_PLATFORM`
- `API_LEVEL_OS`
- `AFFISE_SDK_VERSION`
- `OS_VERSION`
- `RANDOM_USER_ID`
- `AFFISE_SDK_POS`
- `TIMEZONE_DEV`
- `AFFISE_EVENT_NAME`
- `AFFISE_EVENT_TOKEN`
- `LAST_TIME_SESSION`
- `TIME_SESSION`
- `AFFISE_SESSION_COUNT`
- `LIFETIME_SESSION_COUNT`
- `AFFISE_DEEPLINK`
- `AFFISE_PART_PARAM_NAME`
- `AFFISE_PART_PARAM_NAME_TOKEN`
- `AFFISE_APP_TOKEN`
- `LABEL`
- `AFFISE_SDK_SECRET_ID`
- `UUID`
- `AFFISE_APP_OPENED`
- `PUSHTOKEN`
- `PUSHTOKEN_SERVICE`
- `AFFISE_EVENTS_COUNT`
- `AFFISE_SDK_EVENTS_COUNT`
- `AFFISE_METRICS_EVENTS_COUNT`
- `AFFISE_INTERNAL_EVENTS_COUNT`
- `IS_ROOTED`
- `IS_EMULATOR`

### AdService

- `AD_SERVICE_ATTRIBUTION`

### Advertising

- `GAID_ADID`
- `GAID_ADID_MD5`
- `ADID`
- `ALTSTR_ADID`
- `FIREOS_ADID`
- `COLOROS_ADID`

### AndroidId

- `ANDROID_ID`
- `ANDROID_ID_MD5`

### Huawei

- `OAID`
- `OAID_MD5`

### Meta

- `META`

### Network

- `MAC_SHA1`
- `MAC_MD5`
- `CONNECTION_TYPE`
- `PROXY_IP_ADDRESS`

### Phone

- `NETWORK_TYPE`
- `ISP`

## Event send control

There are two ways to send events

1. Cache event to later scheduled send in batch

```c#
AddToCartEvent()
    .Send();
```

2. Send event right now

```c#
AddToCartEvent()
    .SendNow(() =>
        {
            // handle event send success
        }, (errorResponse) =>
        {
            // handle event send failed
            // 🟥Warning:🟥 event is NOT cached for later send
        }
    );
```

## Events tracking

For example, we want to track achievements. To send event first create it with
following code

```c#
class Presenter {
    void OnUnlockAchievement()
    {
        var achievement =  new JSONObject
        {
            ["achievement"] = "new level",
        };

        new UnlockAchievementEvent("best damage")
            .AddPredefinedParameter(PredefinedLong.USER_SCORE, 12552L)
            .AddPredefinedParameter(PredefinedString.ACHIEVEMENT_ID, "1334-1225-ASDZ")
            .AddPredefinedParameter(PredefinedObject.CONTENT, achievement)
            .Send();
    }
}
```

With above example you can implement other events:

- `AchieveLevel`
- `AddPaymentInfo`
- `AddToCart`
- `AddToWishlist`
- `AdRevenue`
- `ClickAdv`
- `CompleteRegistration`
- `CompleteStream`
- `CompleteTrial`
- `CompleteTutorial`
- `Contact`
- `ContentItemsView`
- `CustomizeProduct`
- `DeepLinked`
- `Donate`
- `FindLocation`
- `InitiateCheckout`
- `InitiatePurchase`
- `InitiateStream`
- `Invite`
- `LastAttributedTouch`
- `Lead`
- `ListView`
- `Login`
- `OpenedFromPushNotification`
- `Order`
- `OrderItemAdded`
- `OrderItemRemove`
- `OrderCancel`
- `OrderReturnRequest`
- `OrderReturnRequestCancel`
- `Purchase`
- `Rate`
- `ReEngage`
- `Reserve`
- `Sales`
- `Schedule`
- `Search`
- `Share`
- `SpendCredits`
- `StartRegistration`
- `StartTrial`
- `StartTutorial`
- `SubmitApplication`
- `Subscribe`
- `TravelBooking`
- `UnlockAchievement`
- `Unsubscribe`
- `Update`
- `ViewAdv`
- `ViewCart`
- `ViewContent`
- `ViewItem`
- `ViewItems`
- `InitialSubscription`
- `InitialTrial`
- `InitialOffer`
- `ConvertedTrial`
- `ConvertedOffer`
- `TrialInRetry`
- `OfferInRetry`
- `SubscriptionInRetry`
- `RenewedSubscription`
- `FailedSubscriptionFromRetry`
- `FailedOfferFromRetry`
- `FailedTrialFromRetry`
- `FailedSubscription`
- `FailedOfferise`
- `FailedTrial`
- `ReactivatedSubscription`
- `RenewedSubscriptionFromRetry`
- `ConvertedOfferFromRetry`
- `ConvertedTrialFromRetry`
- `Unsubscription`

### Custom events tracking

Use any of custom events if default doesn't fit your scenario:

- `CustomId01Event`
- `CustomId02Event`
- `CustomId03Event`
- `CustomId04Event`
- `CustomId05Event`
- `CustomId06Event`
- `CustomId07Event`
- `CustomId08Event`
- `CustomId09Event`
- `CustomId10Event`

If above event functionality still limits your usecase, you can use `UserCustomEvent`

```c#
new UserCustomEvent(eventName: "MyCustomEvent")
    .Send();  
```

## Predefined event parameters

To enrich your event with another dimension, you can use predefined parameters for most common cases.
Add it to any event:

```c#
class Presenter {
    void OnUnlockAchievement()
    {
        var achievement = new JSONObject
        {
            ["achievement"] = "new level",
        };
        
        new UnlockAchievementEvent(
            userData: "best damage"
        )
            .AddPredefinedParameter(PredefinedString.DESCRIPTION, "best damage")
            .AddPredefinedParameter(PredefinedObject.CONTENT, achievement)
            .Send();
    }
}
```

In examples above `PredefinedParameters.DESCRIPTION` and `PredefinedObject.CONTENT` is used, but many others is available:

| PredefinedParameter                           | Type                   |
|-----------------------------------------------|------------------------|
| [PredefinedString](#predefinedstring)         | string                 |
| [PredefinedLong](#predefinedlong)             | long                   |
| [PredefinedFloat](#predefinedfloat)           | float                  |
| [PredefinedObject](#predefinedobject)         | JSONObject             |
| [PredefinedListObject](#predefinedlistobject) | List&lt;JSONObject&gt; |
| [PredefinedListString](#predefinedliststring) | List&lt;string&gt;     |

### PredefinedString

- `ACHIEVEMENT_ID`
- `ADREV_AD_TYPE`
- `BRAND`
- `BRICK`
- `CAMPAIGN_ID`
- `CATALOGUE_ID`
- `CHANNEL_TYPE`
- `CITY`
- `CLASS`
- `CLICK_ID`
- `CONTENT_ID`
- `CONTENT_NAME`
- `CONTENT_TYPE`
- `CONVERSION_ID`
- `COUNTRY`
- `COUPON_CODE`
- `CURRENCY`
- `CUSTOMER_SEGMENT`
- `CUSTOMER_TYPE`
- `CUSTOMER_USER_ID`
- `DEEP_LINK`
- `DESCRIPTION`
- `DESTINATION_A`
- `DESTINATION_B`
- `DESTINATION_LIST`
- `EVENT_NAME`
- `NEW_VERSION`
- `OLD_VERSION`
- `ORDER_ID`
- `PARAM_01`
- `PARAM_02`
- `PARAM_03`
- `PARAM_04`
- `PARAM_05`
- `PARAM_06`
- `PARAM_07`
- `PARAM_08`
- `PARAM_09`
- `PARAM_10`
- `PAYMENT_INFO_AVAILABLE`
- `PID`
- `PREFERRED_NEIGHBORHOODS`
- `PRODUCT_ID`
- `PRODUCT_NAME`
- `PURCHASE_CURRENCY`
- `RECEIPT_ID`
- `REGION`
- `REGISTRATION_METHOD`
- `REVIEW_TEXT`
- `SEARCH_STRING`
- `SEGMENT`
- `STATUS`
- `SUBSCRIPTION_ID`
- `SUCCESS`
- `SUGGESTED_DESTINATIONS`
- `SUGGESTED_HOTELS`
- `TUTORIAL_ID`
- `UTM_CAMPAIGN`
- `UTM_MEDIUM`
- `UTM_SOURCE`
- `VALIDATED`
- `VERTICAL`
- `VIRTUAL_CURRENCY_NAME`
- `VOUCHER_CODE`

### PredefinedLong

- `AMOUNT`
- `DATE_A`
- `DATE_B`
- `DEPARTING_ARRIVAL_DATE`
- `DEPARTING_DEPARTURE_DATE`
- `HOTEL_SCORE`
- `LEVEL`
- `MAX_RATING_VALUE`
- `NUM_ADULTS`
- `NUM_CHILDREN`
- `NUM_INFANTS`
- `PREFERRED_NUM_STOPS`
- `PREFERRED_STAR_RATINGS`
- `QUANTITY`
- `RATING_VALUE`
- `RETURNING_ARRIVAL_DATE`
- `RETURNING_DEPARTURE_DATE`
- `SCORE`
- `TRAVEL_START`
- `TRAVEL_END`
- `USER_SCORE`
- `EVENT_START`
- `EVENT_END`

### PredefinedFloat

- `PREFERRED_PRICE_RANGE`
- `PRICE`
- `REVENUE`
- `LAT`
- `LONG`

### PredefinedObject

- `CONTENT`

### PredefinedListObject

- `CONTENT_LIST`

### PredefinedListString

- `CONTENT_IDS`

## Events buffering

Affise library will send any pending events with first opportunity,
but if there is no network connection or device is disabled, events are kept locally for 7 days before deletion.

## Advertising Identifier (google) tracking

> [!NOTE] 
>
> `Android` only
>
> Requires [Affise Advertising Module](#modules)

Advertising Identifier (google) tracking is supported automatically, no actions needed

## Open Advertising Identifier (huawei) tracking

> [!NOTE] 
>
> `Android` only
>
> Requires [Affise Huawei Module](#modules)

Open Advertising Identifier is supported automatically, no actions needed

## Push token tracking

To let affise track push token you need to receive it from your push service provider, and pass to Affise library.

```c#
Affise.AddPushToken(token, PushTokenService.FIREBASE);
```

Supported service providers:

- `APPLE` - **iOS only**
- `FIREBASE`

### Firebase Flutter Plugin

[Add Firebase to your Unity project](https://firebase.google.com/docs/unity/setup)

[Retrieve ID tokens on clients](https://firebase.google.com/docs/auth/admin/verify-id-tokens#unity)

```c#
Firebase.Auth.FirebaseUser user = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;

user.TokenAsync(true).ContinueWith(task => {
    if (task.IsCanceled) {
        Debug.LogError("TokenAsync was canceled.");
        return;
    }

    if (task.IsFaulted) {
        Debug.LogError("TokenAsync encountered an error: " + task.Exception);
        return;
    }

    string idToken = task.Result;

    Affise.AddPushToken(idToken, PushTokenService.FIREBASE);
});
```

## Links

- Register deeplink callback right after `Affise.Settings(affiseAppId,secretKey).Start()`

```c#
Affise.RegisterDeeplinkCallback((value) =>
{ 
    // full uri "scheme://host/path?parameters"
    var deeplink = value.Deeplink;

    // separated for convenience
    var scheme = value.Scheme;
    var host = value.Host;
    var path = value.Path;
    var queryParametersMap = value.Parameters;

    if(queryParametersMap["<your_uri_key>"]?.Contains("<your_uri_key_value>") == true) {
      // handle value
    }
});
```

Test Android DeepLink via terminal command:

```terminal
adb shell am start -a android.intent.action.VIEW -d "YOUR_SCHEME://YOUR_DOMAIN/somepath?param=1\&list=some\&list=other\&list="
```

Test iOS DeepLink via terminal command:

```terminal
xcrun simctl openurl booted "YOUR_SCHEME://YOUR_DOMAIN/somepath?param=1&list=some&list=other&list=1"
```

### Config

Open `Edit / Project Settings / Affise`

On `Settings` tab add links

![affise_deeplinks](https://github.com/affise/affise-mmp-sdk-unity/blob/assets/affise_deeplinks.png?raw=true)

> [!CAUTION]
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧
>
> For Android deeplink remove is manual
>
> Open Android project and remove deeplink `intent-filter` from `AndroidManifest.xml`
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧

### Deeplinks manual

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Deeplinks support only **CUSTOM** scheme **NOT** `http` or `https`
>
> For `http` or `https` read how to setup [AppLinks](#config-applink-manual)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

#### Android

To integrate deeplink support in android you need:

- Add intent filter to `AndroidManifest.xml`. For more info read [Unity docs](https://docs.unity3d.com/Manual/android-manifest.html)

- Add **custom** scheme (**NOT** `http` or `https`) and host to filter

Example: `YOUR_SCHEME://YOUR_DOMAIN`

Example: `myapp://mydomain.com`

```xml
<intent-filter android:autoVerify="true">
    <action android:name="android.intent.action.VIEW" />
    
    <category android:name="android.intent.category.DEFAULT" />
    <category android:name="android.intent.category.BROWSABLE" />
    
    <data
        android:host="YOUR_DOMAIN"
        android:scheme="YOUR_SCHEME" />
</intent-filter>
```

#### iOS

To integrate deeplink support in iOS you need:

Add key `CFBundleURLTypes` to `Info.plist` file in Xcode project folder

Example: `YOUR_SCHEME://YOUR_DOMAIN`

Example: `myapp://mydomain.com`

```xml
<key>CFBundleURLTypes</key>
<array>
    <dict>
        <key>CFBundleTypeRole</key>
        <string>Editor</string>
        <key>CFBundleURLName</key>
        <string>YOUR_DOMAIN</string>
        <key>CFBundleURLSchemes</key>
        <array>
            <string>YOUR_SCHEME</string>
        </array>
    </dict>
</array>
```

### Applinks manual

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> You must owne website domain.
>
> And has ability to add file `https://yoursite/.well-known/apple-app-site-association` for iOS support
>
> And has ability to add file `https://yoursite/.well-known/assetlinks.json` for Android support
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

#### Android

To integrate applink support in android you need:

- Add intent filter to `AndroidManifest.xml`. For more info read [Unity docs](https://docs.unity3d.com/Manual/android-manifest.html)

- Add `https` or `http` scheme and host to filter

Example: `https://YOUR_DOMAIN`

Example: `https://mydomain.com`

```xml
<intent-filter android:autoVerify="true">
    <action android:name="android.intent.action.VIEW" />
    
    <category android:name="android.intent.category.DEFAULT" />
    <category android:name="android.intent.category.BROWSABLE" />
    
    <data
        android:host="YOUR_DOMAIN"
        android:scheme="https" />
</intent-filter>
```

- Associate your app with your website. [Read Google instructions](https://developer.android.com/studio/write/app-link-indexing#associatesite) <details>
  <summary>How To Associate your app with your website</summary>

  ---

  After setting up URL support for your app, the App Links Assistant generates a Digital Assets Links file you can use to [associate your website with your app](https://developer.android.com/training/app-links/verify-android-applinks#web-assoc).

  As an alternative to using the Digital Asset Links file, you can [associate your site and app in Search Console](https://support.google.com/webmasters/answer/6212023).

  If you're using [Play App Signing](https://support.google.com/googleplay/android-developer/answer/9842756) for your app, then the certificate fingerprint produced by the App Links Assistant usually doesn't match the one on users' devices. In this case, you can find the correct Digital Asset Links JSON snippet for your app in your [Play Console](https://play.google.com/console/) developer account under **Release** > **Setup** > **App signing**.

  To associate your app and your website using the App Links Assistant, click **Open Digital Asset Links File Generator** from the App Links Assistant and follow these steps:

  ![app-links-assistant-dal-file-generator_2x](https://developer.android.com/static/studio/images/write/app-links-assistant-dal-file-generator_2x.png)
  **Figure 2**. Enter details about your site and app to generate a Digital Asset Links file.

  1. Enter your **Site domain** and your [**Application ID**](https://developer.android.com/studio/build/configure-app-module#set-application-id).

  2. To include support in your Digital Asset Links file for [One Tap sign-in](https://developers.google.com/identity/one-tap/android/overview), select **Support sharing credentials between the app and the website** and enter your site's sign-in URL.This adds the following string to your Digital Asset Links file declaring that your app and website share sign-in credentials: `delegate_permission/common.get_login_creds`.

  3. Specify the [signing config](https://developer.android.com/studio/publish/app-signing#sign-auto) or select a [keystore file](https://developer.android.com/studio/publish/app-signing#certificates-keystores).

  Make sure you select the right release config or keystore file for the release build or the debug config or keystore file for the debug build of your app. If you want to set up your production build, use the release config. If you want to test your build, use the debug config.

  4. Click **Generate Digital Asset Links file**.
  5. Once Android Studio generates the file, click **Save file** to download it.
  6. Upload the `assetlinks.json` file to your site, with read access for everyone, at `https://yoursite/.well-known/assetlinks.json`.

  > [!IMPORTANT]
  >
  > The system verifies the Digital Asset Links file via the encrypted HTTPS protocol. Make sure that the **assetlinks.json** file is accessible over an HTTPS connection, regardless of whether your app's intent filter includes **https**.

  7. Click **Link and Verify** to confirm that you've uploaded the correct Digital Asset Links file to the correct location.

  Learn more about associating your website with your app through the Digital Asset Links file in Declare website associations.

  ---

</details>

#### iOS

To integrate applink support in iOS you need:

- Follow how to set up applink in the [official documentation](https://developer.apple.com/documentation/xcode/supporting-universal-links-in-your-app).

- Associate your app with your website. [Supporting associated domains](https://developer.apple.com/documentation/xcode/supporting-associated-domains)

- [Configuring an associated domain](https://developer.apple.com/documentation/xcode/configuring-an-associated-domain/)

- Add key `com.apple.developer.associated-domains` to `Info.plist`

Example: `https://YOUR_DOMAIN`

Example: `https://mydomain.com`

```xml
<key>com.apple.developer.associated-domains</key>
<array>
    <string>applinks:YOUR_DOMAIN</string>
</array>
```

### Get deferred deeplink

> [!NOTE] 
>
> Requires [`Affise Status Module`](#modules)

Use the next public method of SDK to get deferred deeplink from server

```C#
Affise.GetDeferredDeeplink(deferredDeeplink => {
    // handle deferred deeplink
});
```

### Get deferred deeplink value

> [!NOTE] 
>
> Requires [`Affise Status Module`](#modules)

Use the next public method of SDK to get deferred deeplink value from server

```C#
Affise.GetReferrerOnServerValue(ReferrerKey.CLICK_ID, deferredDeeplinkValue => {
    // handle deferred deeplink value
});
```

## Offline mode

In some scenarios you would want to limit Affise network usage, to pause that activity call anywhere in your application following code after Affise start:

```c#
Affise.SetOfflineModeEnabled(true) // to enable offline mode
Affise.SetOfflineModeEnabled(false) // to disable offline mode
```

While offline mode is enabled, your metrics and other events are kept locally, and will be delivered once offline mode is disabled.
Offline mode is persistent as Application lifecycle, and will be disabled with process termination automatically.
To check current offline mode status call:

```c#
Affise.IsOfflineModeEnabled() // returns true or false describing current tracking state
```

## Disable tracking

To disable any tracking activity, storing events and gathering device identifiers and metrics call anywhere in your application following code after Affise start:

```c#
Affise.SetTrackingEnabled(true) // to enable tracking
Affise.SetTrackingEnabled(false) // to disable tracking
```

By default tracking is enabled.

While tracking mode is disabled, metrics and other identifiers is not generated locally.
Keep in mind that this flag is persistent until app reinstall, and don't forget to reactivate tracking when needed.
To check current status of tracking call:

```c#
Affise.IsTrackingEnabled() // returns true or false describing current tracking state
```

## Disable background tracking

To disable any background tracking activity, storing events and gathering device identifiers and metrics call anywhere in your application following code after Affise start:

```c#
Affise.SetBackgroundTrackingEnabled(true) // to enable background tracking
Affise.SetBackgroundTrackingEnabled(false) // to disable background tracking
```

By default background tracking is enabled.

While background tracking mode is disabled, metrics and other identifiers is not generated locally.
Background tracking mode is persistent as Application lifecycle, and will be re-enabled with process termination automatically.
To check current status of background tracking call:

```c#
Affise.IsBackgroundTrackingEnabled() // returns true or false describing current background tracking state
```

## Get random user Id

```c#
Affise.GetRandomUserId();
```

## Get random device Id

> [!NOTE] 
>
> To make `device id` more persistent on application reinstall
>
> use [Affise `Persistent` Module](#modules) for `iOS`
>
> use [Affise `AndroidId` Module](#modules) for `Android`

```c#
Affise.GetRandomDeviceId();
```

## Get providers

Returns providers map with [ProviderType](#providertype-identifiers-collection) as key

```c#
var providers = Affise.GetProviders();
var key = ProviderType.AFFISE_APP_TOKEN;
if (providers.ContainsKey(key)) {
    var value = providers[key];
}
```

## Is first run

```c#
Affise.IsFirstRun();
```

## Get referrer

> [!CAUTION]
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧
>
> Don't call this method directly in `Awake()` it may cause `NullReferenceException`
>
> 🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧🟧

Use the next public method of SDK to get referrer

> To get Install referrer by installing from `Android` `RuStore` include module [`RuStore`](#modules)

> To get Install referrer by installing from `Android` `AppGallery` include module [`Huawei`](#modules)

```C#
Affise.GetReferrerUrl(referrer => {
    // handle referrer
});
```

## Get referrer value

Use the next public method of SDK to get referrer value by

> To get Install referrer by installing from `Android` `RuStore` include module [`RuStore`](#modules)

> To get Install referrer by installing from `Android` `AppGallery` include module [`Huawei`](#modules)

```C#
Affise.GetReferrerUrlValue(ReferrerKey.CLICK_ID, referrer => {
    // handle referrer value
});
```

## Referrer keys

In examples above `ReferrerKey.CLICK_ID` is used, but many others is available:

- `AD_ID`
- `CAMPAIGN_ID`
- `CLICK_ID`
- `AFFISE_AD`
- `AFFISE_AD_ID`
- `AFFISE_AD_TYPE`
- `AFFISE_ADSET`
- `AFFISE_ADSET_ID`
- `AFFISE_AFFC_ID`
- `AFFISE_CHANNEL`
- `AFFISE_CLICK_LOOK_BACK`
- `AFFISE_COST_CURRENCY`
- `AFFISE_COST_MODEL`
- `AFFISE_COST_VALUE`
- `AFFISE_DEEPLINK`
- `AFFISE_KEYWORDS`
- `AFFISE_MEDIA_TYPE`
- `AFFISE_MODEL`
- `AFFISE_OS`
- `AFFISE_PARTNER`
- `AFFISE_REF`
- `AFFISE_SITE_ID`
- `AFFISE_SUB_SITE_ID`
- `AFFISE_SUB_1`
- `AFFISE_SUB_2`
- `AFFISE_SUB_3`
- `AFFISE_SUB_4`
- `AFFISE_SUB_5`
- `AFFC`
- `PID`
- `SUB_1`
- `SUB_2`
- `SUB_3`
- `SUB_4`
- `SUB_5`

## Get module state

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Required [Module Status](#module-status)
> 
> If `getStatus` return an error or working more than 2 minutes
>
> Please see section [validation credentials](#validate-credentials)
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

```C#
Affise.Module.GetStatus(AffiseModules.Status, response => {
    // handle response
});
```

## Platform specific

### StoreKit Ad Network

> `iOS Only`

For ios prior `16.1` first call

```C#
Affise.IOS.RegisterAppForAdNetworkAttribution(error =>
{
    // Handle error
});
```

Updates the fine and coarse conversion values, and calls a completion handler if the update fails.
Second argument coarseValue is available in iOS 16.1+

```C#
Affise.IOS.UpdatePostbackConversionValue(1, SKAdNetwork.CoarseConversionValue.Medium, error =>
{
    // Handle error
});
```

Configure your app to send postback copies to Affise:

Add key `NSAdvertisingAttributionReportEndpoint` to project `Info.plist`
Set key value to `https://affise-skadnetwork.com/`

```xml
<key>CFBundleURLTypes</key>
<array>
    <dict>
      <key>NSAdvertisingAttributionReportEndpoint</key>
      <string>https://affise-skadnetwork.com/</string>
    </dict>
</array>
```

# SDK to SDK integrations

```c#
// Send AdRevenue info
new AffiseAdRevenue(AffiseAdSource.ADMOB)
        .SetRevenue(2.5f, "ImpressionData_Currency")
        .SetNetwork("ImpressionData_Network")
        .SetUnit("ImpressionData_Unit")
        .SetPlacement("ImpressionData_Placement")
        .Send();
```

# Debug

## Validate credentials

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Debug methods WON'T work on Production 
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

Validate your credentials by receiving `ValidationStatus` values:

- `VALID` - your credentials are valid
- `INVALID_APP_ID` - your app id is not valid 
- `INVALID_SECRET_KEY` - your SDK secretKey is not valid
- `PACKAGE_NAME_NOT_FOUND` - your application package name not found
- `NOT_WORKING_ON_PRODUCTION` - you using debug method on production
- `NETWORK_ERROR` - network or server not available (for example `Airoplane mode` is active)

```c#
Affise
    .Settings(
        affiseAppId: "Your appId",
        secretKey: "Your SDK secretKey"
    )
    .SetProduction(false) //To enable debug methods set Production to false
    .Start(); // Start Affise SDK

Affise.Debug.Validate(status =>
{
    // Handle validation status
});
```

## Version

Get Affise flutter library version

```c#
Affise.Debug.Version()
```

## Version native

Get Affise native `Android`/`iOS` library version

```c#
Affise.Debug.VersionNative();
```

# Troubleshoots

## iOS

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> This app has crashed because Affise Advertising Module is attempted to access privacy-sensitive data without a usage description.
>
> The app's `Info.plist` must contain an `NSUserTrackingUsageDescription` key with a string value explaining
>
> to the user how the app uses this data.
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

Open `info.plist` and add key `NSUserTrackingUsageDescription` with string value. For more information [read requirements](#requirements)

## Android

> [!CAUTION]
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥
>
> Application has crashed on Android 14 (API level 34) with error
>
> `java.lang.SecurityException: One of RECEIVER_EXPORTED or RECEIVER_NOT_EXPORTED should be specified when a receiver isn't being registered exclusively for system broadcasts`
>
> Cause: Google enforced new security policy for Android 14 (API level 34).
>
> 🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥

Update `Unity` minimum version for  `2023.3.0a4`, `2022.3.14f1`, `2021.3.33f1`
[Related Unity issue](https://issuetracker.unity3d.com/issues/android-targetapi-34-crash-on-launch)

Earlier versions are not supported
