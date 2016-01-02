package app.transfer.smart.smarttransfer.welcome_activity;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ProgressBar;

import app.transfer.smart.smarttransfer.R;
import app.transfer.smart.smarttransfer.global.SqliteManager;
import app.transfer.smart.smarttransfer.global.User;

public class WelcomeActivity extends AppCompatActivity {
    private ProgressBar pBar;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        User.init();
        SqliteManager.init(getApplicationContext());

        setContentView(R.layout.activity_welcome);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        this.pBar = (ProgressBar)findViewById(R.id.customProgress);
        SearchEngine searchEngine = new SearchEngine(this.pBar,getApplicationContext(),this);
        searchEngine.run();
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
