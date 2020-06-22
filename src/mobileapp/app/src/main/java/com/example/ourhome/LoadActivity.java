package com.example.ourhome;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

import com.example.ourhome.data.House;
import com.example.ourhome.data.User;
import com.example.ourhome.ui.auth.AuthFragment;
import com.example.ourhome.ui.util.ProgressBarFragment;
import com.example.ourhome.ui.views.MyEditText;
import com.example.ourhome.utils.URLs;

import org.json.JSONException;
import org.json.JSONObject;

import es.dmoral.toasty.Toasty;

public class LoadActivity extends AppCompatActivity {

    private User user;
    SharedPreferences sharedPreferences;
    FragmentManager fragmentManager;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_load);

        getSupportActionBar().hide();
        sharedPreferences = getSharedPreferences("AppData", MODE_PRIVATE);

        if (sharedPreferences.contains("user")) {
            String json = sharedPreferences.getString("user", null);
            if (json == null)
                auth();
            else {
                try {
                    user = new User(new JSONObject(json));
                    finish();
                } catch (JSONException e) {
                    auth();
                    Toasty.error(this, "Нет соединения с сервером").show();
                }
            }


        }
    }

    @Override
    public void finish() {
        Intent intent = new Intent(this, MainActivity.class);
        intent.putExtra("user", user);
        startActivity(intent);
        super.finish();
    }

    public void auth() {
        fragmentManager = getSupportFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.add(R.id.authContainer, new AuthFragment());
        transaction.commit();
    }

    public void setHouse(House house) {
        URLs.setHouseID(house.getGuid());
    }
    public static boolean checkToVoid(MyEditText[] fields) {
        boolean isValidate = true;
        for(MyEditText field : fields) {
            if(field.getText().isEmpty()) {
                field.setErrorMessage("Пусто");
                isValidate = false;
            }
            else {
                field.allRight();
            }
        }
        return isValidate;
    }

    public void setUser(User user) {
        this.user = user;
        SharedPreferences.Editor editor = sharedPreferences.edit();
        editor.putString("user", user.getJsonString());
        editor.apply();
        finish();
    }
}
