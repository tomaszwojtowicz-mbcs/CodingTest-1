# Software Engineer Technical Test

## Background

One of the major jobs that we do is claims reserving, which involves assessing how much money is likely to be paid in future in respect of the claims that arise from a set of insurance policies that have already been taken out. The statistical analysis of this is based around data triangles of payment figures for the sum of previous claims.
 
A very simple example triangle might look like this (the triangle is the numbers in central grid):

	                            ╔═════════════════╗
	  Incremental Claims Data   ║ Development Year║
	                            ║─────┬─────┬─────║
	                            ║  1  │  2  │  3  ║
	╔═══════════════════════════╬═════════════════╣
	║                │   1995   ║ 100 │  50 │ 200 ║ 
	║                │──────────║─────┼─────┼─────║ 
	║   Origin Year  │   1996   ║  80 │  40 │  ?  ║
	║                │──────────║─────┼─────┼─────║ 
	║                │   1997   ║ 120 │  ?  │  ?  ║ 
	╚═══════════════════════════╩═════════════════╝
	
The numbers in the triangle are amounts of payments paid out for claims on a particular block of insurance policies.  
The block of claims is be broken into origin years. For example, all claims originally happening in 1996 will go into the 1996 row of the triangle.  
For each origin year, we will see the development of payments over time as settlements are made.  
In the example above we see that payments of 100 were made in respect or origin year 1995 during the year 1995, 50 during the year 1996 (development year 2) and 200 during 1997 (development year 3).  
Summing the values in an origin year gives the total claim payments. In the example above, at the end of 1997 the total of claim payments in respect of origin year 1995 is 350 = 100 + 50 + 200.

The purpose of the triangle is to try to estimate what numbers should go in the empty boxes (the ones with question marks in them) by looking at the patterns in the triangle – the numbers in the empty boxes are the amounts we expect to have to pay in the future in respect of the claims that occurred in 1996 and 1997.

To do this in practice, it is easier to spot patterns if we 'accumulate' the data in the triangle. Showing the total amount paid in all development years inclusively achives this. 

The accumulated version of the above triangle is:

	                            ╔═══════════════════════════════════════╗
	  Cumulative  Claims Data   ║           Development Year            ║
	                            ║─────┬────────────────┬────────────────║
	                            ║  1  │        2       │        3       ║
	╔═══════════════════════════╬═══════════════════════════════════════╣
	║                │   1995   ║ 100 │  150 (=100+50) │ 350 (=150+200) ║ 
	║                │──────────║─────┼────────────────┼────────────────║ 
	║   Origin Year  │   1996   ║  80 │  120 (=80+40)  │        ?       ║
	║                │──────────║─────┼────────────────┼────────────────║ 
	║                │   1997   ║ 120 │        ?       │        ?       ║ 
	╚═══════════════════════════╩═══════════════════════════════════════╝

## The Problem

You are required to create a program to read in incremental claims data from a comma-separated text file, to accumulate the data and output the results to a different comma-separated text file. This process represents the creation of the cumulative data triangle from the incremental data triangle above.

### Example Input Data

A short input file might contain the following:

	Product, Origin Year, Development Year, Incremental Value
	Comp, 1992, 1992, 110.0
	Comp, 1992, 1993, 170.0
	Comp, 1993, 1993, 200.0
	Non-Comp, 1990, 1990, 45.2
	Non-Comp, 1990, 1991, 64.8
	Non-Comp, 1990, 1993, 37.0
	Non-Comp, 1991, 1991, 50.0	
	Non-Comp, 1991, 1992, 75.0
	Non-Comp, 1991, 1993, 25.0
	Non-Comp, 1992, 1992, 55.0
	Non-Comp, 1992, 1993, 85.0
	Non-Comp, 1993, 1993, 100.0
	
This example file contains two triangles – one for a product called 'Comp' and one for a product called 'Non-Comp'.  
The first row contains column headings, and the subsequent rows contain the data. For example, accidents occurring on the Non-Comp product in 1990, 45.2 was paid in 1990, 64.8 was paid in 1991 and 37 was paid in 1993.

### Example Output Data

The output file corresponding to the above input file would be:

	1990, 4
	Comp, 0, 0, 0, 0, 0, 0, 0, 110, 280, 200
	Non-Comp, 45.2, 110, 110, 147, 50, 125, 150, 55, 140, 100

The first line gives the earliest origin year (i.e. 1990) and the number of development years (in this case ranging from 1990 through to 1993 i.e. 4).  
Subsequently there is a line for each triangle. The first field in the line gives the name of the product. The subsequent fields are the accumulated triangle values.

## General Considerations

Below are some hints to help with this exercise. 
-   Demonstrate engineering best practice 
-   Use the pull request to provide a 'self review' to highlight any assumptions or potential future refactoring
-   The data may well contain more than two triangles and there may be many more than four origin years
-   The example input data file given is very ‘clean’. In practice data files may contain errors or the data may be in an unexpected format
-   Incremental values in the input data may have been left out of the input file if they are zero. Origin years with no claims may have been left out of the input file

## Running instructions

Once compliled, execute the binary produced without any command line parameters.

The user will be prompted to enter the name of file containing input data. Both full path and just the filename are valid inputs; if just the filename is provided, the application will look for the file in the same directory where the binary has been executed from.

The output can be found in file 'Output.csv', located in the execution directory.
