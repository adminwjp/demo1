package com.utility.jackson;

import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializerProvider;
import java.io.IOException;

public class CustomJsonEnumSerializer extends JsonSerializer<Object> {

    @Override
    public void serialize(Object value, JsonGenerator generator,
                          SerializerProvider provider) throws IOException {

        Enum<?> obj=(Enum<?>)value;
        generator.writeNumber(obj.ordinal());
        //{"ordinal":1,"name":"name"}
        //generator.writeStartObject();
        //generator.writeNumberField("ordinal", obj.ordinal());
        //generator.writeStringField("name", ((Enum<?>) value).name());
        //generator.writeEndObject();
    }
}
