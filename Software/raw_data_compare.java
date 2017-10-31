import java.io.File;
import java.util.*;
import java.io.FileNotFoundException;
import java.util.Scanner;
import static java.lang.Math.abs; 
import static java.lang.Math.pow;
public class raw_data_compare {

	public static int locks = 0;
	 public static void main(String[] args) throws FileNotFoundException{
		 read_gfpfile("GFP");
		 read_gfpfile("RFP");
		 read_gfpfile("CFP");
		 
		 if (locks == 3){
			 //do system call
		 }
	 } 
	 
	public static List<Double> gfp_a_t = new ArrayList<Double>();
	public static List<Double> gfp_b_t = new ArrayList<Double>();
	//... for polynomial
	
	public static double x3 = 0;
	public static double x2 = 0;
	public static double x1 = 0;
	public static double k = 0;
	
	public static void read_gfpfile(String look_for) throws FileNotFoundException{
		 float gfp_a = 0.0f;
		 float gfp_b = 0.0f;
		 int timescale = 2;
		 double threshold = 0.0f;
		 int count = 0, counting_verified = 0;
	     	 Scanner scanner = new Scanner(new File("/home/violet/iGEM_NOTTS/Random Constructions.csv"));
	     //  Scanner scanner = new Scanner(new File("/home/violet/iGEM_NOTTS/x1.csv"));
	        scanner.useDelimiter(",");
	        String buffer;
	        while (scanner.hasNext()){
	        	
	       	if (scanner.next().contains(look_for)){	       		
	        		buffer = scanner.next();
	        	
	        		try {
	        			buffer = scanner.next();
	        			gfp_a = Float.parseFloat(buffer);
	        			gfp_a_t.add((double) gfp_a);        	

	        		}
	        		catch (NumberFormatException nfe){
	        		
	        		}
	        		
	        		buffer = scanner.next();
	        	
	        		
	        		try {
	        			
	        			gfp_b = Float.parseFloat(buffer);
	        			gfp_b_t.add((double) gfp_b);  
	        			count++;
	        			
	        		}
	        		catch (NumberFormatException nfe){
	        		
	        		}
	        	
	        	
	        	} 
	        } 
	        scanner.close();
	        
	        poly_fit(gfp_a_t.size());
	        
			for (int x = 0; x < gfp_a_t.size(); x++){
			//recalculate
			threshold = -(x3 * pow(timescale, 3) + x2 * pow(timescale, 2) + x1 * pow(timescale, 1) + k);
			if (abs(gfp_a_t.get(x) - gfp_b_t.get(x)) < (threshold + 200)){
        		counting_verified++;
        		} 
			System.out.print("Debug: Threshold: " + threshold + " \n Difference " + abs(gfp_a_t.get(x) - gfp_b_t.get(x)) + "\n");
			timescale = timescale + 2;
			
			}
			
	        if (counting_verified == count){
	        	System.out.print("Lock lifted \n \n ");
	        	locks++;
	        } else {
	        	System.out.print("Lock out \n \n");
	        }
	        
	        //... reset all statics
	        gfp_a_t.clear();
	        gfp_b_t.clear();
	        x3 = 0;
	        x2 = 0;
	        x1 = 0;
	        k = 0;
	    }
	 
	 
	 public static void poly_fit(int N){
			int n = 3;                   
			N = 3;                      
			Double[] x = gfp_a_t.toArray(new Double[gfp_a_t.size()]);                        
			Double[] y = {(double)0, (double)2, (double)4, (double)6};
		    double X[] = new double[2 * n + 1];
		    for (int i = 0; i < 2 * n + 1; i++) {
		        X[i] = 0;
		        for (int j = 0; j < N; j++)
		            X[i] = X[i] + Math.pow(x[j], i);       
		    }
		    double B[][] = new double[n + 1][n + 2]; 
		    double a[] = new double[n + 1];   
		    for (int i = 0; i <= n; i++)
		        for (int j = 0; j <= n; j++)
		            B[i][j] = X[i + j];       
		    double Y[] = new double[n + 1];                   
		    for (int i = 0; i < n + 1; i++) {
		        Y[i] = 0;
		        for (int j = 0; j < N; j++)
		            Y[i] = Y[i] + Math.pow(x[j], i) * y[j]; 
		    }
		    for (int i = 0; i <= n; i++)
		        B[i][n + 1] = Y[i];         
		    n = n + 1;
		    for (int i = 0; i < n; i++)   
		        for (int k = i + 1; k < n; k++)
		            if (B[i][i] < B[k][i])
		                for (int j = 0; j <= n; j++) {
		                    double temp = B[i][j];
		                    B[i][j] = B[k][j];
		                    B[k][j] = temp;
		                }

		    for (int i = 0; i < n - 1; i++)         
		        for (int k = i + 1; k < n; k++) {
		            double t = B[k][i] / B[i][i];
		            for (int j = 0; j <= n; j++)
		                B[k][j] = B[k][j] - t * B[i][j];   
		        }
		    for (int i = n - 1; i >= 0; i--)                
		    {                      
		        a[i] = B[i][n];               
		        for (int j = 0; j < n; j++)
		            if (j != i)        
		                a[i] = a[i] - B[i][j] * a[j];
		        a[i] = a[i] / B[i][i];         
		    }

		    
			for (int l = 0; l < a.length; l++){
				if (x3 == 0){
				x3 = a[l];
				} else if (x2 == 0){
					x2 = a[l];
					} else if (x1 == 0){
						x1 = a[l];
						} else if (k == 0){
							k = a[l];
						}
			}
	 }

	 
	 
}
