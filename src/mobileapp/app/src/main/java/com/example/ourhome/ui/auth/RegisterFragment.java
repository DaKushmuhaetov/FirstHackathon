package com.example.ourhome.ui.auth;

import android.graphics.Color;
import android.os.Bundle;
import android.text.method.PasswordTransformationMethod;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ProgressBar;

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
import com.example.ourhome.ui.util.ProgressBarFragment;
import com.example.ourhome.ui.views.MyEditText;
import com.example.ourhome.utils.URLs;

import org.json.JSONException;
import org.json.JSONObject;

import butterknife.BindView;
import butterknife.ButterKnife;
import es.dmoral.toasty.Toasty;

public class RegisterFragment extends Fragment {
    private AuthFragment authFragment;

    public  RegisterFragment(AuthFragment authFragment) {
        this.authFragment = authFragment;
    }
    View root;

    @BindView(R.id.back)
    Button back;
    @BindView(R.id.changeHouse)
    Button changeHouse;
    @BindView(R.id.username)
    MyEditText name;
    @BindView(R.id.email)
    MyEditText email;
    @BindView(R.id.password)
    MyEditText password;
    @BindView(R.id.signup)
    Button signUp;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable final ViewGroup container, @Nullable Bundle savedInstanceState) {
        root = inflater.inflate(R.layout.fragment_register, container, false);
        ButterKnife.bind(this, root);
        password.setInputType(PasswordTransformationMethod.getInstance());
        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                authFragment.setLogin();
            }
        });
        changeHouse.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                createChangeHouseDialog(container);
            }
        });

        signUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(!checkToValidate()) return;

                String url = URLs.getRegistrationURL();
                final ProgressBarFragment fragment = ProgressBarFragment.splashLoad(getActivity(),
                        R.id.parent);
                final FragmentManager fragmentManager = getActivity().getSupportFragmentManager();

                final JSONObject data = new JSONObject();
                try {
                    data.put("name", name.getText().split(" ")[0]);
                    data.put("surname", name.getText().split(" ")[1]);
                    data.put("login", email.getText());
                    data.put("password", password.getText());
                } catch (JSONException e) {
                    alertErrorMessage("Ошибка обработки данных", true);
                    return;
                }
                RequestQueue queue = Volley.newRequestQueue(getContext());
                JsonObjectRequest request = new JsonObjectRequest(Request.Method.POST,
                        url, data,
                        new Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {

                                fragment.remove();
                                Toasty.success(getContext(), "Ваш запрос отправлен на обработку").show();
                            }
                        }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        fragment.remove();
                        Toasty.error(getContext(), "Нет соединения с сервером").show();
                    }
                });
                queue.add(request);


            }
        });

        return root;
    }


    private boolean checkToValidate() {
        boolean isValidate = true;
        MyEditText[] fields = new MyEditText[] {name, email, password};
        if(!((LoadActivity) getActivity()).checkToVoid(fields)) {
            alertErrorMessage("Не все поля заполнены", false);
            isValidate = false;
        }

        if(changeHouse.getText().equals("Выбрать дом")) {
            alertErrorMessage("Выберите дом", isValidate);
            changeHouse.setTextColor(Color.RED);
            isValidate = false;
        }
        if(name.getText().split(" ").length != 2) {
            name.setErrorMessage("Введите фамилию и имя");
            alertErrorMessage("Введите фамилию и имя", isValidate);
            isValidate = false;
        }
        return isValidate;
    }

    private void alertErrorMessage(String message, boolean isValidate) {
        if(isValidate)
            Toasty.error(getContext(), message).show();
    }


    private void createChangeHouseDialog(ViewGroup container) {
        ChangeHouseDialog dialog = new ChangeHouseDialog(changeHouse);
        FragmentManager fragmentManager = getChildFragmentManager();
        dialog.show(fragmentManager, "ChangeHouseDialog");

    }
}
