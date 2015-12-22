package app.transfer.smart.smarttransfer.welcome_activity;

import android.os.AsyncTask;
import android.util.Log;
import android.widget.ProgressBar;

import app.transfer.smart.smarttransfer.global.User;

/**
 * Created by apolol92 on 21.12.2015.
 */
public class ProgressbarAsyncTask extends AsyncTask<Void, Integer, Void> {
    private final int INTERVAL = 5;
    private ProgressBar pBar;
    private BroadcastServerFinder bFinder;

    public ProgressbarAsyncTask(ProgressBar pBar,BroadcastServerFinder bFinder) {
        this.pBar = pBar;
        this.bFinder = bFinder;
    }

    private int getTimeSecs() {
        return (int) (System.currentTimeMillis() / 1000);
    }

    public boolean isDone() {
        return done;
    }

    private boolean done;
    @Override
    protected Void doInBackground(Void... params) {
        int startTime = getTimeSecs();
        int currentTime = getTimeSecs();
        int counter;
        do {
            currentTime = getTimeSecs();
            counter = currentTime - startTime;
            publishProgress((int) ((float) counter * 100f / (float) INTERVAL));
        } while (counter <= INTERVAL);
        bFinder.cancel(true);
        for(int i = 0; i < User.getInstance().getBroadcastServerList().getServerList().size();i++) {
            Log.d("Broadcast-SERVER",User.getInstance().getBroadcastServerList().getServerList().get(i).getIp());
        }
        done = true;
        return null;
    }


    @Override
    protected void onProgressUpdate(Integer... values) {
        this.pBar.setProgress(values[0]);
        super.onProgressUpdate(values);
    }
}
