package main;

import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

/**
 * Created by apolol92 on 02.01.2016.
 */
public class FileManager {
    public static byte[] readeFile() {
        Path path = Paths.get("data.png");
        try {
            byte[] data = Files.readAllBytes(path);
            return data;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static void saveFile(byte[] data,String name) {
        Path path = Paths.get(name+".png");
        try {
            Files.write(path,data);
        }
        catch(IOException e) {
            System.out.println("Failed saving..");
        }

    }
}
