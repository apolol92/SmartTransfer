package app.transfer.smart.smarttransfer.global;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.AsyncTask;
import android.os.Build;
import android.util.Log;
import android.widget.ImageView;

import java.io.BufferedWriter;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;

import app.transfer.smart.smarttransfer.Command;
import app.transfer.smart.smarttransfer.CommandFactory;
import app.transfer.smart.smarttransfer.crypto.Crypter;
import app.transfer.smart.smarttransfer.welcome_activity.WlanServer;

/**
 * Created by apolol92 on 30.12.2015.
 */
public class DataDownloader extends AsyncTask<Void,byte[],Void>{
    ImageView imageView;
    WlanServer wlanServer;
    Context context;
    byte[] data;
    public DataDownloader(ImageView imageView, WlanServer wlanServer, Context context){
        this.imageView = imageView;
        this.wlanServer = wlanServer;
        this.context = context;
    }


    @Override
    protected Void doInBackground(Void... params) {
        final String SERVER_IP=wlanServer.getIp().substring(1);
        final int SERVER_PORT = 7000;
        //Create Command
        CommandFactory cmdFactory = new CommandFactory();
        //{42;Hans;1;C:\\Users\\Dennis\\Pictures\\test\\abc;none;Inhalt}
        Command cmd = null;
        cmd = cmdFactory.createCommand(User.getInstance().id, "USER", 0, "hallowelt.png", "SERVER_PW","");
        System.out.print("HERE:::::"+cmd.toString());
        System.out.println("Cmd created..");
        System.out.println("Length: " + cmd.toString().length());
        //-----
        try {
            System.out.println(SERVER_IP);
            InetAddress serverAddr = InetAddress.getByName(SERVER_IP);
            System.out.println(SERVER_IP);
            Socket socket = new Socket(SERVER_IP, SERVER_PORT);
            System.out.println("Socket created..");
            //sends the message to the server
            PrintWriter mBufferOut = new PrintWriter(new BufferedWriter(new OutputStreamWriter(socket.getOutputStream())), true);
            //System.out.println(wlanServer.getPw() + cmd.toString());
            byte[] msg2Send = Crypter.Encrypt("test123456789123" + cmd.toString(), "test123456789123");
            System.out.print(new String(msg2Send, "UTF-8"));
            //System.out.println(msg2Send);
            ServerCommunicator.sendMsgAsByteArr(socket, msg2Send);
            Command recCmd = cmdFactory.extractCommandFromStr(Crypter.Decrypt(ServerCommunicator.receiveMsg(socket), "test123456789123"));
            socket.close();
            this.data = recCmd.data.getBytes();
            publishProgress(this.data);
            //Command recCmd = cmdFactory.extractCommandFromStr(Crypter.Decrypt(receiveMsg(socket),"test123456789123"));
            //socket.close();
            //System.out.println(recCmd.toString());
            //System.out.println(recCmd.id);
            //System.out.println("RECEIVED ID:"+recCmd.id);
            //User.getInstance().id = recCmd.id;



        } catch (UnknownHostException e) {
            System.out.println("ERROR 1111111111111111111");
            e.printStackTrace();
        } catch (IOException e) {
            System.out.println("ERROR 2222222222222222222");
            e.printStackTrace();
        } catch (Throwable throwable) {
            throwable.printStackTrace();
        }
        return null;
    }
    @TargetApi(Build.VERSION_CODES.LOLLIPOP)
    @Override
    protected void onProgressUpdate(byte[]... values) {
        super.onProgressUpdate(values[0]);
        byte[] data = values[0];
        System.out.print(new String(data));
        Bitmap bMap = BitmapFactory.decodeByteArray(data, 0, data.length);
        imageView.setImageBitmap(bMap);
    }
    public static Bitmap drawableToBitmap(Drawable drawable) {
        if (drawable instanceof BitmapDrawable) {
            return ((BitmapDrawable) drawable).getBitmap();
        }

        final int width = !drawable.getBounds().isEmpty() ? drawable
                .getBounds().width() : drawable.getIntrinsicWidth();

        final int height = !drawable.getBounds().isEmpty() ? drawable
                .getBounds().height() : drawable.getIntrinsicHeight();

        final Bitmap bitmap = Bitmap.createBitmap(width <= 0 ? 1 : width,
                height <= 0 ? 1 : height, Bitmap.Config.ARGB_8888);

        Log.v("Bitmap width - Height :", width + " : " + height);
        Canvas canvas = new Canvas(bitmap);
        drawable.setBounds(0, 0, canvas.getWidth(), canvas.getHeight());
        drawable.draw(canvas);

        return bitmap;
    }
}

