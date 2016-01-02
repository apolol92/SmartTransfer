package main;

import java.util.Base64;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class Command {
    public int Id;
    public String Username;
    public int Typ;
    public String Filename;
    public String Parameter;
    public byte[] Data;

    public String printInnerHeader()
    {
        return Id + ";" + Username + ";" + Typ + ";" + Filename + ";" + Parameter + ";" + Data;
    }
    public byte[] toByteArr()
    {
        String b64 = Base64.getEncoder().encodeToString(printInnerHeader().getBytes());
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
