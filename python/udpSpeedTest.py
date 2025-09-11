import socket
import time

class UdpSpeedTest:
    def udpClient(message: str, count: int, host: str = "127.0.0.1", port: int = 4200):
        sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        adress = (host, port)

        try:
            print(f"Sending {count} UDP packet to {host} : {port}")

            startTimeOverall = time.time()

            for i in range(count):
                data = f"{message} #{i}".encode()

                startTime = time.time()
                sock.sendto(data, adress)
                print(f"Sent packet {i} : {count}")
                try:
                    response, addr = sock.recvfrom(1024)
                    endTime = time.time()
                    rtt = (endTime - startTime) * 1000
                    print(f"Response : {response.decode()} (RTT : {rtt:.2f} ms)")
                except socket.timeout:
                    print(f"No response (packet : {i}) (timeout).")

            endTimeOverall = time.time()
            totalTime = (endTimeOverall - startTimeOverall) * 1000
            print(f"\nTotal elapsed time : {totalTime:.2f} ms")
        finally:
            sock.close()

if __name__ == "__main__":
    UdpSpeedTest.udpClient("Hello, server", count=1000)