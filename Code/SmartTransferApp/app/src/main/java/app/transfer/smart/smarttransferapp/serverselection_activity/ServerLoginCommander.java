package app.transfer.smart.smarttransferapp.serverselection_activity;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;

import app.transfer.smart.smarttransferapp.main_activity.MainActivity;
import app.transfer.smart.smarttransferapp.server_communication.Command;
import app.transfer.smart.smarttransferapp.server_communication.CommandFactory;
import app.transfer.smart.smarttransferapp.server_communication.Crypter;
import app.transfer.smart.smarttransferapp.server_communication.Receiver;
import app.transfer.smart.smarttransferapp.server_communication.Sender;
import app.transfer.smart.smarttransferapp.serversearch_activity.WlanServer;

/**
 * Created by apolol92 on 07.01.2016.
 * This class sends a login command to the server
 */
public class ServerLoginCommander extends AsyncTask<Void,Void,Void> {
    /**
     * This is the bundle key for command ID
     */
    public static final String ID = "id";
    /**
     * This is the bundle key for WLAN-Server
     */
    public static final String SERVER="server";
    /**
     * Reference to the parent activity
     */
    private ServerSelectionActivity serverSelectionActivity;
    /**
     * This will be the selected WLAN-Server
     */
    private WlanServer wlanServer;
    /**
     * This is the server response
     */
    Command serverResponse;
    /**
     * This is the context of the parent activity
     */
    Context context;

    /**
     * Creates the ServerLoginCommander
     * @param wlanServer
     * @param context
     * @param serverSelectionActivity
     */
    public ServerLoginCommander(WlanServer wlanServer, Context context,ServerSelectionActivity serverSelectionActivity) {
        this.wlanServer = wlanServer;
        this.context = context;
        this.serverSelectionActivity = serverSelectionActivity;
    }

    /**
     * Sends and receive login command
     * @param params
     * @return
     */
    @Override
    protected Void doInBackground(Void... params) {
        Command loginCmd = CommandFactory.createCommand(-1, "USER", 9, "none", "none", new byte[1]);
        Sender.sendData(Crypter.Encrypt(loginCmd.toByteArr(), this.wlanServer.getPw()), this.wlanServer);
        byte[] serverResponseBytes = Crypter.Decrypt(Receiver.receiveMsg(Sender.socket),wlanServer.getPw());
        this.serverResponse = CommandFactory.extractCommand(serverResponseBytes);
        System.out.println(this.serverResponse.Id + " " + this.serverResponse.Username + " " + this.serverResponse.Typ);
        return null;
    }

    /**
     * After receiving command.. open MainActivity with given WLAN-Server and given ID
     * @param aVoid
     */
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        //Open MainActivity
        Intent nIntent = new Intent(context, MainActivity.class);
        nIntent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        Bundle nBundle = new Bundle();
        nBundle.putInt(ID, serverResponse.Id);
        nBundle.putSerializable(SERVER,wlanServer);
        nIntent.putExtras(nBundle);
        context.startActivity(nIntent);
        this.serverSelectionActivity.finish();


    }
}
