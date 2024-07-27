#include <iostream>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <string>
#include <cstring>
#include <wiringPi.h>
#include <libserialport.h>
#include <thread>
#include <sstream>

#define Right_T_bridge_Forwards 12
#define Right_T_bridge_Backwards 13
#define Left_T_bridge_Forwards 14
#define Left_T_bridge_Backwards 21
#define Rot_Stp 2
#define Rot_Dir 3
#define Pit_Stp 22
#define Pit_Dir 23
#define Sw_Rot_Lim_1 7
#define Sw_Rot_Lim_2 9
#define Sw_Pit_Lim_1 8
#define Sw_Pit_Lim_2 0

char PinMotorCommand = 'S';
bool StepperCommandEnable = false;
bool AlreadyStopped = true;
bool AlreadyStoppedRotation = true;

sp_port *port1;
sp_port *port2;
sp_port *raspberryPiUartPort; 

// Function prototypes
void configure_port(sp_port *port);
void initialize_serial_ports();
void close_serial_ports();
void SendUSBUART1(const std::string &message);
void SendUSBUART2(const std::string &message);
std::string ReceiveUSBUART(sp_port *port);
void handleRotCommand(char movCommand);
void StepperMovementHandler(char movCommand);
void handleMovCommand(char movCommand);
void handleSNSCommand(char command);
void interpretMessage(const std::string& message);
void forwardMessageToClient(const std::string &message, int client_socket);
void handleRaspberryPiUartCommunication();

void configure_port(sp_port *port) {
    sp_set_baudrate(port, 9600);
    sp_set_bits(port, 8);
    sp_set_parity(port, SP_PARITY_NONE);
    sp_set_stopbits(port, 1);
    sp_set_flowcontrol(port, SP_FLOWCONTROL_NONE);
}

void initialize_serial_ports() {
    if (sp_get_port_by_name("/dev/ttyACM1", &port1) != SP_OK) {
        std::cerr << "Failed to get port /dev/ttyACM1" << std::endl;
        exit(1);
    }
    if (sp_open(port1, SP_MODE_READ_WRITE) != SP_OK) {
        std::cerr << "Failed to open port /dev/ttyACM1" << std::endl;
        exit(1);
    }
    configure_port(port1);

    if (sp_get_port_by_name("/dev/ttyACM0", &port2) != SP_OK) {
        std::cerr << "Failed to get port /dev/ttyACM0" << std::endl;
        sp_close(port1);
        exit(1);
    }
    if (sp_open(port2, SP_MODE_READ_WRITE) != SP_OK) {
        std::cerr << "Failed to open port /dev/ttyACM0" << std::endl;
        sp_close(port1);
        exit(1);
    }
    configure_port(port2);

    if (sp_get_port_by_name("/dev/ttyAMA0", &raspberryPiUartPort) != SP_OK) {
        std::cerr << "Failed to get port /dev/ttyAMA0" << std::endl;
        sp_close(port1);
        sp_close(port2);
        exit(1);
    }
    if (sp_open(raspberryPiUartPort, SP_MODE_READ_WRITE) != SP_OK) {
        std::cerr << "Failed to open port /dev/ttyAMA0" << std::endl;
        sp_close(port1);
        sp_close(port2);
        exit(1);
    }
    configure_port(raspberryPiUartPort);
}

void close_serial_ports() {
    sp_close(port1);
    sp_close(port2);
    sp_close(raspberryPiUartPort);
    sp_free_port(port1);
    sp_free_port(port2);
    sp_free_port(raspberryPiUartPort);
}

void SendUSBUART1(const std::string &message) {
    sp_nonblocking_write(port1, message.c_str(), message.length());
}

void SendUSBUART2(const std::string &message) {
    sp_nonblocking_write(port2, message.c_str(), message.length());
}

std::string ReceiveUSBUART(sp_port *port) {
    char buffer[1024];
    int bytes_read = sp_blocking_read(port, buffer, sizeof(buffer) - 1, 1000);
    if (bytes_read > 0) {
        buffer[bytes_read] = '\0';
        return std::string(buffer);
    }
    return "";
}

void handleRotCommand(char movCommand) {
    StepperCommandEnable = true;
    PinMotorCommand = movCommand;
    std::cout << "Handling ROT command: " << movCommand << std::endl;
}

void StepperMovementHandler(char movCommand) {
    if (movCommand == 'B') {
        digitalWrite(Rot_Dir, HIGH);
        digitalWrite(Rot_Stp, HIGH);
        delay(2);
        digitalWrite(Rot_Stp, LOW);
        digitalWrite(Rot_Dir, LOW);
    } else if (movCommand == 'D') {
        digitalWrite(Pit_Dir, HIGH);
        digitalWrite(Pit_Stp, HIGH);
        delay(2);
        digitalWrite(Pit_Stp, LOW);
        digitalWrite(Pit_Dir, LOW);
    } else if (movCommand == 'A') {
        digitalWrite(Rot_Stp, HIGH);
        delay(2);
        digitalWrite(Rot_Stp, LOW);
    } else if (movCommand == 'C') {
        digitalWrite(Pit_Stp, HIGH);
        delay(2);
        digitalWrite(Pit_Stp, LOW);
    }
}

void handleMovCommand(char movCommand) {
    std::cout << "Handling MOV command: " << movCommand << std::endl;
    if (movCommand == 'F') {
        digitalWrite(Right_T_bridge_Backwards, LOW);
        digitalWrite(Left_T_bridge_Backwards, LOW);
        digitalWrite(Right_T_bridge_Forwards, HIGH);
        digitalWrite(Left_T_bridge_Forwards, HIGH);
    } else if (movCommand == 'B') {
        digitalWrite(Right_T_bridge_Forwards, LOW);
        digitalWrite(Left_T_bridge_Forwards, LOW);
        digitalWrite(Right_T_bridge_Backwards, HIGH);
        digitalWrite(Left_T_bridge_Backwards, HIGH);
    } else if (movCommand == 'S') {
        digitalWrite(Right_T_bridge_Backwards, LOW);
        digitalWrite(Left_T_bridge_Backwards, LOW);
        digitalWrite(Right_T_bridge_Forwards, LOW);
        digitalWrite(Left_T_bridge_Forwards, LOW);
        digitalWrite(Pit_Dir, LOW);
        digitalWrite(Pit_Stp, LOW);
        digitalWrite(Rot_Dir, LOW);
        digitalWrite(Rot_Stp, LOW);
    } else if (movCommand == 'L') {
        digitalWrite(Right_T_bridge_Backwards, HIGH);
        digitalWrite(Left_T_bridge_Backwards, LOW);
        digitalWrite(Right_T_bridge_Forwards, LOW);
        digitalWrite(Left_T_bridge_Forwards, HIGH);
    } else if (movCommand == 'R') {
        digitalWrite(Right_T_bridge_Backwards, LOW);
        digitalWrite(Left_T_bridge_Backwards, HIGH);
        digitalWrite(Right_T_bridge_Forwards, HIGH);
        digitalWrite(Left_T_bridge_Forwards, LOW);
    }
}

void handleSNSCommand(char command) {
    std::cout << "Handling SNS command: " << command << std::endl;
    if (command == 'L') {
        SendUSBUART1("LST");
    }
    if (command == 'R') {
        SendUSBUART1("MSR");
    }
    if (command == 'T') {
        SendUSBUART2("MSR");
    }
}

void ExtractNumbers(const std::string& MSG, int& RX, int& RY, int& LX, int& LY) {
    std::istringstream iss(MSG);
    std::string keyValue;

    while (iss >> keyValue) {
        std::size_t pos = keyValue.find('=');
        if (pos != std::string::npos) {
            std::string key = keyValue.substr(0, pos);
            int value = std::stoi(keyValue.substr(pos + 1));

            if (key == "RX") {
                RX = value;
            } else if (key == "RY") {
                RY = value;
            } else if (key == "LX") {
                LX = value;
            } else if (key == "LY") {
                LY = value;
            }
        }
    }
}

int AmIGaming(const std::string& MSG) {
    bool containsRX = MSG.find("RX") != std::string::npos;
    bool containsRY = MSG.find("RY") != std::string::npos;
    bool containsLX = MSG.find("LX") != std::string::npos;
    bool containsLY = MSG.find("LY") != std::string::npos;
    return containsRX && containsRY && containsLX && containsLY ? 1 : 0;
}

void interpretMessage(const std::string& message) {
    size_t pos = 0;
    size_t messageLength = message.length();

    while (static_cast<ssize_t>(pos) < static_cast<ssize_t>(messageLength)) {
        if (message.compare(pos, 3, "MOV") == 0) {
            char command = message[pos + 3];
            handleMovCommand(command);
            pos += 4;
        } else if (message.compare(pos, 3, "ROT") == 0) {
            char command = message[pos + 3];
            handleRotCommand(command);
            pos += 4;
        } else if (message.compare(pos, 3, "SNS") == 0) {
            char command = message[pos + 3];
            handleSNSCommand(command);
            pos += 4;
        } else {
            pos++;
        }
    }
}

void forwardMessageToClient(const std::string &message, int client_socket) {
    send(client_socket, message.c_str(), message.length(), 0);
}

void handleRaspberryPiUartCommunication() {
    while (true) {
        std::string serialMessage = ReceiveUSBUART(raspberryPiUartPort);

        if (!serialMessage.empty()) {
            //std::cout << "Received from Raspberry Pi UART: " << serialMessage << std::endl;
            int RX, RY, LX, LY;
            ExtractNumbers(serialMessage, RX, RY, LX, LY);
            if(!(LX < 530 && LX > 500 && LY < 530 && LY>490)){
                AlreadyStoppedRotation=false;
                if (LY > 550) interpretMessage("ROTA");
                if (LY < 500) interpretMessage("ROTB");
                else interpretMessage("MOVS");
                if (LX < 500) interpretMessage("ROTC");
                else if (LX > 530) interpretMessage("ROTD");
            }
            else{
                if(!AlreadyStoppedRotation){
                    interpretMessage("MOVS");
                    AlreadyStoppedRotation=true;
                }
            }

            if(!(RX < 530 && RX > 500 && RY < 530 && RY>500)){
                AlreadyStopped=false;
                if (RX < 530 && RX > 500) {
                    if (RY > 550) interpretMessage("MOVF");
                    if (RY < 500) interpretMessage("MOVB");
                }
                else interpretMessage("MOVS");
                if (RX < 500) interpretMessage("MOVL");
                else if (RX > 530) interpretMessage("MOVR");
            }
            else{
                if(!AlreadyStopped){
                    interpretMessage("MOVS");
                    AlreadyStopped=true;
                }
            }
            
        }
    }
}

void handleSerialCommunication(int client_socket) {
    while (true) {
        std::string serialMessage1 = ReceiveUSBUART(port1);
        std::string serialMessage2 = ReceiveUSBUART(port2);

        if (!serialMessage1.empty()) {
            std::cout << "Received from Microcontroller 1: " << serialMessage1 << std::endl;
            forwardMessageToClient(serialMessage1 + '\n', client_socket);
        }

        if (!serialMessage2.empty()) {
            std::cout << "Received from Microcontroller 2: " << serialMessage2 << std::endl;
            forwardMessageToClient(serialMessage2+ '\n', client_socket);
        }
    }
}

void handleNetworkCommunication(int client_socket) {
    char buffer[1024] = {0};
    while (true) {
        ssize_t bytes_read = read(client_socket, buffer, sizeof(buffer) - 1); 
        if (bytes_read <= 0) {
            if (bytes_read == 0) {
                std::cout << "Client disconnected" << std::endl;
            } else {
                perror("Read failed");
            }
            close(client_socket);
            break;
        }
        buffer[bytes_read] = '\0';
        std::string MSG(buffer);

        if (AmIGaming(MSG)) {
            int RX, RY, LX, LY;
            ExtractNumbers(MSG, RX, RY, LX, LY);
            std::string response;

            if(!(LX < 530 && LX > 500 && LY < 530 && LY>490)){
                AlreadyStoppedRotation=false;
                if (LY > 550) interpretMessage("ROTA");
                if (LY < 500) interpretMessage("ROTB");
                else interpretMessage("MOVS");
                if (LX < 500) interpretMessage("ROTC");
                else if (LX > 530) interpretMessage("ROTD");
                if (!response.empty()) {
                    std::strncpy(buffer, response.c_str(), sizeof(buffer) - 1);
                    buffer[sizeof(buffer) - 1] = '\0';
                }
            }
            else{
                if(!AlreadyStoppedRotation){
                    interpretMessage("MOVS");
                    AlreadyStoppedRotation=true;
                }
            }

            if(!(RX < 530 && RX > 500 && RY < 530 && RY>500)){
                AlreadyStopped=false;
                if (RX < 530 && RX > 500) {
                    if (RY > 550) interpretMessage("MOVF");
                    if (RY < 500) interpretMessage("MOVB");
                }
                else interpretMessage("MOVS");
                if (RX < 500) interpretMessage("MOVL");
                else if (RX > 530) interpretMessage("MOVR");
                if (!response.empty()) {
                    std::strncpy(buffer, response.c_str(), sizeof(buffer) - 1);
                    buffer[sizeof(buffer) - 1] = '\0';
                }
            }
            else{
                if(!AlreadyStopped){
                    interpretMessage("MOVS");
                    AlreadyStopped=true;
                }
            }
        }

        interpretMessage(MSG);
        if (StepperCommandEnable) {
            StepperMovementHandler(PinMotorCommand);
            StepperCommandEnable = false; 
        }
    }
}

int main() {
    if (wiringPiSetup() == -1) {
        printf("WiringPi setup failed\n");
        return 1;
    }
    // Initialize pin block start
    pinMode(Right_T_bridge_Forwards, OUTPUT);
    pinMode(Right_T_bridge_Backwards, OUTPUT);
    pinMode(Left_T_bridge_Forwards, OUTPUT);
    pinMode(Left_T_bridge_Backwards, OUTPUT);
    pinMode(Rot_Stp, OUTPUT);
    pinMode(Rot_Dir, OUTPUT);
    pinMode(Pit_Stp, OUTPUT);
    pinMode(Pit_Dir, OUTPUT);
    pinMode(Sw_Pit_Lim_1, INPUT);
    pinMode(Sw_Pit_Lim_2, INPUT);
    pinMode(Sw_Rot_Lim_1, INPUT);
    pinMode(Sw_Rot_Lim_2, INPUT);
    // Initialize pin block end

    int server_fd, new_socket;
    struct sockaddr_in address;
    int opt = 1;
    int addrlen = sizeof(address);

    // Create a socket
    if ((server_fd = socket(AF_INET, SOCK_STREAM, 0)) == 0) {
        perror("Socket creation failed");
        exit(EXIT_FAILURE);
    }

    // Set socket options
    if (setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR | SO_REUSEPORT, &opt, sizeof(opt))) {
        perror("Setsockopt failed");
        exit(EXIT_FAILURE);
    }

    // Bind the socket to localhost:8080
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(8080);
    if (bind(server_fd, (struct sockaddr*)&address, sizeof(address)) < 0) {
        perror("Bind failed");
        exit(EXIT_FAILURE);
    }

    // Listen for connections
    if (listen(server_fd, 3) < 0) {
        perror("Listen failed");
        exit(EXIT_FAILURE);
    }

    // Initialize serial ports
    initialize_serial_ports();

    // Start thread to handle Raspberry Pi UART communication
    std::thread raspberryPiUartThread(handleRaspberryPiUartCommunication);
    raspberryPiUartThread.detach();

    while (true) {
        if ((new_socket = accept(server_fd, (struct sockaddr*)&address, (socklen_t*)&addrlen)) < 0) {
            perror("Accept failed");
            exit(EXIT_FAILURE);
        }

        std::thread serialThread(handleSerialCommunication, new_socket);
        std::thread networkThread(handleNetworkCommunication, new_socket);

        serialThread.detach();
        networkThread.detach();
    }

    close_serial_ports();

    return 0;
}
