package com.utility.database.adapter;

import com.utility.database.*;

public  class MySqlColumnAdapter extends AbstractColumnAdapter implements ColumnAdapter {
    
    @Override
    protected DatabaseFactory getDatabaseFactory(){
        if(databaseFactory==null){
            databaseFactory=new MySqlFactory();
        }
        return databaseFactory;
    }


}