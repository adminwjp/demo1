package com.utility.jackson;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.core.JsonStreamContext;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonDeserializer;

import java.io.IOException;

public class CustomJsonEnumDeSerializer extends JsonDeserializer<Enum<?>> {

    /**
     * 反序列化mvn的处理
     */
    @Override
    public Enum<?> deserialize(JsonParser jp, DeserializationContext ctxt) throws IOException {
        JsonStreamContext parent = jp.getParsingContext().getParent();
        Object currentValue = parent.getCurrentValue();
        String currentName = parent.getCurrentName();
        //JsonNode node = jp.getCodec().readTree(jp);
        //error
        //Class clazz = BeanUtils.findPropertyType(currentName, currentValue.getClass());
       // return (Enum<?>) clazz.getEnumConstants()[Integer.parseInt(currentName)];
        return (Enum<?>) currentValue;
    }
}
