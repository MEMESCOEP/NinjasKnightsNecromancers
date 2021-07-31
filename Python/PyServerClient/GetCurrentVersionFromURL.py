from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import urllib.request
    

page = "https://raw.githubusercontent.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/main/VersionControl/CurrentVersion"

file = urllib.request.urlopen(page)
deeta = "lol"

for line in file:
	decoded_line = line.decode("utf-8")
	deeta = decoded_line
	print(decoded_line)

hostName = "localhost"
serverPort = 8080

class MyServer(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.send_header("Content-type", "text/html")
        self.end_headers()
        self.wfile.write(bytes("<html><head><title>https://pythonbasics.org</title></head>", "utf-8"))
        self.wfile.write(bytes("<p>Request: %s</p>" % self.path, "utf-8"))
        self.wfile.write(bytes("<body>", "utf-8"))
        self.wfile.write(bytes("<p>" + deeta + "</p>", "utf-8"))
        self.wfile.write(bytes("</body></html>", "utf-8"))

if __name__ == "__main__":        
    webServer = HTTPServer((hostName, serverPort), MyServer)
    print("Server started http://%s:%s" % (hostName, serverPort))

    try:
        webServer.serve_forever()
    except KeyboardInterrupt:
        pass

    webServer.server_close()
    print("Server stopped.")
