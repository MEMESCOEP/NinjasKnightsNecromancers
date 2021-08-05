import sys
#from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import urllib.request

retry = 4

try:
	currver = sys.argv[1]
except:
	print("CURRENT VERSION INFORMATION NOT AVALIABLE. Please enter version: ")
	currver = input(">> ")
	#time.sleep(1)
	

assignVar = 1


def end():
	print("exitting...")
	exit()


sayget = 0


def GetDeeta():
	global sayget
	if sayget == 0:
		
		print("Getting Version Data...")
		sayget = 1
	global assignVar
	global retry
	global currver
	if assignVar == 1:
		retry = 5
		assignVar = 0

	try:	

		page = "https://raw.githubusercontent.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/main/VersionControl/CurrentVersion"

		file = urllib.request.urlopen(page)
		deeta = ""

		for line in file:
			decoded_line = line.decode("utf-8")
			deeta = decoded_line
			print("Newest Version: " + decoded_line)
			print("Current Game Version: " + currver)
			
			if(deeta != (currver + "\n")):
				print ("New Version Avaliable!")

		try:
			print("Writing Version Data to File...")
			filetowrite = open("CurrVer.txt", "w")
			filetowrite.write(deeta)
			filetowrite.close()
			print("Done Writing.")
		except:
			print("Failed to write data to file!")
			
			
		#print("Finished.")
		retry = 0
		try:
			
			sys.exit(0)
			
		except:
			print("Non fatal error: exit() failed.")
			return deeta
		return deeta
		print (deeta)
		
	except:
		print ("Failed to get version information! retrying... (", retry, ")")
		if retry > 1:
			retry -= 1
			if retry == 1:
				1+1
				#print("FINAL ATTEMPT!")
			time.sleep(1)
			GetDeeta()
		else:
			print("Getting data failed.")
			end()
			
print("PROGRAM STARTING...")
GetDeeta()
