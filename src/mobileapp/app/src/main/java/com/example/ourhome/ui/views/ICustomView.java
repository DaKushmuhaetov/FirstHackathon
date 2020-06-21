package com.example.ourhome.ui.views;

import android.util.AttributeSet;
import android.view.View;

import androidx.annotation.Nullable;

public interface ICustomView {

    void initView();
    void handleAttributes(@Nullable AttributeSet attrs);
}
