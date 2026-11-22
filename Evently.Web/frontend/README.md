# Evently Web — Scoped Frontend

An ASP.NET Core Razor Pages (`net9.0`) frontend limited to the requested Evently website scope: guest self-registration (`/Register`), QR ticket view (`/Ticket/{ticket_id}`), staff login and phone-camera scanning for check-in/check-out (`/Login`, `/Scan`), and post-event review submission (`/Review/{ticket_id}`). The ASP.NET Core app binds to `http://0.0.0.0:5000` so it can listen on LAN interfaces.

This task is frontend-only. No database, backend services, APIs, or real authentication are included. Local browser state is only for clearly labelled UI previews and is not an Evently registration, valid ticket, authenticated session, real attendance record, or submitted review. Do not enter real personal data or credentials. The app includes no marketplace, payment/pricing, general homepage, logout page, or error page. Visiting `/` redirects to `/Register`.

## Run

With .NET 9 installed, run from this directory:

```sh
dotnet restore
dotnet run --no-launch-profile --urls http://0.0.0.0:5000
```

The WebDev runtime uses the same command. From a phone on the same LAN, navigate to the host computer's LAN address on port 5000 and open `/Register`, `/Login`, `/Scan`, `/Ticket/{ticket_id}`, or `/Review/{ticket_id}`.

Sample event names and Philippine venues are fictional and not verified schedules.
