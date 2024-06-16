# RDEAS
Remote Dangerous Environment Assessment System (Sistem de Evaluare a Mediilor Periculoase la Distanță)
Descrierea succintă a proiectului: 
Sistemul RDEAS este destinat explorării și supravegherii mediilor inaccesibile sau periculoase pentru oameni. Setul RDEAS este compus dintr-un dispozitiv de control la distanță pentru navigație de bază și măsurarea senzorilor, precum și robotul de supraveghere.
# Sisteme mecanice: 
Sistemul este împărțit în 2 părți:
1.	Turela de scanare
2.	Sistem de deplasare

Turela de scanare
Alcătuită din două motoare , comutatoare de limită și banca de senzori, aceasta se poate roti cu 300° pe orizontală iar banca de senzori cu 270° baleaj vertical.
Sistem de deplasare
Compus din 4 motoare, controlate câte 2 deodată pentru mișcare și viraj tip tanc.
# Sisteme Electronice: 
Partea electronică este compusă din:
1.	Dispozitivul de control
2.	Circuitul de navigare al robotului
   
Dispozitivul de control:
Acesta se bazează pe un procesor xscale pocket pc, cu o frecvență de 400MHz, 64 MB de RAM și 64 de ROM, cu o interfață intermediară bazată pe MCU-ul atmel care permite depanarea ușoară și utilizarea unui afișaj de informații secundar.
Portul serial DB-9 de pe dispozitiv este conform standardelor RS-232 și poate comunica cu majoritatea dispozitivelor care au o astfel de conexiune.
Dispozitivul de control la distanță este echipat cu două baterii, una principală și una secundară. Pentru funcționare, este necesară doar prima. Dispozitivul de control la distanță beneficiază de aproximativ  12 ore de funcționare continuă din prima baterie, iar cea de-a doua baterie este opțională și este utilizată doar pentru a rula pocket pc-ul.

Circuitul de navigare al robotului:
Acesta este construit în jurul plăcii de dezvoltare Arduino Pro Micro, cu rolul de control al sistemelor mecanice și procesarea semnalelor recepționate de la senzori în valori compatibile cu dispozitivul de control.
Pentru calibrarea poziției de 0 a comenzilor de rotire pentru turelă, se folosesc doi senzori magnetici.
Banca de senzori este alcătuită dintr-un senzor ultrasonic de distanță și o termopilă  digitală IR.
# Componenta Software:
Componentele software sunt împărțite pe 2 categorii:
	1. Program telecomandă
	2. Programul microcontroller robot
Program telecomandă:
Scris in C# pentru platforma Windows Mobile, utilizând sistemele .NET Compact pentru interfața grafică și alte funcții „Low-level”, este folosit pentru interacționarea dintre utilizator și robot.
Aceasta are 2 funcții principale: 
1.	Terminal serial RS-232
2.	Telecomandă robot
Primul mod reprezintă o interfață pentru orice sistem cu o conecțiune serial RS-232, unde utilizatorul poate trimite și recepționa mesaje la/de la dispozitivul conectat, sau pentru depanarea robotului.
Al doilea mod reprezintă o telecomandă simplificată a robotului, unde utilizatorul poate controla funțiile robotului prin intermediul butoanelor de pe ecran.

Program microcontroller robot:
Aceasta are ca scop traducerea comenziilor de la telecomandă în semnale de acționare a motoarelor, interpretarea și transmiterea semnalelor de la senzori.

