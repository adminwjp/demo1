package com.utility.service.dto;

public class Tuple1 <T,T1,T2> extends Tuple<T,T1> {
    public Tuple1(T item1, T1 item2,T2 item3) {
        super(item1, item2);
        this.item3=item3;
    }

    public T2 getItem3() {
        return item3;
    }


    T2 item3;
}
