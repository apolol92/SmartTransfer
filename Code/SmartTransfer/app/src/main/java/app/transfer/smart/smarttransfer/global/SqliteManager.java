package app.transfer.smart.smarttransfer.global;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import app.transfer.smart.smarttransfer.welcome_activity.WlanServer;


/**
 * Created by apolol92 on 21.12.2015.
 * This SqliteManager is a singleton pattern class.
 * Only one SqliteManager is needed..
 *
 */
public class SqliteManager  extends SQLiteOpenHelper{
    /**
     * App-Context
     */
    private Context context;
    /**
     * Databasename
     */
    public static final String DATABASE_NAME = "smarttransfer";
    /**
     * Databaseversion
     */
    public static final int DATABASE_VERSION = 1;
    /**
     * Tables
     */
    public static final String TABLE_CURRENT_SERVER = "current_server";
    public static final String TABLE_LAST_TAB = "last_tab";
    /**
     * Table-Columns CURRENT_SERVER
     */
    public static final String CURRENT_SERVER_SSID = "ssid";
    public static final String CURRENT_SERVER_SERVNAME = "servername";
    public static final String CURRENT_SERVER_IP = "ip";
    public static final String CURRENT_SERVER_PW = "password";
    /**
     * Table-Columns TABLE_LAST_TAB
     */
    public static final String LAST_TAB_TABPOSITION = "tabposition";
    /**
        This SQL-Commands create tables
    */
    private static final String CREATE_CURRENT_SERVER_TABLE = "CREATE TABLE IF NOT EXISTS "+TABLE_CURRENT_SERVER+"("+CURRENT_SERVER_SSID+" TEXT, "+CURRENT_SERVER_SERVNAME+" TEXT, "
            + CURRENT_SERVER_IP + " TEXT, "+CURRENT_SERVER_PW+ " TEXT)";
    private static final String CREATE_LAST_TAB_TABLE = "CREATE TABLE IF NOT EXISTS "+TABLE_LAST_TAB+"("+LAST_TAB_TABPOSITION+" INTEGER)";

    private SqliteManager(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;
    }

    private static SqliteManager manager = null;

    /**
     * This method initilize the SqliteManager
     * @param context
     */
    public static void init(Context context) {
        if(manager == null) {
            manager = new SqliteManager(context);
        }
    }

    /**
     * Get the SqliteManager
     * @return the SqliteManager
     */
    public static SqliteManager getInstance() {
        return manager;
    }

    /**
     * OnCreate creates the table, if not exists
     * @param db
     */
    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL(this.CREATE_CURRENT_SERVER_TABLE);
        db.execSQL(this.CREATE_LAST_TAB_TABLE);
        //deleteCurrentServer();

    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }

    /**
     * This method sets a new smarTransferServer to the sqlite table
     * @param wlanServer
     */
    public void setCurrentServer(WlanServer wlanServer) {
        deleteCurrentServer();
        insertCurrentWlanServer(wlanServer);
    }

    /**
     * Delete the current server from the TABLE_CURRENT_SERVER
     */
    private void deleteCurrentServer() {
        SQLiteDatabase db = super.getWritableDatabase();
        String query = "DELETE FROM " + TABLE_CURRENT_SERVER;
        db.execSQL(query);
    }

    /**
     * This method inserts a smartTransferServer to TABLE_CURRENT_SERVER
     * @param wlanServer
     */
    private void insertCurrentWlanServer(WlanServer wlanServer) {
        SQLiteDatabase db = super.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(CURRENT_SERVER_SSID,wlanServer.getWlanSsid());
        values.put(CURRENT_SERVER_SERVNAME, wlanServer.getName());
        values.put(CURRENT_SERVER_IP, wlanServer.getIp());
        values.put(CURRENT_SERVER_PW, wlanServer.getPw());
        try {
            db.insert(TABLE_CURRENT_SERVER, "null", values);

        }
        catch (Exception ex) {
            ex.printStackTrace();

        }
    }

    /**
     * Get the current server from TABLE_CURRENT_SERVER
     * @return current server
     */
    public WlanServer getCurrentWlanServer() {
        WlanServer wlanServer = null;
        try {
            String selectQuery = "SELECT * FROM "+TABLE_CURRENT_SERVER;
            Cursor cursor = this.getReadableDatabase().rawQuery(selectQuery, null);
            cursor.moveToFirst();
            if(cursor.getString(0)==null) {
                return null;
            }
            wlanServer = new WlanServer();
            wlanServer.setWlanSsid(cursor.getString(0));
            wlanServer.setName(cursor.getString(1));
            wlanServer.setIp(cursor.getString(2));
            wlanServer.setPw(cursor.getString(3));
            return wlanServer;
        }
        catch(Exception ex) {
            return wlanServer;
        }
    }

    /**
     * Set the last tab position to the TABLE_LAST_TAB
     * @param position
     */
    public void setLastTabPosition(int position) {
        deleteLastTabPosition();
        insertLastTabPosition(position);
    }

    /**
     * Delete the last tab position from TABLE_LAST_TAB
     */
    private void deleteLastTabPosition() {
        SQLiteDatabase db = super.getWritableDatabase();
        String query = "DELETE FROM " + TABLE_LAST_TAB;
        db.execSQL(query);
    }

    /**
     * This method inserts a new last tab position
     * @param position
     */
    private void insertLastTabPosition(int position) {
        SQLiteDatabase db = super.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(LAST_TAB_TABPOSITION,position);

        try {
            db.insert(TABLE_LAST_TAB, "null", values);
        }
        catch (Exception ex) {
            ex.printStackTrace();

        }
    }

    /**
     * Get the last tab position
     * @return position
     */
    public int getLastTabPosition() {
        try {
            String selectQuery = "SELECT * FROM " + TABLE_LAST_TAB;
            Cursor cursor = super.getReadableDatabase().rawQuery(selectQuery, null);
            cursor.moveToFirst();
            return cursor.getInt(0);
        }
        catch (Exception ex) {
            return -1;
        }
    }


}
