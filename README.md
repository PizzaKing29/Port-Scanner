# Port Scanner by PizzaKing29

A simple asynchronous port scanner written in C# that checks for open TCP ports on a specified IP address within a user-defined port range. Uses parallel scanning with controlled concurrency for faster results.

---

## Features

- Scans ports from 0 up to a specified maximum port number.
- Asynchronously checks ports using `TcpClient.ConnectAsync`.
- Parallel scanning with configurable maximum concurrency (`MaxDegreeOfParallelism` set to 250).
- Displays a live loading animation with progress.
- Lists all detected open ports after scanning completes.
- Validates user input for IP addresses and port ranges.
- Clean console UI for easy use.

---

## Usage

1. Run the program.
2. Enter the maximum port number you want to scan (up to 65535).
3. Enter the target IP address to scan.
4. Wait while the program scans ports in parallel.
5. View the list of open ports detected.

---

## Example

