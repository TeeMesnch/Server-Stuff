import socket

class Client:

    def Main():
        Client.StartClient()

    def StartClient():
        global sock

        port = 4200
        ip = "127.0.0.1"
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.connect((ip, port))
        print(f"Connected to Server (port: 4200)")
        Client.SendData()


    def StopClient():
        sock.close()
        print("Connection closed")

    def SendData():
        sock.sendall(b"Hello, Server!")
        data = sock.recv(1024)
        print(f"Received data (message: {data.decode('utf-8')})")
        Client.StopClient()

if __name__ == "__main__":
    Client.Main()