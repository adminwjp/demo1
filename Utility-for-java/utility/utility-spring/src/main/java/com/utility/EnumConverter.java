package com.utility;

import org.springframework.core.convert.converter.Converter;

public class EnumConverter<T extends Enum> implements Converter<String, T> {
    private Class<T> enumType;

    public EnumConverter(Class<T> enumType) {
        this.enumType = enumType;
    }

    @Override
    public T convert(String source) {
        if (source.length() == 0) {
            return null;
        }
        return (T) enumType.getEnumConstants()[Integer.parseInt(source)];
    }


}
