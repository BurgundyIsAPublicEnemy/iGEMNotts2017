#include <stdlib.h>
#include<stdio.h>
#include <ctype.h>
#include <math.h>
#include <stdbool.h>
#include <string.h>

//gcc -o LuciferProto LuciferA.c -lm
#define newline "------------------ \n"


// gcc -o LuciferProto LuciferA.c -lm
struct color_des {
    int red;
    int blue;
    int green;
};

float sfGFP_LP(float sfGFP_con){
    float coeff = (1/0.699);
    float y = 100.13 * pow(sfGFP_con, coeff);
    return y;
}

float MRFP_LP(float MRFP_con){
    float coeff = (1/0.699);
    //CHANGE WITH RESULTS
    float y = 100.13 * pow(MRFP_con, coeff);
    return y;
}

float ECFP_LP(float ECFP_con){
    float coeff = (1/0.699);
    //CHANGE WITH RESULTS
    float y = 100.13 * pow(ECFP_con, coeff);
    return y;
}

bool isFloat(float val)
{
    int truncated = (int)val;
    return (val == truncated);
}

struct color_des getColor(int wavelength){
    struct color_des color_wave;
    if ((wavelength >= 375) && (wavelength <= 500)){
        color_wave.blue = 1;
    } else {
        color_wave.blue = 0;
    }
    if ((wavelength >= 350) && (wavelength <= 500)){
        color_wave.green = 1;
    }
    else {
        color_wave.green = 0;
    }
    if ((wavelength >= 400) && (wavelength <= 590)){
        color_wave.red = 1;
    }
    else {
        color_wave.red = 0;
    }
    
    return color_wave;
}

float gfp_EM(float gfp_abs){
    float em_gfp_orig_wave = roundf(gfp_abs * 100) / 100; 
    char GFPem_string[64];
    snprintf(GFPem_string, 50, "%f", gfp_abs);
    char* filename_em[100];
    char buffer_em[64];
   
    // Open file
    FILE *fptr1 = fopen("sgGFP - Em.txt", "r");
    if (fptr1 == NULL)
    {
        printf("Cannot open file \n");
        exit(0);
    }
    // Read contents from file
    float wave_length_em, percent_T_em;
    while (!feof(fptr1))
    {
        fgets(buffer_em, 64, fptr1);
        sscanf(buffer_em, "%f %f \n", &wave_length_em, &percent_T_em);
   	 float em_gfp_new_wave = roundf(percent_T_em * 100) / 100; 
        if ( em_gfp_new_wave == em_gfp_orig_wave){
//	printf ("PERCENTAGE NEW  %f \n",  em_gfp_new_wave);
 //       printf ("PERCENTAGE ORIG  %f \n", em_gfp_orig_wave);
            return wave_length_em;
        }
    }
    return 0;
}

int main(int argc, char *argv[]) {
    printf("@universityofnottingham2017geneticsoftware \n");
    printf("@AUTHOR Vikram Chhapwale \n");
    printf("Model: Flourescence Color and Intensity Predictor \n");
    printf(newline);
    
    //STEP 1 -- RECALCULATE PROTEIN
    
    double k1 = 0.1, k2 = 4, k3 = 0.04, Vm = 5.6, Km = 0.5, S = 0.1,  S1 = 0.1, temp = 0.1, temp1 = 1, inhibited = 0, protein = atof(argv[1]);
    int  t = 0;
    
    for (t; t <= 100; t++){
        //FOR GFP ----
        inhibited = (- exp(- 0.2*t) + 1);
        inhibited = pow (inhibited,0.5) * protein;
        
        //STEP 2-- GET LIGHT POTENTIAL FROM CONCENTRATION OF PROTEINS
        float sfGFP_con, MRFP_con, ECFP_con;
        sfGFP_con = inhibited;
        float LP_sfGFP = sfGFP_LP(sfGFP_con);
        MRFP_con = atof(argv[2]);
        float LP_MRFP = MRFP_LP(MRFP_con);
        ECFP_con = atof(argv[3]);
        float LP_ECFP = ECFP_LP(ECFP_con);

        //STEP 3-- GET WAVELENGTH WITHIN PARAMETERS
        int wavelengths[1];
        struct color_des color_laser[10];
        int i = 4, x = 0;
        for (i; i < argc; i++){
            wavelengths[x] = atoi(argv[i]);
            color_laser[x] = getColor(wavelengths[x]);
            x++;
        }
       
        //STEP 4-- CHECK PERCTENAGE FROM WAVELENGTH FOR GFP
        char GFP_string[64];
        snprintf(GFP_string, 50, "%f", LP_sfGFP);
        char* filename[100];
        char buffer[64];
        
        // Open file
        FILE *fptr = fopen("sgGFP - Abs.txt", "r");
        if (fptr == NULL)
        {
            printf("Cannot open file \n");
            exit(0);
        }
        // Read contents from file
        float wave_length, percent_T, expected_fl;
        float final_per;
        while (!feof(fptr))
        {
            fgets(buffer, 64, fptr);
            sscanf(buffer, "%f %f \n", &wave_length, &percent_T);
            
            i = 0;
            for (i; i < sizeof(wavelengths); i++){
                if (wavelengths[i] == roundf(wave_length)){
                    expected_fl = percent_T * LP_sfGFP;
                    final_per = percent_T;
                }
            }
        }
        printf(newline);
        printf ("At t minutes =  %d \n", t);
        printf ("|PROTEIN CONCENTRATION: %f    nanograms per microlitre per second|\n", inhibited);
        printf ("|EXP FL AT INTENSITY  : %f                                    RFU|\n", expected_fl);
        printf ("|EXP FL AT WAVELENGTH : %f                             nanometers|\n", gfp_EM(final_per));
  
   char sentence[1000];
   FILE *fptr1;

   fptr1 = fopen("tmp.txt", "a");
   if(fptr1 == NULL)
   {
      printf("Error!");
      exit(1);
   }
   
sprintf(sentence, "%f %d \n", expected_fl, t);
   fprintf(fptr1,"%s", sentence);
   fclose(fptr1);


        printf(newline);
        fclose(fptr);
    }

system("/home/violet/iGEM_NOTTS/lg");
    return 0;
}
