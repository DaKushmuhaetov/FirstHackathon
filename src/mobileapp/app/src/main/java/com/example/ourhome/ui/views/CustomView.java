package com.example.ourhome.ui.views;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.FrameLayout;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import com.example.ourhome.R;

public abstract class CustomView extends FrameLayout implements ICustomView {

    protected  Context context;

    public CustomView(@NonNull Context context) {
        super(context);
        System.out.println("0");
        initView();

    }

    public CustomView(@NonNull Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        this.context = context;
        handleAttributes(attrs);
        initView();
    }

    public CustomView(@NonNull Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
        System.out.println("2");
        initView();
    }

}
