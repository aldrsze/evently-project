# Evently — Narrow Scope Checklist

- [x] ASP.NET Core Razor Pages project targeting `net9.0`, configured to listen on `http://0.0.0.0:5000` for LAN use.
- [x] Guest self-registration at `/Register`, including event selection and the PRD attendee fields (first name, last name, email, optional birthdate) with validation.
- [x] QR ticket view at `/Ticket/{ticket_id}`, showing ticket ID, QR, attendee and event information, with clear demo-only labeling.
- [x] Staff login interface at `/Login`; no real authentication is claimed or simulated as a secured session.
- [x] Mobile staff scanning interface at `/Scan`, using the `html5-qrcode` library where loaded and presenting check-in, check-out, invalid, and repeat-use preview states without an event selector.
- [x] Guest review submission at `/Review/{ticket_id}`, with 1–5 rating, optional 500-character comment, validation, and local-demo eligibility/refusal states.
- [x] Route inventory lists only the five scoped routes. `/` redirects to `/Register` and renders no homepage; no logout, error, marketplace, admin, or organizer page is provided.
- [x] Frontend-only boundary is explicit: no database, backend, API, real credentials, or real ticket/attendance/review writes; all sample events are fictional Philippine examples.
- [ ] Real authentication, shared capacity/duplicate enforcement, ticket issuance, live check-in/out, and durable review writes require the backend and are not part of this frontend-only scope.
