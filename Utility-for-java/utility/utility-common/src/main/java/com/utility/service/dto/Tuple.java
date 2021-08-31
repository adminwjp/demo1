package com.utility.service.dto;

public class Tuple<T,T1> {
    public T getItem1() {
        return item1;
    }

    public T1 getItem2() {
        return item2;
    }

    public Tuple(T item1, T1 item2) {
        this.item1 = item1;
        this.item2 = item2;
    }

    T item1;
    T1 item2;
}

