# Simple Port/Network Info Tool

A small C# console app that checks whether a given host and port are reachable over TCP.

## What it does

You enter a hostname or IP address and a port number. The tool attempts a TCP connection and reports one of:

- **OPEN** — the connection succeeded, something is listening on that port
- **CLOSED** — the host responded but actively refused the connection
- **TIMEOUT** — no response at all within 3 seconds (host unreachable, packet dropped by a firewall, or filtered port)

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (targets .NET 10)

## Running it

```bash
cd NetworkInfoTool
dotnet run
```

Then enter a host and port when prompted:

```
=== Simple Port/Network Info Tool ===
Enter hostname or IP: google.com
Enter port number: 443
[OPEN] google.com:443 is reachable.
```

## Example targets to try

| Host | Port | Expected result |
|---|---|---|
| google.com | 443 | OPEN |
| 8.8.8.8 | 53 | OPEN |
| google.com | 81 | CLOSED |
| 10.255.255.1 | 443 | TIMEOUT |

## Notes

This checks TCP port reachability, not ICMP ping — it's asking "is a service listening on this specific port?" rather than "is this machine alive?"