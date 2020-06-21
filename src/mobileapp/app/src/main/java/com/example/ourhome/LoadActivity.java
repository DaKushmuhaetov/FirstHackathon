package com.example.ourhome;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.os.Bundle;

import com.example.ourhome.data.House;
import com.example.ourhome.ui.auth.AuthFragment;
import com.example.ourhome.utils.URLs;

public class LoadActivity extends AppCompatActivity {

    public House myHouse;
    FragmentManager fragmentManager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_load);

        getSupportActionBar().hide();

        fragmentManager = getSupportFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.add(R.id.authContainer, new AuthFragment());
        transaction.commit();
    }

    public void setHouse(House house) {
        myHouse = house;
        URLs.setHouseID(house.getGuid());
    }

}
