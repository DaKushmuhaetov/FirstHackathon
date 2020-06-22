package com.example.ourhome.ui.auth;

import android.os.Bundle;
import android.text.InputType;
import android.text.method.PasswordTransformationMethod;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.example.ourhome.LoadActivity;
import com.example.ourhome.R;
import com.example.ourhome.data.User;
import com.example.ourhome.ui.util.ProgressBarFragment;
import com.example.ourhome.ui.views.MyEditText;
import com.example.ourhome.utils.URLs;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;
import es.dmoral.toasty.Toasty;

public class LoginFragment extends Fragment {
    AuthFragment authFragment;

    public LoginFragment(AuthFragment authFragment) {
        this.authFragment = authFragment;
    }

    @BindView(R.id.signin)
    Button signin;
    @BindView(R.id.signup)
    Button signup;
    @BindView(R.id.email)
    MyEditText email;
    @BindView(R.id.password)
    MyEditText password;
    @BindView(R.id.signinAdmin)
    Button signinAdmin;

    View root;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable final ViewGroup container, @Nullable Bundle savedInstanceState) {
        root = inflater.inflate(R.layout.fragment_login, container, false);
        ButterKnife.bind(this, root);
        email.setInputType(InputType.TYPE_TEXT_VARIATION_EMAIL_ADDRESS);
        password.setInputType(PasswordTransformationMethod.getInstance());

        signinAdmin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                authFragment.setAdminLogin();
            }
        });
        signup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                authFragment.setRegister();
            }
        });

        signin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(!((LoadActivity)getActivity()).checkToVoid(new MyEditText[] {email, password})) {
                    Toasty.error(getContext(), "Не все поля заполнены").show();
                    return;
                }
                String url = URLs.getLoginURL();
                final ProgressBarFragment fragment = ProgressBarFragment.splashLoad(getActivity(),
                        R.id.parent);

                final JSONObject data = new JSONObject();
                try {
                    data.put("login", email.getText());
                    data.put("password", password.getText());
                } catch (JSONException e) {
                    Toasty.error(getContext(), "Ошибка обработки данных").show();
                    return;
                }
                RequestQueue queue = Volley.newRequestQueue(getContext());
                JsonObjectRequest request = new JsonObjectRequest(Request.Method.POST,
                        url, data,
                        new Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {

                                fragment.remove();
                                Toasty.success(getContext(), "Вы успешно вошли").show();
                                try {
                                    ((LoadActivity) getActivity()).setUser(new User(response));
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }

                            }
                        }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Log.e("RequestError", error.getMessage());
                        fragment.remove();
                        Toasty.error(getContext(), "Неправильное имя пользователя или пароль").show();
                    }
                });
                queue.add(request);
            }
        });

        return root;
    }
}
