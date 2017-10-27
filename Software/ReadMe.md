Image Comparision
--------------
# Purpose
Image Comparision was developed with the intentions of allowing two images of the same size and which are of type Bitmap to compare each other. This is very useful as the if the spectra was in an image form, this allows them to be compared still, without needing the raw data which means Key.Coli can work with graphs.

# Installation 
Image Comparision was developed using C#. Currently, stable.

```
Simply click the .exe file to download it. This will only work on Windows. If you want, you can download the source .zip file and port it over to your machine of choice.
```

# Usage
Select the image you'd like to use, then select the second. They must be the same size and be bitmaps.
Furthermore, to change the threshold, change the number then click the button. It will not change unless you do that.
Hit Compare to check for similarity and to know if you are verified.

Raw Data Comparision
--------------
# Purpose
This was developed with the intenton of allowing comparision between two sets of data pulled from a fluorescence reader in the lab to verify whether the user is let in or not. This was developed in Java, which is supported by all machines. This allowed it to be ported to a Raspberry Pi. 


# Installation 
Raw Data Comparision, at the current state, is unstable. Please download the source code and manually change the code to support your own file directory.

```
LINUX: To compile, in Terminal, navigate to directory with .java files and type: java read_gfp.java
```
```
LINUX: To run, in Terminal, navigate to directory with .class files and type: javac read_gfp
```

# Usage
HAVEN'T TESTED ON WINDOWS / MAC

Use CVS format. Make sure the directory goes to that .cvs file. This .cvs file must be from the fluorescence reader to work. Make sure the format matches the code. Then run.

In Dev
------------
These are old builds / experiments. Unsupported but kept for a reference.
