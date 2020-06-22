package com.example.ourhome.utils;

import android.util.Log;

import com.android.volley.toolbox.StringRequest;

public class URLs {
    private static final String SERVER_URL = "http://192.168.1.7";

    private static final String HOUSES = "/houses";
    private static final String SLASH = "/";
    private static final String HOUSE = "/house";
    private static final String PERSON = "/person";
    private static final String LOGIN = "/login";
    private static String houseID;

    public static String getHousesURL() {
        return SERVER_URL+HOUSES;
    }
    public static String getRegistrationURL() {
        return SERVER_URL+HOUSE+houseID+PERSON;
    }
    public static String getLoginURL() {
        return SERVER_URL+PERSON+LOGIN;
    }



    public static void setHouseID(String houseID) {
        URLs.houseID = SLASH + houseID;
    }
}
