package app.transfer.smart.smarttransferapp;

import android.content.Intent;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.util.ArrayList;

public class WelcomeActivity extends AppCompatActivity {
    ArrayList<ServerInfo> serverInfos;
    TextView tvStatus;
    Button btConnect;
    public static int ipStringToInt(String str) {
        int result = 0;
        String[] array = str.split("\\.");
        if (array.length != 4) return 0;
        try {
            result = Integer.parseInt(array[3]);
            result = (result << 8) + Integer.parseInt(array[2]);
            result = (result << 8) + Integer.parseInt(array[1]);
            result = (result << 8) + Integer.parseInt(array[0]);
        } catch (NumberFormatException e) {
            return 0;
        }
        return result;
    }
    public static InetAddress intToInetAddress(int hostAddress) {
        InetAddress inetAddress;
        byte[] addressBytes = { (byte)(0xff & hostAddress),
                (byte)(0xff & (hostAddress >> 8)),
                (byte)(0xff & (hostAddress >> 16)),
                (byte)(0xff & (hostAddress >> 24)) };

        try {
            inetAddress = InetAddress.getByAddress(addressBytes);
        } catch(UnknownHostException e) {
            return null;
        }
        return inetAddress;
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_welcome);

        final ServerFinder serverFinder = new ServerFinder();
        Thread t = new Thread(new Runnable() {
            @Override
            public void run() {
                serverInfos = serverFinder.findServers();
            }
        });
        t.start();

        tvStatus = (TextView)findViewById(R.id.textView);
        btConnect = (Button)findViewById(R.id.btConnect);
        btConnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                    serverInfos = new ArrayList<ServerInfo>();
                    serverInfos.add(new ServerInfo(intToInetAddress(ipStringToInt("192.168.2.148"))));
                    User.getInstance().init(serverInfos.get(0));
                    Log.d("DServer", serverInfos.get(0).getIpAsString());
                    Intent mIntent = new Intent(getApplication(),MainActivity.class);
                    startActivity(mIntent);



            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_welcome, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
