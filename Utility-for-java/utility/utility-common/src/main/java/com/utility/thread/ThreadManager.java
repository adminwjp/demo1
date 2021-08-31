package com.utility.thread;

import java.util.HashSet;
import java.util.Set;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Semaphore;

public class ThreadManager {
    private final ExecutorService executorService = Executors.newCachedThreadPool();
    //当前在多线程环境下被扩放使用，操作系统的信号量是个很重要的概念
    private  Semaphore semaphore;
    private final Set<IRunnable> runnables=new HashSet<>();
    public ThreadManager(){
        this(5);
    }
    public  ThreadManager(final  int permits){
        semaphore= new Semaphore(permits);
    }
    public <T extends IRunnable>  boolean  add(T runnable){
        return  add(runnable,false);
    }
    public  void  start(){
        for (IRunnable it:runnables) {
            if(!it.isStart()){
               executorService.execute(it);
            }
        }
    }
    public  void  stop(){
        for (IRunnable it:runnables) {
            if(it.isStart()){
                it.setStart(false);
            }
        }
    }
    public <T extends IRunnable>  boolean  start(T runnable){
        return  add(runnable,true);
    }
    private <T extends IRunnable>   boolean  add(T runnable,boolean start){
        if(runnable==null)return  false;
        IRunnable runnable1=(IRunnable)null;
        for (IRunnable it:runnables) {
            if(it==runnable){
                runnable1=it;
                break;
            }
        }
        boolean has=runnable1!=null;
        if(!has){
            runnables.add(runnable);
            if(start)
            {
                executorService.execute(runnable);
            }
        }
        else{
            if(start&& !runnable1.isStart()){
                executorService.execute(runnable1);
            }
        }
        return  true;
    }
    /**
     * 获取许可
     * */
    public  void  acquire(){
       try {
           semaphore.acquire();
       }
       catch (InterruptedException ex){
           ex.printStackTrace();
       }
    }
    /**
     * 访问完后，释放
     * */
    public  void  release(){
        semaphore.release();
    }
    public  void  shutdown(){
        executorService.shutdown();
    }



}
