package app.transfer.smart.smarttransferapp.main_activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.HorizontalScrollView;
import android.widget.LinearLayout;
import android.widget.Toast;

import app.transfer.smart.smarttransferapp.R;
import app.transfer.smart.smarttransferapp.main_activity.dfile.DFileListServer;
import app.transfer.smart.smarttransferapp.main_activity.drop_area.DropArea;
import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;
import app.transfer.smart.smarttransferapp.serverselection_activity.ServerLoginCommander;

public class MainActivity extends AppCompatActivity {
    public static int lastId;
    public static WlanServer wlanServer;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null) {
            this.lastId = (Integer) bundle.get(ServerLoginCommander.ID);
            this.wlanServer = (WlanServer)bundle.get(ServerLoginCommander.SERVER);
            Toast.makeText(getApplicationContext(), this.lastId + " " + this.wlanServer.getName(), Toast.LENGTH_SHORT).show();
        }
        HorizontalScrollView horizontalScrollView = (HorizontalScrollView)findViewById(R.id.main_server_selection_scroll_view);
        LinearLayout linearLayout = (LinearLayout)findViewById(R.id.main_horizontal_scroll_view_content);
        if(horizontalScrollView==null) {
            System.out.println("Horizontal null");
        }
        if(linearLayout==null) {
            System.out.println("linear null");
        }
        DFileListServer dFileListServer = new DFileListServer(getApplicationContext(),3,horizontalScrollView,linearLayout);
        ServerListFilesCommander listFilesCommander = new ServerListFilesCommander(wlanServer,lastId,dFileListServer);
        listFilesCommander.execute();
        DropArea dropArea = new DropArea(getApplicationContext(),(LinearLayout)findViewById(R.id.main_linear_layout_drop_area));

    }

}
