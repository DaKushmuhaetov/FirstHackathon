package com.example.ourhome.ui.auth;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.DialogFragment;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.ourhome.LoadActivity;
import com.example.ourhome.R;
import com.example.ourhome.data.House;
import com.example.ourhome.utils.URLs;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class ChangeHouseDialog extends DialogFragment implements View.OnClickListener {

    HousesAdapter adapter;

    public Button getOutput() {
        return output;
    }

    private Button output;

    public ChangeHouseDialog(Button output) {
        this.output = output;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.dialog_change_house, container, false);
        adapter = new HousesAdapter(new ArrayList<House>(), getContext(), this, (LoadActivity) getActivity());

        ListView listView = root.findViewById(R.id.list);
        listView.setAdapter(adapter);

        final ProgressBar progressBar = root.findViewById(R.id.progressBar);

        RequestQueue queue = Volley.newRequestQueue(getContext());
        StringRequest stringRequest = new StringRequest(Request.Method.GET, "http://192.168.1.7/houses",
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        List<House> houseArrayList = new ArrayList<>();
                        try {
                            JSONObject jsonObject = new JSONObject(response);
                            JSONArray jsonArray = jsonObject.getJSONArray("items");
                            for (int i = 0; i < jsonArray.length(); i++) {
                                houseArrayList.add(new House(jsonArray.getJSONObject(i)));
                            }
                            progressBar.setVisibility(View.INVISIBLE);
                            adapter.setList(houseArrayList);
                            System.out.println(houseArrayList.size());
                            adapter.notifyDataSetChanged();
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                System.out.println(error);
            }
        });

        queue.add(stringRequest);

        return root;
    }

    @Override
    public void onClick(View v) {

    }
}