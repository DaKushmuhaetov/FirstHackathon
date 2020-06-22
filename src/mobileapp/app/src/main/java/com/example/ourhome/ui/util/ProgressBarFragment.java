package com.example.ourhome.ui.util;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.example.ourhome.R;

public class ProgressBarFragment extends Fragment {
    FragmentManager fragmentManager;

    public ProgressBarFragment(FragmentManager fragmentManager) {
        this.fragmentManager = fragmentManager;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_load, container, false);
        return root;
    }
    public static ProgressBarFragment splashLoad(FragmentActivity activity, int layout) {
        FragmentManager fragmentManager = activity.getSupportFragmentManager();
        ProgressBarFragment progressBarFragment = new ProgressBarFragment(fragmentManager);
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.add(layout, progressBarFragment);
        transaction.commit();
        return progressBarFragment;
    }
    public void remove() {
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.remove(this);
        transaction.commit();
    }
}
