package rd_compare;

import java.io.FileNotFoundException;

public class rd_main {
	public static int locks = 0;
	 public static void main(String[] args) throws FileNotFoundException{
		 read_gfp object1 = new read_gfp("GFP");
		 object1.read_gfpfile();
		 read_gfp object2 = new read_gfp("RFP");
		 object2.read_gfpfile();
		 read_gfp object3 = new read_gfp("CFP");
		 object3.read_gfpfile();
		 
		 if (locks == 3){
			 //do system call
		 }
	 } 
}
