package com.affise.attribution.unity;


import com.affise.attribution.internal.AffiseApiMethod;
import com.affise.attribution.internal.callback.InternalResult;
import com.affise.attribution.internal.utils.JSONObjectExtKt;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

public abstract class NativeCallHandler {

    public <T> T apiCall(String apiName, String json) throws Exception {
        ApiResult result = new ApiResult();
        JSONObject jsonObject = new JSONObject(json);

        apiCall(
                AffiseApiMethod.from(apiName),
                JSONObjectExtKt.toMap(jsonObject),
                result
        );

        if (result.isNotImplemented()) {
            throw new Exception(String.format("AffiseApi [%s] error: not implemented", apiName));
        }

        if (result.isError()) {
            throw new Exception(String.format("AffiseApi [%s] error: [%s]", apiName, result.getError()));
        }

        try {
            return asNativeData(result);
        } catch (Exception e) {
            throw new Exception(String.format("AffiseApi [%s] error: [%s]", apiName, e.getLocalizedMessage()));
        }
    }
    
    public int apiCallBool(String apiName, String json) throws Exception {
        Boolean value = apiCall(apiName, json);
        return value ? 1 : 0;
    }
    
    @SuppressWarnings({"unchecked", "rawtypes"})
    private <T> T asNativeData(ApiResult result) {
        Object data = result.getResult();

        if (data instanceof Map) {
            JSONObject json = new JSONObject((Map)data);
            // return as JSON string to parse in C# on Unity side
            return (T) json.toString();
        } else if (data instanceof List) {
            JSONArray json = new JSONArray((List)data);
            // return as JSON string to parse in C# on Unity side
            return (T) json.toString();
        }

        return (T) data;
    }

    public void apiCallVoid(String apiName, String json) throws Exception {
        apiCall(apiName, json);
    }

    public abstract void apiCall(AffiseApiMethod api, Map<String, ?> data, InternalResult result);
}
