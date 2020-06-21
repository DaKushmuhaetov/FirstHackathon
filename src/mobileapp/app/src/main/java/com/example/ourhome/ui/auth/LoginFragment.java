package com.example.ourhome.ui.auth;

import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.ourhome.R;

import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;

public class LoginFragment extends Fragment {
    AuthFragment authFragment;

    public LoginFragment(AuthFragment authFragment) {
        this.authFragment = authFragment;
    }

    @BindView(R.id.signin)
    Button signin;
    @BindView(R.id.signup)
    Button signup;

    View root;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable final ViewGroup container, @Nullable Bundle savedInstanceState) {
        root = inflater.inflate(R.layout.fragment_login, container, false);
        ButterKnife.bind(this, root);

        signup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                authFragment.setRegister();
            }
        });

        return root;
    }
}
