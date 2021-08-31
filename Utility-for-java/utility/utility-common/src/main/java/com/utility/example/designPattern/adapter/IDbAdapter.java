package com.utility.example.designPattern.adapter;

import com.utility.service.dto.Tuple;

public interface IDbAdapter {
    Tuple<Integer,Object> operator(String sql, String[] params);
}
