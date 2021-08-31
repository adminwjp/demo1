
    PRAGMA foreign_keys = OFF

    drop table if exists t_city

    drop table if exists t_menu

    drop table if exists t_address

    drop table if exists t_seller_address

    drop table if exists t_agent_address

    drop table if exists t_manufacturer_address

    drop table if exists t_bank

    drop table if exists t_sms

    drop table if exists t_source_material

    drop table if exists t_token

    drop table if exists t_user_friend

    drop table if exists t_user_log

    drop table if exists t_admin

    drop table if exists t_user

    PRAGMA foreign_keys = ON

    create table t_city (
        id INTEGER not null,
       prodive TEXT,
       city TEXT,
       area TEXT,
       prodive_code TEXT,
       city_code TEXT,
       area_code TEXT,
       parent_id INTEGER,
       primary key (id),
       constraint fk_parent_id foreign key (parent_id) references t_city
    )

    create table t_menu (
        id INTEGER not null,
       parent_id INTEGER,
       soure TEXT,
       orders INTEGER,
       creation_time INTEGER,
       last_modification_time INTEGER,
       is_deleted INTEGER,
       deletion_time INTEGER,
       text TEXT,
       state TEXT,
       checked INTEGER,
       attributes_json TEXT,
       icon_cls TEXT,
       name TEXT,
       collpse INTEGER,
       groups TEXT,
       icon TEXT,
       href TEXT,
       description TEXT,
       hui_icon TEXT,
       id_name TEXT,
       ace_icon TEXT,
       primary key (id),
       constraint fk_parent_id foreign key (parent_id) references t_menu
    )

    create table t_address (
        id  integer primary key autoincrement,
       contact_name TEXT,
       address TEXT,
       area TEXT,
       city TEXT,
       province TEXT,
       country TEXT,
       memo TEXT,
       phone TEXT,
       post_code TEXT,
       is_default INTEGER,
       user_id INTEGER
    )

    create table t_seller_address (
        id  integer primary key autoincrement,
       contact_name TEXT,
       address TEXT,
       area TEXT,
       city TEXT,
       province TEXT,
       country TEXT,
       memo TEXT,
       phone TEXT,
       post_code TEXT,
       is_default INTEGER,
       user_id INTEGER
    )

    create table t_agent_address (
        id  integer primary key autoincrement,
       contact_name TEXT,
       address TEXT,
       area TEXT,
       city TEXT,
       province TEXT,
       country TEXT,
       memo TEXT,
       phone TEXT,
       post_code TEXT,
       is_default INTEGER,
       user_id INTEGER
    )

    create table t_manufacturer_address (
        id  integer primary key autoincrement,
       contact_name TEXT,
       address TEXT,
       area TEXT,
       city TEXT,
       province TEXT,
       country TEXT,
       memo TEXT,
       phone TEXT,
       post_code TEXT,
       is_default INTEGER,
       user_id INTEGER
    )

    create table t_bank (
        id  integer primary key autoincrement,
       bank_id TEXT,
       bank_name TEXT,
       bank_photo1 TEXT,
       bank_photo2 TEXT,
       bank_photo_id1 INTEGER,
       bank_photo_id2 INTEGER,
       bank_address TEXT,
       bank_user_name TEXT,
       bank_user_address TEXT,
       is_default INTEGER,
       user_id INTEGER
    )

    create table t_sms (
        id  integer primary key autoincrement,
       app_id TEXT,
       secrt TEXT
    )

    create table t_source_material (
        id  integer primary key autoincrement,
       src TEXT,
       key TEXT,
       base64 TEXT,
       description TEXT,
       buket TEXT,
       object_name TEXT
    )

    create table t_token (
        id  integer primary key autoincrement,
       token TEXT,
       token_expried INTEGER,
       refresh_token TEXT,
       refresh_token_expried INTEGER,
       create_date INTEGER,
       user_id INTEGER,
       flag INTEGER
    )

    create table t_user_friend (
        id  integer primary key autoincrement,
       user_id INTEGER,
       friend_id INTEGER,
       agree INTEGER,
       delete_flag INTEGER
    )

    create table t_user_log (
        id  integer primary key autoincrement,
       user_id INTEGER,
       account TEXT,
       email TEXT,
       phone TEXT,
       old_pwd TEXT,
       new_pwd TEXT,
       new_account TEXT,
       new_email TEXT,
       new_phone TEXT,
       add_date INTEGER,
       flag INTEGER
    )

    create table t_admin (
        id  integer primary key autoincrement,
       account TEXT,
       email TEXT,
       phone TEXT,
       pwd TEXT,
       nick_name TEXT,
       real_name TEXT,
       sex INTEGER,
       birthday INTEGER,
       head_pic TEXT,
       head_pic_id INTEGER,
       status INTEGER,
       register_date INTEGER,
       login_date INTEGER,
       register_ip INTEGER,
       login_ip INTEGER,
       description TEXT,
       fail_count INTEGER,
       parent_id INTEGER,
       role_id INTEGER
    )

    create table t_user (
        id  integer primary key autoincrement,
       edution TEXT,
       school TEXT,
       job_company TEXT,
       job TEXT,
       likes TEXT,
       marital INTEGER,
       description TEXT,
       card_id TEXT,
       card_photo1 TEXT,
       card_photo2 TEXT,
       hand_card_photo1 TEXT,
       hand_card_photo2 TEXT,
       card_photo_status INTEGER,
       card_photo_id1 TEXT,
       card_photo_id2 TEXT,
       hand_card_photo_id1 TEXT,
       hand_card_photo_id2 TEXT,
       level INTEGER,
       bank_id TEXT
    )
