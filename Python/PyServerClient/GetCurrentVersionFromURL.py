import sys
from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import urllib.request

retry = 5

currver = sys.argv[1]

assignVar = 1
print("Getting Version Data...")

def end():
	print("exitting...")
	exit()



def GetDeeta():
	global assignVar
	global retry
	global currver
	if assignVar == 1:
		retry = 5
		assignVar = 0

	try:	




    

		page = "https://raw.githubusercontent.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/main/VersionControl/CurrentVersion"

		file = urllib.request.urlopen(page)
		deeta = "lol"

		for line in file:
			decoded_line = line.decode("utf-8")
			deeta = decoded_line
			print("Current newest Version: " + decoded_line)
			print("Current Game Version: " + currver)
			
			if(deeta != (currver + "\n")):
				print ("New Version Avaliable!")


		print("Done")
		retry = 0
		return deeta
		print (deeta)
	except:
		print ("Failed to get version information! retrying...")
		if retry >= 1:
			retry -= 1
			GetDeeta()
		else:
			print("Get data failed.")
			end()
		
		
		
GetDeeta()
