package app.transfer.smart.smarttransferapp;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import org.w3c.dom.Text;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.nio.CharBuffer;

/**
 * Created by apolol92 on 06.12.2015.
 */
public class MainActivity extends Activity {
    Button btLoadImg;
    TextView tvStatus;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        this.tvStatus = (TextView)findViewById(R.id.tvStatus2);
        this.btLoadImg = (Button)findViewById(R.id.btLoadImg);
        this.btLoadImg.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Thread t = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        CommandFactory commandFactory = new CommandFactory();
                        Command request = commandFactory.createCommand(-1,"KeineMemme",4,"none","none","none");
                        InetAddress serverAddr = User.getInstance().getSelectedServer().getServerAddress();
                        try {
                            Socket s = new Socket("192.168.2.148",ServerFinder.MULTICAST_PORT);
                            BufferedReader in = new BufferedReader(new InputStreamReader(s.getInputStream()));
                            BufferedWriter out = new BufferedWriter(new OutputStreamWriter(s.getOutputStream()));
                            out.write(request.toString());
                            out.flush();
                            //accept server response
                            String inMsg = in.readLine() + System.getProperty("line.separator");
                            Log.d("Received msg", inMsg);
                            //close connection
                            s.close();
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                });
                t.start();


            }
        });
    }
}
