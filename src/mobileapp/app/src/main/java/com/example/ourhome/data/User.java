package com.example.ourhome.data;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;

public class User implements Serializable {
    String token;
    String jsonString;

    public User(JSONObject response) throws JSONException {
        token = response.getString("access_token");
        jsonString = response.toString();
    }


    public void setToken(String token) {
        this.token = "Bearer "+token;
    }

    public String getToken() {
        return token;
    }

    public String getJsonString() {
        return jsonString;
    }
}
