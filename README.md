# TSAPIClient
TSAPI .NET Client w/Demo

This project is a C# .NET based TSAPI client for connecting to Avaya phone systems for the purpose of monitoring and controlling extensions.

It has been used in production for over 3 years for a screen pop application, as well as for monitoring calls queued to hunt groups.

I am no longer developing this project. I am open sourcing it here so it can of use to others.

There is a demo app that shows how to connect to the phone system, monitor an extension, dial and hang up. A lot more can be done with the TSAPIClient though.

This project requires some 3rd party libraries from Avaya to work. Follow these steps to acquire them:

1. Download Avaya Aura AE Services TSAPI Client MS Windows 7.1 from the Avaya support website. It should be named something like this: tsapi-client-win32-7.1.0-67.zip
2. Extract the downloaded zip file.
3. In the extracted folder is file named Data1.cab. Extract it like a zip archive.
4. The four following files are required: aes_libeay32.dll, aes_ssleay32.dll, attprv32.dll, and csta32.dll. Copy them into the output directory of the TSAPIClient project.
5. Rename aes_libeay32.dll to AES-LIBEAY32.dll. Rename aes_ssleay32.dll to AES-SSLEAY32.dll.
6. Also copy the TSLIB.INI file from the extracted zip archive into the output directory of the TSAPIClient project. This file will need to contain the IP and port of the Avaya phone system in the Telephony Servers section. It should look something like this:

[Telephony Servers]
;
; List your Telephony Servers and Application Enablement (AE) Services
; servers that offer TSAPI Telephony Services above.
;
; Each entry must have the following format:
;
; host_name=port_number
;
; where:
;
; - host_name is either the domain name or IP address of the AE Services
;   server.
; - port_number is the TSAPI Service port number.  The default port number
;   used by AE Services is 450.
;
; For example:
;
; aeserver.mydomain.com=450
172.16.13.27=450