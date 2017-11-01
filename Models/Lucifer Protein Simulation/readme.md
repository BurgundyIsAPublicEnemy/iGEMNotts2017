
# Lucifer Protein Simulation
--------------
## Purpose
This simulation was created combining all the models to help build different components.
It was created to help the wet lab predict how the spectra might look as well as create sample data for the Raw Data and 
Image Comparision verification.

When creating these models, real world data was integrated we can have accurate representation of how the bacteria would behave in real life. This suggests that when comparing the modelled data to real life data from for our lab data set. there is a strong fit. However, this is not necessarily true for all cases: we simply only had data for the conditions we were using, which suggests that more data would be required for the models to be truly representative of real world data.

## Installation 
Simulation was written entirely in C. Currently, stable.
Navigate to directory in Terminal / Command Prompt. Enter all seperately.
```
Terminal / Command Prompt: gcc -o la LuciferA.c -lm
```
```
Terminal / Command Prompt: gcc -o lb LuciferB.c -lm
```
```
Terminal / Command Prompt: gcc -o lc LuciferC.c -lm
```
```
Terminal / Command Prompt: gcc 'pkg-config --cflag gtk+-3.0 -o lg Lucifer_Grapher.c 'pkg-config --libs gtk+-3.0' -lX11
```
```
Terminal / Command Prompt: gcc -o loader module_loader.c
```

## Usage
Enter on terminal:
```
Terminal / Command Prompt: ./loader
```
Follow the instructions on executing the program as it will provide the parameters
