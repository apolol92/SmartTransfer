package app.transfer.smart.smarttransferapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.util.ArrayList;

public class WelcomeActivity extends AppCompatActivity {
    ArrayList<ServerInfo> serverInfos;
    TextView tvStatus;
    Button btConnect;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_welcome);
      ServerFinder serverFinder = new ServerFinder();
        serverInfos = serverFinder.findServers();
//        tvStatus = (TextView)findViewById(R.id.textView);
//        btConnect = (Button)findViewById(R.id.btConnect);
//        btConnect.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View v) {
//                try {
//                    User.getInstance().init(serverInfos.get(0));
//                    Intent mIntent = new Intent(getApplication(),MainActivity.class);
//                    startActivity(mIntent);
//                }
//                catch (Exception ex) {
//                    tvStatus.setText("No Server avaible..");
//                }
//
//
//            }
//        });
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
