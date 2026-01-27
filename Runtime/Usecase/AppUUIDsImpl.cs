#nullable enable

using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Exceptions;
using AffiseAttributionLib.Utils;

namespace AffiseAttributionLib.Usecase
{
    internal class AppUUIDsImpl : IAppUUIDs
    {
        
        private readonly IConverter<string, string> _md5Converter;
        public AppUUIDsImpl(
            IConverter<string, string> md5Converter
        )
        {
            _md5Converter = md5Converter;
        }

        public string? GetAffiseDeviseId()
        {
            var prefDeviseId =  PrefUtils.GetString(IAppUUIDs.AFF_DEVICE_ID, null);
            if (!string.IsNullOrWhiteSpace(prefDeviseId))
            {
                return prefDeviseId;
            }

            var genUuid = Uuid.Generate().Sign(Uuid.SignType.RANDOM);
            PrefUtils.SaveString(IAppUUIDs.AFF_DEVICE_ID, genUuid);
            return PrefUtils.GetString(IAppUUIDs.AFF_DEVICE_ID, AffiseError.UUID_NO_VALID_METHOD);
        }

        public string? GetAffiseAltDeviseId()
        {
            var prefAltDeviseId =  PrefUtils.GetString(IAppUUIDs.AFF_ALT_DEVICE_ID, null);
            if (!string.IsNullOrWhiteSpace(prefAltDeviseId))
            {
                return prefAltDeviseId;
            }

            var genUuid = Uuid.Generate().Sign(Uuid.SignType.RANDOM);
            PrefUtils.SaveString(IAppUUIDs.AFF_ALT_DEVICE_ID, genUuid);
            return PrefUtils.GetString(IAppUUIDs.AFF_ALT_DEVICE_ID, AffiseError.UUID_NO_VALID_METHOD);
        }

        public string? GetRandomUserId()
        {
            var prefUserId =  PrefUtils.GetString(IAppUUIDs.RANDOM_USER_ID, null);
            if (!string.IsNullOrWhiteSpace(prefUserId))
            {
                return prefUserId;
            }

            var affUserId = _md5Converter.Convert(PrefUtils.GetLong(FirstAppOpenUseCase.FIRST_OPENED_DATE_KEY).ToString())
                .ToFakeUuid()
                .Sign(Uuid.SignType.INSTALL_TIME);
            if (!string.IsNullOrWhiteSpace(affUserId))
            {
                return affUserId;
            }

            var genUuid = Uuid.Generate().Sign(Uuid.SignType.RANDOM);
            PrefUtils.SaveString(IAppUUIDs.RANDOM_USER_ID, genUuid);
            return PrefUtils.GetString(IAppUUIDs.RANDOM_USER_ID, AffiseError.UUID_NO_VALID_METHOD);
        }
    }
}