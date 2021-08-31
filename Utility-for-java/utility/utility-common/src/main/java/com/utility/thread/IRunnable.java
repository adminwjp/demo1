package com.utility.thread;

/** java 8 not support   java 12  support*/
/*public interface IRunnable extends Runnable,IThread {

}*/
public interface IRunnable extends Runnable {
    public boolean isStart();

    public void setStart(boolean start);

    public void acquire();

    public void release();
}
