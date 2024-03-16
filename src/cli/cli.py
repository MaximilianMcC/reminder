import colorama
from colorama import *
import sys


def main():
	
	# Enable colors
	colorama.init()

	# Get, then parse the CLI arguments
	arguments = sys.argv

	# Check for what we wanna do
	#? Starting at [1] because [0] is path to file
	if arguments[1] == "list":
		print("Listing all reminders rn")

	if arguments[1] == "add":

		# Get input for the reminder
		reminder_text = arguments[2]
		print("Adding reminder for ")

	

if __name__ == "__main__":
	main()