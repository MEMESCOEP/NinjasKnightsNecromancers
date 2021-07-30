#Python 3
import urllib.request
url = "https://www.cs.cmu.edu/~./enron/enron_mail_20150507.tgz"
print ("download start!")
filename, headers = urllib.request.urlretrieve(url, filename="e:\\PythonStuff\\enron_mail_20150507.tgz")
print ("download complete!")
print ("download file location: ", filename)
print ("download headers: ", headers)
