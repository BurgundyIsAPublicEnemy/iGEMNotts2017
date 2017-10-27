Lucifer Protein Simulation
--------------
# Purpose
This simulation was created combining all the models to help build different components.
It was created to help the wet lab predict how the spectra might look as well as create sample data for the Raw Data and 
Image Comparision verification.

# Installation 
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

# Usage
Enter on terminal:
```
Terminal / Command Prompt: ./loader
```
And follow the on screen prompts.


Python Models
--------------
# Purpose
This was developed to test the Ordinary Diffrential Equations by creating visualisations of them.

# Installation 
Prequisites: You need Tellurium and Python

```
LINUX: To install Tellurium, use: http://tellurium.analogmachine.org/
```

# Usage
Run the scripts onto any Python IDE of your choice once Tellurium is loaded and it should work.

Data For Programs
------------
Contains data from Semrock's Fluorescence Spectra Grapher: https://searchlight.semrock.com/
Simply click to see the data.
