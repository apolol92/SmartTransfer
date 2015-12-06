package app.transfer.smart.smarttransferapp;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.DhcpInfo;
import android.net.NetworkInfo;
import android.net.wifi.WifiManager;
import android.os.Handler;
import android.os.Looper;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.TextView;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.MulticastSocket;
import java.net.SocketException;
import java.net.UnknownHostException;

public class MainActivity extends AppCompatActivity {
    protected static String DEBUG_TAG = "UDPMessenger"; // to log out things
    protected static final Integer BUFFER_SIZE = 4096; // size of the reading buffer

    protected String TAG; // chat TAG
    protected int MULTICAST_PORT; // chat port

    public static String ipToString(int ip, boolean broadcast) {
        String result = new String();

        Integer[] address = new Integer[4];
        for (int i = 0; i < 4; i++)
            address[i] = (ip >> 8 * i) & 0xFF;
        for (int i = 0; i < 4; i++) {
            if (i != 3)
                result = result.concat(address[i] + ".");
            else result = result.concat("255.");
        }
        return result.substring(0, result.length() - 2);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        final TextView Mensaje;
        MULTICAST_PORT = 1337;
        DatagramPacket packet;

        Mensaje = (TextView) findViewById(R.id.Mensaje);
        new Thread(new Runnable() {
            public void run() {
    //Send UDP-Multicast
//                try {
//                    MulticastSocket mSocket = new MulticastSocket(MULTICAST_PORT);
//                    while (true) {
//                        byte data[] = "Hallo WLAN-MARATHON".getBytes();
//                        try {
//                            DatagramPacket packet = new DatagramPacket(data, data.length, InetAddress.getByName("255.255.255.255"), MULTICAST_PORT);
//                            mSocket.send(packet);
//                        } catch (UnknownHostException e) {
//                            e.printStackTrace();
//                        }
//                        Thread.sleep(1000);
//                    }
//                } catch (IOException e) {
//                    e.printStackTrace();
//                } catch (InterruptedException e) {
//                    e.printStackTrace();
//                }
    //Receive UDP-Multicast
                try {
                    MulticastSocket mSocket = new MulticastSocket(MULTICAST_PORT);
                    while (true) {
                        byte data[] = new byte[500];
                        try {
                            DatagramPacket packet = new DatagramPacket(data,data.length);
                            mSocket.receive(packet);
                            Log.d("Hallo", String.valueOf(packet.getAddress()));
                        } catch (UnknownHostException e) {
                            e.printStackTrace();
                        }
                        Thread.sleep(1000);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }

            }
        }).start();


    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
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
