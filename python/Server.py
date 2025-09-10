import socket

class Server:

    def Main():
        Server.StartServer()

    def StartServer():
        global sock, adress, ip, port

        port = 4200
        ip = "127.0.0.1"
        adress = (ip, port)

        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.bind((ip, port))

        print(f"Started Server (port: 4200) (ip: 127.0.0.1)")
        Server.RunServer()


    def RunServer():
        while True:
            sock.listen(5)
            socket_client, address = sock.accept()
            print(f"\nConnection from client (adress: {address})")
            data = socket_client.recv(1024)
            print(f"Received data (message: {data.decode("utf-8")})")
            socket_client.sendall(b"Hello, Client!")
            print("send data to client\n")

            if KeyboardInterrupt:
                Server.StopServer()
                break

    def StopServer():
        sock.close()
        print("Server closed from user")
        return



if __name__ == "__main__":
    Server.Main()
