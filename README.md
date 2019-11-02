# NubianVR_Learn - Basic Electronics
Setting up the Basic Electronics Demo
This Demo is a demonstration of an implementation of the Basic Electronics in VR made with the Unity Game Engine and created for the Oculus Go Device. Along with the document is an APK that is going to be transferred to the Oculus Go.

Windows Users
Steps to Transferring the APK to the Oculus Go
Enable Developer Mode
First Go to Oculus Dashboard website and create an organization. The name of the Organisation can be anything https://dashboard.oculus.com/organization/create/ if you are not logged in, login with your facebook account linked to the Oculus Device.

Go the Oculus App on your phone, Make sure your Oculus Go device is turned on though. 
Head to Settings on the Oculus App on the phone. 
Click on the Oculus Device to confirm its connected to the device.
Click on more settings and choose Developer mode
Click on the switch to enable the developer mode



Android Device Bridge
Download the content in the dropbox. This contains the files need to the Android Device Bridge. https://www.dropbox.com/s/pk4e0lmwim5vgi0/Adb.rar?dl=0
Unzip and Paste the platform-tools folder you have in a location Downloads

Connect your Oculus Device(...press Enter after you type.)
Go to your command prompt
Type: cd Downloads 
Type: adb devices
If you see a device under List devices attached, it means it has detected your Oculus Device.
Now wear the Headset and use your Oculus Controller to authorize connection to the PC you are using..
Type: adb devices again and you will realize you will see the serial number of the VR headset connected.
Copy the *Basic Electronics.apk* to the platform-tools folder. The folder that has the adb file.
Type adb install Basic_Electronics.apk
When you see success, you are done
In your VR headset, go to Library, and then Unknown Sources to find your app there and then run it.

Connecting to Vysor.
Download and install Vysor to your PC. (https://www.vysor.io/download/)
Install the application downloaded.
Run it and you will see this Dialog box. below
With the Head set connect click on Find Devices and it should find it in a sec.
Click on the View button to see a mirrored view of the content in the headset.


Mac Users (https://headjack.io/tutorial/sideload-install-app-apk-oculus-go-quest/)
Open Terminal
Paste ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)" to install homebrew .. this takes a while to install so be patient
After that, Paste brew cask install android-platform-tools
When this is done, plug in your Headset and type, adb devices. If it shows up as unauthorize, check your headset and authorize the device.
Now copy the apk to the downloads folder your mac and then from your terminal, type cd Downloads 
Type adb install Basic_Electronics.apk
success!






https://www.youtube.com/watch?v=Eiz5WKObDeA - This Youtube provides a guide to complete the same task for windows and Mac
