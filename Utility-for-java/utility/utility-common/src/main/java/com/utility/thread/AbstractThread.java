package com.utility.thread;

public abstract class    AbstractThread extends Thread implements IRunnable {

    public  abstract void  run();

    public abstract boolean isStart();

    public abstract void setStart(boolean start);

    public abstract void acquire();

    public abstract void release();

}
