package com.example.ourhome.ui.views;

import android.content.Context;
import android.content.res.Resources;
import android.content.res.TypedArray;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.text.method.PasswordTransformationMethod;
import android.text.method.TransformationMethod;
import android.util.AttributeSet;
import android.view.View;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.ourhome.R;

public class MyEditText extends CustomView{
    public MyEditText(@NonNull Context context) {
        super(context);
    }

    public MyEditText(@NonNull Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
    }

    public MyEditText(@NonNull Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }
    public enum State {
        base,
        error
    }
    TypedArray attributes;
    public void handleAttributes(@Nullable AttributeSet attrs) {
        attributes = context.getTheme().obtainStyledAttributes(
                attrs,
                R.styleable.templateEditText,
                0, 0);
    }
    State state;
    View root;
    TextView name, error;
    TextView text;
    EditText textET;
    FrameLayout frame;

    @Override
    public void initView() {
        root = inflate(getContext(), R.layout.view_edit_text, this);
        text = findViewById(R.id.text);
        name = root.findViewById(R.id.name);
        textET = root.findViewById(R.id.text);
        error = root.findViewById(R.id.error);
        error.setVisibility(View.INVISIBLE);
        frame = root.findViewById(R.id.frame);
        try {
            name.setText(attributes.getString(R.styleable.templateEditText_nameTitle));
            error.setText(attributes.getString(R.styleable.templateEditText_errorMessage));
        } finally {
            attributes.recycle();
        }
        text.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                setState(State.base);
            }

            @Override
            public void afterTextChanged(Editable s) {

            }
        });
    }

    public void setInputType(int type) {
        textET.setInputType(type);
    }
    public void setInputType(TransformationMethod method) {
        textET.setTransformationMethod(method);
    }
    protected void setState(State state) {
        this.state=state;
        switch (state) {
            case base:
                text.setTextColor(getResources().getColor(R.color.colorPrimary));
                error.setVisibility(View.INVISIBLE);
                frame.setBackground(getResources().getDrawable(R.drawable.view_edit_text_primary));
                break;
            case error:
                text.setTextColor(getResources().getColor(R.color.colorError));
                error.setVisibility(View.VISIBLE);
                frame.setBackground(getResources().getDrawable(R.drawable.view_edit_text_error));
                break;
        }

    }
    public void setErrorMessage(String message) {
        setState(State.error);
        error.setText(message);
    }
    public void allRight() {
        setState(State.base);
    }

    public String getText() {
        return text.getText().toString();
    }
}
