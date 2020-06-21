package com.example.ourhome.data;

import org.json.JSONException;
import org.json.JSONObject;

public class House {
    String guid, address;
    int peopleCount;

    public House(JSONObject data) {
        try {
            this.guid = data.getString("id");
            this.address = data.getString("address");
            this.peopleCount = data.getInt("livesHereCounter");
        } catch (JSONException e) {
            e.printStackTrace();
        }

    }

    public String getAddress() {
        return address;
    }

    public int getPeopleCount() {
        return peopleCount;
    }

    public String getGuid() {
        return guid;
    }
}
