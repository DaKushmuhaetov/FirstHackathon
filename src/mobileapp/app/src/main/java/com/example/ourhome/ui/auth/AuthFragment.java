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
        adminLoginFragment = new AdminLoginFragment(this);
        setLogin();

        return root;
    }
    private LoginFragment loginFragment;
    private RegisterFragment registerFragment;
    private AdminLoginFragment adminLoginFragment;

    public void setLogin() {
        changeFragment(loginFragment);

    }
    public void setRegister() {
        changeFragment(registerFragment);

    }
    public void setAdminLogin() {
        changeFragment(adminLoginFragment);
    }
    public void changeFragment(Fragment fragment) {
        FragmentTransaction transaction = getChildFragmentManager().beginTransaction();
        transaction.replace(R.id.authContainer, fragment);
        transaction.commit();
    }

}
