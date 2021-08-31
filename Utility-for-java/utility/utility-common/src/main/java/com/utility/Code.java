package com.utility;

public enum Code {
    Success("sucesss","成功",200,true),
    Fail("fail","失败",400,false),
    Error("error","系统繁忙",500,false);
    private  String note;//描述
    private  int code;//状态码
    private boolean status;//状态
    private String englishNote;//英文描述
    private String chineseNote;//中文描述
    Code(String note, int code, boolean status){
        this.note=note;
        this.code=code;
        this.status=status;
    }
    Code(String englishNote, String chineseNote, int code, boolean status){
        this.englishNote=englishNote;
        this.chineseNote=chineseNote;
        this.note=englishNote;
        this.code=code;
        this.status=status;
    }
    public String getNote() {
        return note;
    }

    public void setNote(String note) {
        this.note = note;
    }

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public boolean isStatus() {
        return status;
    }

    public void setStatus(boolean status) {
        this.status = status;
    }
    public String getEnglishNote() {
        return englishNote;
    }

    public void setEnglishNote(String englishNote) {
        this.englishNote = englishNote;
    }

    public String getChineseNote() {
        return chineseNote;
    }

    public void setChineseNote(String chineseNote) {
        this.chineseNote = chineseNote;
    }
}
