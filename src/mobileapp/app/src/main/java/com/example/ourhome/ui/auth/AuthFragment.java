package com.example.ourhome.ui.auth;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.constraintlayout.widget.ConstraintSet;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import com.example.ourhome.R;

public class AuthFragment extends Fragment {

    View root;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        root = inflater.inflate(R.layout.fragment_auth, container, false);

        loginFragment = new LoginFragment(this);
        registerFragment = new RegisterFragment(this);
        setLogin();

        return root;
    }
    private LoginFragment loginFragment;
    private RegisterFragment registerFragment;

    public void setLogin() {
        System.out.println("AuthCrated");
        FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
        transaction.replace(R.id.authContainer, loginFragment);
        transaction.commit();
    }
    public void setRegister() {
        FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
        transaction.remove(loginFragment);
        transaction.add(R.id.authContainer, registerFragment);
        transaction.commit();
    }

}
