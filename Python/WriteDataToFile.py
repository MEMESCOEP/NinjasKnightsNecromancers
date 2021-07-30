import sys
ArgList = sys.argv

file1 = open("Data", "w")  
file1.writelines(ArgList)
file1.close()
