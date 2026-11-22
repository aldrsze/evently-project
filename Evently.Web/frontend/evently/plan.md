# Evently — Confirmed Narrow Scope

The user's latest confirmed scope is the only scope for this revision: an ASP.NET Core Razor Pages application targeting `net9.0`, listening locally on `http://0.0.0.0:5000` for LAN phones; guest self-registration at `/Register`; QR ticket display at `/Ticket/{ticket_id}`; staff login at `/Login`; staff phone-camera QR scan with check-in/check-out interface at `/Scan`; and post-event guest review submission at `/Review/{ticket_id}`.

No marketplace, general homepage, event discovery features beyond selecting an open sample event for registration, price/ticket purchasing, payment, attendee or organizer dashboards, admin web modules, logout page, or separate error page are in scope. Visiting `/` redirects to `/Register` and does not render a homepage.

The app is frontend-only, serves Razor pages and static files, and has no database, API, server-side session, or production business logic. All local persistence is conspicuously labelled as demonstration UI; it must never be presented as a real registration, ticket, check-in, login, or review. Store no entered passwords. The scanner uses the requested `html5-qrcode` browser library. Event samples remain fictional and Philippine-based.

Design remains focused on the workflow: mobile-first registration, a high-contrast QR ticket, a camera viewport with prominent arrival/departure/invalid states, and a simple 1–5 rating form. Use the provided blue-and-slate palette, Segoe UI/system typography, accessible labels, inline validation, visible focus, and reduced motion.

Razor route pages live under `Pages/`; shared markup is `Pages/Shared/_Layout.cshtml`; `wwwroot/js/app.js` and `wwwroot/js/data/store.js` power only local UI-preview interactions; `wwwroot/css/site.css` contains responsive styles; and `wwwroot/manus-routes.json` lists only the five scoped page routes. `Program.cs` only serves Razor pages/static assets and redirects `/` to `/Register`.
