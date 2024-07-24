# Descriere
Remote Dangerous Environment Assessment System (Sistem de Evaluare a Mediilor Periculoase la Distanță)
Sistemul RDEAS este destinat explorării și supravegherii mediilor inaccesibile sau periculoase pentru oameni. Setul RDEAS este compus dintr-un dispozitiv de control la distanță pentru navigație de bază și măsurarea senzorilor, Un set de 2 senzori interschimbabili (Contor Geiger și Senzor de umititate, temperatură și prezența gazelor explozibile), precum și robotul de supraveghere.

# Tehnologii
Sisteme mecanice:
Sistemul este împărțit în 2 părți:

Turela de scanare
Sistem de deplasare

Turela de scanare:
![IMG_6857](https://github.com/user-attachments/assets/fb876746-86c1-4fcc-8d84-4233f363d171)
![IMG_6860](https://github.com/user-attachments/assets/d713b0d3-b6f3-4ba8-b302-4fbeb5e25ca4)
Alcătuită din două motoare , comutatoare de limită și banca de senzori, aceasta se poate roti cu 300° pe orizontală iar banca de senzori cu 270° balans vertical. 
Sistem de deplasare:
Compus din 4 motoare, controlate câte 2 deodată pentru mișcare și viraj tip tanc.

# Sisteme Electronice:
Partea electronică este compusă din:

# Dispozitivul de control
Circuitul de navigare al robotului
![IMG_6854](https://github.com/user-attachments/assets/61a39e99-cce5-4d1a-955a-0ea42a5b1464)

![IMG_6855](https://github.com/user-attachments/assets/a22190a4-3c30-438c-ba00-1aeed2b25fbf)
Circuitul de interfață pentru senzori
![IMG_6856](https://github.com/user-attachments/assets/87849e49-9e56-488f-9ad3-bfd9d386758e)
Calculatorul de placă unică „Raspberry Pi 5”
![IMG_6852](https://github.com/user-attachments/assets/69280e86-920f-44b4-b9a7-fd9465638949)

Dispozitivul de control:
![IMG_6859](https://github.com/user-attachments/assets/8ce374ab-4768-4ea5-b10e-c6f06b4fe1e3)
Acesta se bazează pe un procesor xscale pocket pc, cu o frecvență de 400MHz, 64 MB de RAM și 64 de ROM, cu o interfață intermediară bazată pe MCU-ul atmel care permite depanarea ușoară și utilizarea unui afișaj de informații secundar. Portul serial DB-9 de pe dispozitiv este conform standardelor RS-232 și poate comunica cu majoritatea dispozitivelor care au o astfel de conexiune. Dispozitivul de control la distanță este echipat cu două baterii, una principală și una secundară. Pentru funcționare, este necesară doar prima. Dispozitivul de control la distanță beneficiază de aproximativ 12 ore de funcționare continuă din prima baterie, iar cea de-a doua baterie este opțională și este utilizată doar pentru a rula pocket pc-ul.

Circuitul de navigare al robotului: Acesta are rolul de control al sistemelor mecanice Pentru calibrarea poziției de 0 a comenzilor de rotire pentru turelă, se folosesc doi senzori magnetici. Banca de senzori este alcătuită dintr-un senzor ultrasonic de distanță, o cameră web și o termopilă digitală IR.

Circuitul de interfață pentru senzori:
Acesta are la bază chipul RP-2040, se ocupă prin interfațarea senzorului interschimbabil și a senzorilor de pe turelă. Acesta preia informațiile de la senzori și îi transmite prin USB la calculatorul de placă unică.

Calculatorul de placă unică „Raspberry Pi 5”:
Acesta se ocupă cu procesarea datelor și interpretarea acestora pentru operații autonome, totodată controlând sistemele mecanice și recepționând/transmițând informații de la/la telecomandă prin portul serial, sau prin Ethernet de la/la un calculator.

# Componenta Software:
Componentele software sunt împărțite pe 3 categorii: 1. Program telecomandă 2. Programul microcontroller robot Program telecomandă: Scris in C# pentru platforma Windows Mobile, utilizând sistemele .NET Compact pentru interfața grafică și alte funcții „Low-level”, este folosit pentru interacționarea dintre utilizator și robot. Aceasta are 2 funcții principale.

Terminal serial RS-232
Telecomandă robot Primul mod reprezintă o interfață pentru orice sistem cu o conecțiune serial RS-232, unde utilizatorul poate trimite și recepționa mesaje la/de la dispozitivul conectat, sau pentru depanarea robotului. Al doilea mod reprezintă o telecomandă simplificată a robotului, unde utilizatorul poate controla funcțiile robotului prin intermediul butoanelor de pe ecran.

Programele microcontrolerelor robotului: Aceasta are ca scop traducerea comenziilor de la telecomandă în semnale de acționare a motoarelor, interpretarea și transmiterea semnalelor de la senzori.

Programe pe SBC-ul Raspberry Pi 5 : Acestea se ocupă cu recepționarea comenzilor, interpretarea acestora, codificarea/decodificarea de date pentru transmitere, controlul sistemelor mecanice, interfață electronice senzori și afișaj cameră.

# Cerinte sistem
Operare de bază (Fără cameră, mod teleghidat): 
Doar telecomanda și robotul, cablu serial, Mufă DB-9, distanța maximă variază cu rata de baud.
Operare avansată:
Hardware: Laptop/Computer cu port Ethernet, Cablu Cat5 cu mufă Ethernet.
Software: Windows 7/8/8.1/10/11, Microsoft .NET (Latest)
