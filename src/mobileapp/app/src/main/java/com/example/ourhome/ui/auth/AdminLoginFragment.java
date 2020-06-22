package com.example.ourhome.ui.auth;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.ourhome.R;

import butterknife.BindView;
import butterknife.ButterKnife;

public class AdminLoginFragment extends Fragment {
    AuthFragment parent;
    public AdminLoginFragment(AuthFragment parent) {
        this.parent = parent;
    }

    @BindView(R.id.back)
    Button back;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_admin_login, container, false);
        ButterKnife.bind(this, root);

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                parent.setLogin();
            }
        });

        return root;
    }
}
