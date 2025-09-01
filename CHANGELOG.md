# Changelog

## [1.6.45] - 2025-09-01

### Changed

- Update native Android to [`1.6.64`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.64/CHANGELOG.md)

## [1.6.44] - 2025-08-20

### Added

- New module `TikTok`.
- Api `Affise.Module.TikTok.SendEvent`.

### Changed

- Update native iOS to [`1.6.54`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.54/CHANGELOG.md)
- Update native Android to [`1.6.62`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.62/CHANGELOG.md)

## [1.6.43] - 2025-07-31

### Fixed

- Fix crash for android API < 24 

### Changed

- Update native Android to [`1.6.61`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.61/CHANGELOG.md)

## [1.6.42] - 2025-07-25

### Added

- New module `AdService` for `iOS`
- Provider `AD_SERVICE_ATTRIBUTION`

### Changed

- Update native iOS to [`1.6.53`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.53/CHANGELOG.md)

## [1.6.41] - 2025-07-21

### Added

- Provider `PUSHTOKEN_SERVICE`

### Changed

- Update native iOS to [`1.6.52`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.52/CHANGELOG.md)
- Update native Android to [`1.6.60`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.60/CHANGELOG.md)

## [1.6.40] - 2025-06-27

### Added

- Api `Affise.Module.AppsFlyer.HasModule`
- Api `Affise.Module.Link.HasModule`
- Api `Affise.Module.Subscription.HasModule`

### Changed

- Update native iOS to [`1.6.51`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.51/CHANGELOG.md)
- Update native Android to [`1.6.59`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.59/CHANGELOG.md)

## [1.6.39] - 2025-05-30

### Added

- New module `AppsFlyer`
- Api `Affise.Module.AppsFlyer.LogEvent`

### Changed

- Api `Affise.Module.LinkResolve` moved to `Affise.Module.Link.Resolve`
- Api `Affise.Module.FetchProducts` moved to `Affise.Module.Subscription.FetchProducts`
- Api `Affise.Module.Purchase` moved to `Affise.Module.Subscription.Purchase`
- Update native iOS to [`1.6.50`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.50/CHANGELOG.md)
- Update native Android to [`1.6.58`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.58/CHANGELOG.md)
- 
## [1.6.38] - 2025-05-07

### Added

- New module `meta` for `Facebook`
- Provider for `Facebook` install referrer

### Changed

- Update native Android to [`1.6.57`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.57/CHANGELOG.md)

## [1.6.37] - 2025-04-25

### Fixed

- Fix `Affise.settings.setOnInitSuccess` false positive error
- Fix `Affise.settings.setOnInitError` false positive error

## [1.6.36] - 2025-04-17

### Removed

- Remove code support for `iOS` version lesser then `12`

### Changed

- Update `android` provider `OAID` moved to module `huawei`
- Update `android` provider `OAID_MD5` moved to module `huawei`
- Update `android` provider `OAID` to use `com.huawei.hms:ads-identifier`
- Update `iOS` EventsManager scheduler
- Update native iOS to [`1.6.49`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.49/CHANGELOG.md)
- Update native Android to [`1.6.56`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.56/CHANGELOG.md)

## [1.6.35] - 2025-03-31

### Added

- Api `Affise.Settings.SetOnInitSuccess`
- Api `Affise.Settings.SetOnInitError`
- Api `Affise.Debug.Version`
- Api `Affise.Debug.VersionNative`

### Changed

- Update native iOS to [`1.6.48`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.48/CHANGELOG.md)
- Update native Android to [`1.6.55`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.55/CHANGELOG.md)
  
## [1.6.34] - 2025-03-18

### Fixed

- Fix `GetDeferredDeeplink`
- Fix `GetDeferredDeeplinkValue`

## [1.6.33] - 2025-03-17

### Changed

- Api `Affise.GetReferrerOnServer` moved to `Affise.GetDeferredDeeplink`
- Api `Affise.GetReferrerOnServerValue` moved to `Affise.GetDeferredDeeplinkValue`
- Update native iOS to [`1.6.47`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.47/CHANGELOG.md)
- Update native Android to [`1.6.54`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.54/CHANGELOG.md)

## [1.6.32] - 2025-03-07

### Added

- New module `Persistent` for `iOS`
- Persistent `AFFISE_DEVICE_ID`

### Changed

- Update native iOS to [`1.6.45`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.45/CHANGELOG.md)
- Update native Android to [`1.6.53`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.53/CHANGELOG.md)

## [1.6.31] - 2025-02-14

### Added

- New event index `affise_event_id_index`
- New postback index `uuid_index`

### Fixed

- Fix `ProviderType.INSTALL_FIRST_EVENT`

### Changed

- Update native iOS to [`1.6.43`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.43/CHANGELOG.md)
- Update native Android to [`1.6.52`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.52/CHANGELOG.md)

## [1.6.30] - 2025-01-29

### Added

- Api `Affise.GetReferrerOnServer`
- Api `Affise.GetReferrerOnServerValue`

### Changed

- Update native Android to [`1.6.51`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.51/CHANGELOG.md)

## [1.6.29] - 2024-12-20

### Changed

- Affise modules improvements

## [1.6.28] - 2024-12-13

### Added

- New module `Huawei` for `AppGallery` install referrer

### Changed

- Update native Android to [`1.6.50`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.50/CHANGELOG.md)

## [1.6.27] - 2024-11-26

### Added

- New module `RuStore`

### Fixed

- Fix event writing to storage exception

### Changed

- Update native Android to [`1.6.49`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.49/CHANGELOG.md)

## [1.6.26] - 2024-10-22

### Changed

- Update native Android to [`1.6.47`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.47/CHANGELOG.md)

### Removed

- Api `SetEnabledMetrics`
- Api `SetAutoCatchingTypes`

## [1.6.25] - 2024-10-15

### Fixed

- Crossplatform api call thread switch

## [1.6.24] - 2024-10-07

### Fixed

- Fix `Affise.Module.GetStatus` timing retry

### Changed

- Update native iOS to [`1.6.42`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.42/CHANGELOG.md)
- Update native Android to [`1.6.45`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.45/CHANGELOG.md)
  
## [1.6.23] - 2024-10-02

### Added

- Module `Subscription` StoreKit2 support

### Changed

- Update native iOS to [`1.6.41`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.41/CHANGELOG.md)

## [1.6.22] - 2024-09-19

### Added

- Module `Subscription` support

### Fixed

- Api callback executes on other thread

### Changed

- Update native iOS to [`1.6.40`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.40/CHANGELOG.md)
- Update native Android to [`1.6.44`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.44/CHANGELOG.md)

## [1.6.21] - 2024-08-21

### Added

- iOS only api `Affise.IOS.GetReferrerOnServer`
- iOS only api `Affise.IOS.GetReferrerOnServerValue`

### Changed

- Api `Affise.GetReferrer` to `Affise.GetReferrerUrl`
- Api `Affise.GetReferrerValue` to `Affise.GetReferrerUrlValue`
- Update native iOS to [`1.6.39`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.39/CHANGELOG.md)
- Update native Android to [`1.6.42`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.42/CHANGELOG.md)

## [1.6.20] - 2024-07-31

### Added

- Modules compatibility check

### Fixed

- Api `Affise.Module.GetStatus`

### Changed

- Update native iOS to [`1.6.36`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.36/CHANGELOG.md)
- Update native Android to [`1.6.40`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.40/CHANGELOG.md)

## [1.6.19] - 2024-07-17

### Added

- Module `Link`
- Module `AndroidId`

### Changed

- Update native iOS to [`1.6.33`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.33/CHANGELOG.md)
- Update native Android to [`1.6.38`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.38/CHANGELOG.md)
- Update `RegisterDeeplinkCallback` change uri to convenient values
- Api `Affise.GetStatus` moved to `Affise.Module.GetStatus`
- Api `Affise.ModuleStart` moved to `Affise.Module.ModuleStart`
- Api `Affise.GetModulesInstalled` moved to `Affise.Module.GetModulesInstalled`

## [1.6.18] - 2024-06-06

### Added

- Event api `SendNow`
- Affise `Internal` library group callback support

### Removed

- Api `SendEvents`

### Changed

- Update native iOS to [`1.6.32`](https://github.com/affise/affise-mmp-sdk-ios/blob/1.6.32/CHANGELOG.md)
- Update native Android to [`1.6.34`](https://github.com/affise/affise-mmp-sdk-android/blob/v1.6.34/CHANGELOG.md)

## [1.6.17] - 2024-03-14

### Added

- Api `IsFirstRun`

### Changed

- Update native iOS to `1.6.27`
- Update native Android to `1.6.26`

## [1.6.16] - 2024-02-05

### Changed

- Update native iOS to `1.6.25`

## [1.6.15] - 2024-02-01

### Added

- Settings `Deeplink`

### Changed

- Update native iOS to `1.6.24`
- Update native Android to `1.6.23`

## [1.6.14] - 2023-12-29

### Added

- Settings `Modules`
- Api `GetModulesInstalled`
- New values in `ReferrerKey`

## Deprecated

- Api `SendEvents`

### Changed

- Update native iOS to `1.6.20`
- Update native Android to `1.6.22`

[1.6.45]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.44...1.6.45
[1.6.44]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.43...1.6.44
[1.6.43]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.42...1.6.43
[1.6.42]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.41...1.6.42
[1.6.41]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.40...1.6.41
[1.6.40]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.39...1.6.40
[1.6.39]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.38...1.6.39
[1.6.38]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.37...1.6.38
[1.6.37]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.36...1.6.37
[1.6.36]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.35...1.6.36
[1.6.35]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.34...1.6.35
[1.6.34]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.33...1.6.34
[1.6.33]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.32...1.6.33
[1.6.32]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.31...1.6.32
[1.6.31]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.30...1.6.31
[1.6.30]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.29...1.6.30
[1.6.29]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.28...1.6.29
[1.6.28]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.27...1.6.28
[1.6.27]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.26...1.6.27
[1.6.26]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.25...1.6.26
[1.6.25]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.24...1.6.25
[1.6.24]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.23...1.6.24
[1.6.23]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.22...1.6.23
[1.6.22]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.21...1.6.22
[1.6.21]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.20...1.6.21
[1.6.20]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.19...1.6.20
[1.6.19]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.18...1.6.19
[1.6.18]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.17...1.6.18
[1.6.17]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.16...1.6.17
[1.6.16]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.15...1.6.16
[1.6.15]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.14...1.6.15
[1.6.14]: https://github.com/affise/affise-mmp-sdk-unity/compare/1.6.13...1.6.14
