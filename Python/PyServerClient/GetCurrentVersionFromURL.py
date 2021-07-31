
from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import urllib.request

retry = 5

assignVar = 1


def end():
	print("exitting...")
	exit()



def GetDeeta():
	global assignVar
	global retry
	if assignVar == 1:
		retry = 5
		assignVar = 0

	try:	
		print("Getting Version Data...")



    

		page = "https://raw.githubusercontent.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/main/VersionControl/CurrentVersion"

		file = urllib.request.urlopen(page)
		deeta = "lol"

		for line in file:
			decoded_line = line.decode("utf-8")
			deeta = decoded_line
			print(decoded_line)



		print("Done")
	
	except:
		print ("Failed to get version information! retrying...")
		if retry >= 1:
			retry -= 1
			GetDeeta()
		else:
			print("Get data failed.")
			end()
		
		
		
GetDeeta()
