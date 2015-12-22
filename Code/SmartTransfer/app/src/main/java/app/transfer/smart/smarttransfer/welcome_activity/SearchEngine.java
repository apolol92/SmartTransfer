package app.transfer.smart.smarttransfer.welcome_activity;

import android.content.Context;
import android.content.Intent;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.widget.ProgressBar;

import java.util.concurrent.Executors;
import java.util.concurrent.ThreadPoolExecutor;

import app.transfer.smart.smarttransfer.MainActivity;
import app.transfer.smart.smarttransfer.server_selection_activity.ServerSelectionActivity;
import app.transfer.smart.smarttransfer.global.SqliteManager;
import app.transfer.smart.smarttransfer.global.User;

/**
 * Created by apolol92 on 22.12.2015.
 */
public class SearchEngine {
    Context context;
    BroadcastServerFinder bServerFinder;
    ProgressbarAsyncTask pbTask;
    WelcomeActivity welcomeActivity;
    public SearchEngine(ProgressBar pb,Context context, WelcomeActivity welcomeActivity) {
        this.context = context;
        this.bServerFinder = new BroadcastServerFinder();
        this.welcomeActivity = welcomeActivity;
        this.pbTask = new ProgressbarAsyncTask(pb,this.bServerFinder);
    }

    public void run() {
        ThreadPoolExecutor mPool;
        mPool =  (ThreadPoolExecutor) Executors.newFixedThreadPool(5);
        this.bServerFinder.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        try {
            this.pbTask.executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
            new Thread(new Runnable() {
                @Override
                public void run() {
                    while(pbTask.isDone()==false) {

                    }
                    WifiManager wifiManager = (WifiManager) context.getSystemService(Context.WIFI_SERVICE);
                    WifiInfo info = wifiManager.getConnectionInfo ();
                    for(int i = 0; i < User.getInstance().getBroadcastServerList().serverList.size(); i++) {
                        WlanServer wlanServer = new WlanServer();
                        wlanServer.setIp(User.getInstance().getBroadcastServerList().serverList.get(i).getIp());
                        wlanServer.setName(User.getInstance().getBroadcastServerList().serverList.get(i).getName());
                        wlanServer.setWlanSsid(info.getSSID());
                        User.getInstance().getWlanServerList().servers.add(wlanServer);
                        startNextActivity();
                        welcomeActivity.finish();
                        try {
                            while(true) {
                                this.finalize();
                            }
                        } catch (Throwable throwable) {
                            throwable.printStackTrace();
                        }
                    }
                }
            }).start();


        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void startNextActivity() {
        WlanServer currentWlanServer = SqliteManager.getInstance().getCurrentWlanServer();
        if(currentWlanServer==null) {
            Intent nIntent = new Intent(this.context,ServerSelectionActivity.class);
            nIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            this.context.startActivity(nIntent);
        }
        else {
            if (User.getInstance().getWlanServerList().contains(currentWlanServer)) {
                Intent nIntent = new Intent(this.context, MainActivity.class);
                nIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                this.context.startActivity(nIntent);
            } else {
                Intent nIntent = new Intent(this.context, ServerSelectionActivity.class);
                nIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                this.context.startActivity(nIntent);

            }
        }

    }
}
