package com.utility.jackson;

import com.fasterxml.jackson.databind.PropertyNamingStrategy;
import com.utility.util.StringUtil;

public class MyCamemlToUnderlineCaseStrategy  extends PropertyNamingStrategy.PropertyNamingStrategyBase {
    @Override
    public String translate(String propertyName) {
        String name= StringUtil.parse(propertyName, StringUtil.StringFormat.InitialLetterUpperCaseLower);
        return name;
    }
}
