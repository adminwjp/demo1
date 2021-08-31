package com.utility.thread;

public  interface   IThread{
    public boolean isStart();

    public void setStart(boolean start);

    public void acquire();

    public void release();

}
