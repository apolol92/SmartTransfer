package app.transfer.smart.smarttransferapp.server_communication;


import android.util.Base64;

/**
 * Created by apolol92 on 02.01.2016.
 * This command is the protocol between app and WLAN-Server
 */
public class Command {
    /**
     * This is the userid, it changes every server-response
     */
    public int Id;
    /**
     * The username of the command sender
     */
    public String Username;
    /**
     * Which kind of command?
     */
    public int Typ;
    /**
     * Command manipulate Filename?
     */
    public String Filename;
    /**
     * Some extra Parameters?
     */
    public String Parameter;
    /**
     * If there data, this data is in data[]
     */
    public byte[] Data;

    /**
     * Print the Command header without clams
     * @return
     */
    public String printInnerHeader()
    {
        return Id + ";" + Username + ";" + Typ + ";" + Filename + ";" + Parameter + ";" + Data;
    }

    /**
     * Total command to a byte array
     * @return
     */
    public byte[] toByteArr()
    {
        String b64 = Base64.encodeToString(printInnerHeader().getBytes(),Base64.DEFAULT);
        String header = "{" + b64 + "}";
        int len = header.length() + this.Data.length;
        byte[] rawCmd = new byte[len];
        for(int i=0;i< len;i++)
        {
            if (i < header.length())
            {
                rawCmd[i] = header.getBytes()[i];
            }
            else
            {
                rawCmd[i] = (byte)this.Data[i-header.length()];
            }
        }
        return rawCmd;
    }
}
