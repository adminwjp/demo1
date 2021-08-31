package com.utility.database.bridge;

import com.utility.database.adapter.*;

public  interface TableFactory extends ColumnAdapter {
    public void getTables() ;
}