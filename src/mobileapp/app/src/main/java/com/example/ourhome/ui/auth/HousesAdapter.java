package com.example.ourhome.ui.auth;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import com.example.ourhome.LoadActivity;
import com.example.ourhome.R;
import com.example.ourhome.data.House;

import java.util.List;

public class HousesAdapter extends BaseAdapter {
    List<House> list;
    LayoutInflater inflater;
    ChangeHouseDialog parentDialog;
    LoadActivity activity;
    public HousesAdapter(List<House> list, Context context, ChangeHouseDialog parent, LoadActivity activity) {
        this.list = list;
        inflater = LayoutInflater.from(context);
        this.parentDialog = parent;
        this.activity = activity;
    }

    @Override
    public int getCount() {
        return list.size();
    }

    @Override
    public Object getItem(int position) {
        return list.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        final House item = (House) getItem(position);
        if(convertView == null) {
            convertView = inflater.inflate(R.layout.adapter_house, parent, false);
        }
        TextView name = convertView.findViewById(R.id.name);
        TextView count = convertView.findViewById(R.id.counter);
        name.setText(item.getAddress());
        count.setText(String.valueOf(item.getPeopleCount()));
        convertView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                parentDialog.getOutput().setText(item.getAddress());
                parentDialog.dismiss();
                parentDialog.getOutput().setTextColor(Color.BLACK);
                activity.setHouse(item);
            }
        });
        //System.out.println(item.getPeopleCount());
        return convertView;
    }

    public void setList(List<House> list) {
        this.list = list;
    }
}
