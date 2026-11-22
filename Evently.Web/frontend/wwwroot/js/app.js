import { events, readDemoState, writeDemoState } from './data/store.js';

const app = document.querySelector('#app');
const demoNotice = 'Frontend demonstration only. No registration, ticket, scan, login, or review is written to the Evently database.';
let state = readDemoState();
let scanner = null;
let scanLocked = false;
const $ = (selector, root = document) => root.querySelector(selector);
const esc = (value = '') => String(value).replace(/[&<>"']/g, ch => ({ '&':'&amp;', '<':'&lt;', '>':'&gt;', '"':'&quot;', "'":'&#39;' }[ch]));
const eventFor = id => events.find(event => String(event.id) === String(id));
const ticketFor = id => state.tickets.find(ticket => ticket.id === id);
const formatDate = value => new Intl.DateTimeFormat('en-PH', { dateStyle:'long', timeStyle:'short', timeZone:'Asia/Manila' }).format(new Date(value));
const persist = () => writeDemoState(state);
const pathParts = () => decodeURIComponent(location.pathname).split('/').filter(Boolean);

function header({ staff = false } = {}) {
  return `<header class="site-header"><a class="wordmark" href="/" aria-label="Evently home"><span class="mark" aria-hidden="true">E</span><span>Evently</span></a><nav aria-label="Main navigation"><a href="/Register">Guest registration</a>${staff ? '<a href="/Scan">Staff scan</a>' : '<a href="/Login">Staff login</a>'}</nav></header>`;
}
function notice() { return `<aside class="demo-notice" role="note"><strong>UI demonstration only</strong><span>${demoNotice}</span></aside>`; }
function layout(content, options = {}) {
  return `${header(options)}<main id="main" class="page-wrap">${content}</main><footer class="site-footer"><span>Evently · Event registration and attendance</span><span>Fictional sample events · Philippines</span></footer>`;
}
function registration() {
  const params = new URLSearchParams(location.search);
  const requestedId = params.get('eventId');
  const selected = eventFor(requestedId);
  const openEvents = events.filter(event => new Date(event.date) > new Date() && event.registered < event.capacity);
  if (requestedId && !openEvents.some(event => String(event.id) === String(requestedId))) {
    const reason = selected && selected.registered >= selected.capacity ? 'full-event' : 'unavailable-event';
    return errorPage(reason);
  }
  return layout(`<section class="narrow-page"><a class="back-link" href="/Register">← Guest registration</a><p class="eyebrow">Guest self-registration</p><h1>Register for an event</h1>${notice()}<form id="registration-form" class="form-card" novalidate><div class="field"><label for="event-id">Event</label><select id="event-id" name="eventId" required><option value="">Select an open sample event</option>${openEvents.map(event => `<option value="${event.id}" ${selected?.id === event.id ? 'selected' : ''}>${esc(event.name)} · ${esc(event.city)}</option>`).join('')}</select><p class="field-error" data-error="eventId"></p></div><div id="selected-event" class="selected-event">${selected && openEvents.some(event => event.id === selected.id) ? `<strong>${esc(selected.name)}</strong><span>${esc(selected.venue)}, ${esc(selected.city)} · ${esc(formatDate(selected.date))} (sample)</span>` : '<span>Choose an open sample event to review its details.</span>'}</div><div class="field-grid"><div class="field"><label for="first-name">First name</label><input id="first-name" name="firstName" autocomplete="given-name" required maxlength="50"><p class="field-error" data-error="firstName"></p></div><div class="field"><label for="last-name">Last name</label><input id="last-name" name="lastName" autocomplete="family-name" required maxlength="50"><p class="field-error" data-error="lastName"></p></div></div><div class="field"><label for="email">Email address</label><input id="email" name="email" type="email" autocomplete="email" required maxlength="100"><p class="field-error" data-error="email"></p></div><div class="field"><label for="birthdate">Birthdate <span class="optional">(optional)</span></label><input id="birthdate" name="birthdate" type="date" autocomplete="bday"><p class="field-error" data-error="birthdate"></p></div><button class="button primary full" type="submit">Continue to demo ticket</button><p class="form-note">Use fictional details only. A frontend preview is created in this browser; it is not a real registration.</p></form><div id="form-result" aria-live="polite"></div></section>`);
}
function createPreviewTicket(formData) {
  const event = eventFor(formData.eventId);
  const id = crypto.randomUUID();
  const ticket = { id, eventId: event.id, attendee: { firstName: formData.firstName, lastName: formData.lastName, email: formData.email, birthdate: formData.birthdate || null }, checkedInAt: null, checkedOutAt: null, review: null, preview: true };
  state.tickets.push(ticket);
  persist();
  location.assign(`/Ticket/${encodeURIComponent(id)}`);
}
function ticketPage(id) {
  const ticket = ticketFor(id);
  const event = ticket && eventFor(ticket.eventId);
  if (!ticket || !event) return errorPage('missing-ticket');
  return layout(`<section class="narrow-page"><p class="eyebrow">QR ticket</p><h1>Your event ticket</h1>${notice()}<article class="ticket-card"><div class="ticket-heading"><span class="sample-tag">DEMO TICKET — NOT VALID FOR ENTRY</span><h2>${esc(event.name)}</h2></div><div class="qr-frame"><div id="ticket-qr" aria-label="QR code for this frontend demo ticket"></div></div><p class="ticket-id">Ticket ID <code>${esc(ticket.id)}</code></p><div class="ticket-info"><div><span>Attendee</span><strong>${esc(ticket.attendee.firstName)} ${esc(ticket.attendee.lastName)}</strong></div><div><span>Venue</span><strong>${esc(event.venue)}, ${esc(event.city)}</strong></div><div><span>Event date</span><strong>${esc(formatDate(event.date))} · Sample date</strong></div></div><p class="save-instruction">Screenshot or save this page for the interface preview. This QR code is not recognized by a live Evently system.</p><a class="button secondary full" href="/Review/${encodeURIComponent(ticket.id)}">Open rating page</a></article></section>`);
}
function loginPage() {
  return layout(`<section class="narrow-page"><p class="eyebrow">Staff access</p><h1>Staff login</h1>${notice()}<form id="login-form" class="form-card" novalidate><div class="field"><label for="username">Username</label><input id="username" name="username" autocomplete="username" required><p class="field-error" data-error="username"></p></div><div class="field"><label for="password">Password</label><input id="password" name="password" type="password" autocomplete="current-password" required><p class="field-error" data-error="password"></p></div><button class="button primary full" type="submit">Sign in</button><p class="form-note">Authentication is not connected in this frontend-only project. Do not enter real credentials; this form will not create a session.</p></form><div id="form-result" aria-live="polite"></div><a class="button secondary full preview-link" href="/Scan">Open scan interface preview (no sign-in)</a></section>`);
}
function scanPage() {
  return layout(`<section class="scan-page"><p class="eyebrow">Staff QR scanner</p><h1>Scan attendee ticket</h1>${notice()}<p class="scan-guidance">Allow camera access when prompted. No event is selected here; in Evently, the ticket identifies its event.</p><div class="scan-count" aria-live="polite"><span>People currently inside</span><strong id="inside-count">—</strong><small>Local preview count only · not a live venue count</small></div><div class="scan-shell"><div id="qr-reader" aria-label="QR camera scanning viewport"></div></div><button class="button primary full" id="camera-start" type="button">Start camera scanner</button><button class="button secondary full" id="camera-stop" type="button" disabled>Stop camera</button><div id="scan-result" class="scan-result neutral" role="status" aria-live="assertive"><strong>Ready to scan</strong><span>Camera access has not started.</span></div><p class="form-note">The preview recognizes only demo tickets created in this browser. Scan state is local and does not check in a real attendee.</p></section>` , { staff:true });
}
function reviewPage(id) {
  const ticket = ticketFor(id);
  const event = ticket && eventFor(ticket.eventId);
  if (!ticket || !event) return errorPage('invalid-ticket');
  if (!ticket.checkedInAt) return layout(`<section class="narrow-page"><p class="eyebrow">Event feedback</p><h1>Rating unavailable</h1>${notice()}<div class="error-panel" role="status"><strong>This ticket has not been checked in.</strong><p>Only a checked-in ticket can submit an event rating. This ticket is a frontend demo, not a verified Evently record.</p></div><a class="button secondary full" href="/Ticket/${encodeURIComponent(id)}">Return to demo ticket</a></section>`);
  if (ticket.review) return layout(`<section class="narrow-page"><p class="eyebrow">Event feedback</p><h1>Rating already submitted</h1>${notice()}<div class="info-panel" role="status">This demo ticket already has a local preview rating.</div></section>`);
  return layout(`<section class="narrow-page"><p class="eyebrow">Event feedback</p><h1>Rate your event</h1><p class="lead small-lead">${esc(event.name)} · Demo event</p>${notice()}<form id="review-form" class="form-card" novalidate data-ticket-id="${esc(id)}"><fieldset class="rating-field"><legend>Your rating</legend><div class="rating-options">${[1,2,3,4,5].map(value => `<label><input type="radio" name="rating" value="${value}"><span>${value} ${value === 1 ? 'star' : 'stars'}</span></label>`).join('')}</div><p class="field-error" data-error="rating"></p></fieldset><div class="field"><label for="comment">Comment <span class="optional">(optional, up to 500 characters)</span></label><textarea id="comment" name="comment" maxlength="500" rows="5"></textarea><div class="field-row"><p class="field-error" data-error="comment"></p><span id="comment-count">0 / 500</span></div></div><button class="button primary full" type="submit">Submit demo rating</button><p class="form-note">This stores a clearly labelled rating preview in this browser only; no review is sent to the Evently database.</p></form><div id="form-result" aria-live="polite"></div></section>`);
}
function errorPage(reason = 'not-found') {
  const messages = {
    'invalid-ticket': ['Invalid Ticket.', 'The ticket ID is not available in this frontend demo.'],
    'missing-ticket': ['Ticket not found.', 'Open a ticket link created in this browser demo.'],
    'full-event': ['Event is full.', 'This sample event has reached its illustrative capacity and is not open for registration.'],
    'unavailable-event': ['Event unavailable.', 'This event is not an open sample event. Return to the event list to choose an available event.'],
    'duplicate-registration': ['Registration unavailable.', 'This frontend demo cannot verify existing attendee registrations.'],
    unauthorized: ['Sign-in required.', 'This frontend does not authenticate staff or authorize protected operations.'],
    'invalid-rating': ['Rating unavailable.', 'A valid checked-in demo ticket is required to preview a rating.'],
    'not-found': ['Page not found.', 'The requested page is not part of the Evently website frontend.']
  };
  const [title, message] = messages[reason] || messages['not-found'];
  return layout(`<section class="narrow-page"><p class="eyebrow">Evently</p><h1>${esc(title)}</h1>${notice()}<div class="error-panel"><p>${esc(message)}</p></div><a class="button primary full" href="/Register">Return to registration</a></section>`);
}
function render() {
  if (scanner) stopScanner();
  const parts = pathParts();
  const root = parts[0]?.toLowerCase() || '';
  if (root === 'register') app.innerHTML = registration();
  else if (root === 'ticket') app.innerHTML = ticketPage(parts[1] || '');
  else if (root === 'login') app.innerHTML = loginPage();
  else if (root === 'scan') app.innerHTML = scanPage();
  else if (root === 'review') app.innerHTML = reviewPage(parts[1] || '');
  else app.innerHTML = errorPage('not-found');
  document.title = `${root.charAt(0).toUpperCase() + root.slice(1)} · Evently`;
  if (root === 'ticket') drawTicketQr(parts[1]);
  if (root === 'scan') bindScanner();
  bindForms();
  bindControls();
}
function drawTicketQr(id) {
  const node = $('#ticket-qr');
  if (!node || !id) return;
  const value = ticketFor(id)?.id;
  if (!value) return;
  if (window.QRCode) new QRCode(node, { text:value, width:240, height:240, correctLevel:QRCode.CorrectLevel.M });
  else node.textContent = 'QR image library unavailable. Ticket ID: ' + value;
}
function clearErrors(form) { form.querySelectorAll('.field-error').forEach(node => { node.textContent = ''; }); }
function setError(form, name, message) { const node = form.querySelector(`[data-error="${name}"]`); if (node) node.textContent = message; }
function bindForms() {
  const select = $('#event-id');
  select?.addEventListener('change', () => {
    const event = eventFor(select.value);
    const node = $('#selected-event');
    if (node) node.innerHTML = event ? `<strong>${esc(event.name)}</strong><span>${esc(event.venue)}, ${esc(event.city)} · ${esc(formatDate(event.date))} (sample)</span>` : '<span>Choose an open sample event to review its details.</span>';
  });
  $('#registration-form')?.addEventListener('submit', event => {
    event.preventDefault();
    const form = event.currentTarget;
    clearErrors(form);
    const data = Object.fromEntries(new FormData(form).entries());
    let valid = true;
    const sampleEvent = eventFor(data.eventId);
    if (!sampleEvent) { setError(form, 'eventId', 'Select an event from the list.'); valid = false; }
    for (const [key, label] of [['firstName','First name'], ['lastName','Last name']]) {
      if (!String(data[key] || '').trim()) { setError(form, key, `${label} is required.`); valid = false; }
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(data.email || '')) { setError(form, 'email', 'Enter a valid email address.'); valid = false; }
    if (!valid) return;
    const openEvent = events.find(item => item.id === Number(data.eventId) && item.registered < item.capacity && new Date(item.date) > new Date());
    if (!openEvent) { setError(form, 'eventId', 'This sample event is not open.'); return; }
    createPreviewTicket({ ...data, firstName:data.firstName.trim(), lastName:data.lastName.trim(), email:data.email.trim() });
  });
  $('#login-form')?.addEventListener('submit', event => {
    event.preventDefault();
    const form = event.currentTarget;
    clearErrors(form);
    const data = new FormData(form);
    let valid = true;
    for (const name of ['username','password']) if (!String(data.get(name) || '').trim()) { setError(form, name, 'This field is required.'); valid = false; }
    if (valid) $('#form-result').innerHTML = '<div class="error-panel" role="alert"><strong>Authentication is unavailable.</strong><p>No session was created. This frontend has no authentication service.</p></div>';
  });
  $('#review-form')?.addEventListener('submit', event => {
    event.preventDefault();
    const form = event.currentTarget;
    clearErrors(form);
    const rating = form.querySelector('[name="rating"]:checked')?.value;
    if (!rating) { setError(form, 'rating', 'Select a rating from 1 to 5.'); return; }
    const ticket = ticketFor(form.dataset.ticketId);
    if (!ticket?.checkedInAt) { form.innerHTML = '<div class="error-panel" role="alert"><strong>Rating unavailable.</strong><p>This ticket is not marked checked in in the local demo.</p></div>'; return; }
    if (ticket.review) { location.assign(`/Review/${encodeURIComponent(ticket.id)}`); return; }
    ticket.review = { rating:Number(rating), comment:$('#comment').value.slice(0, 500), preview:true };
    state.reviews.push({ ticketId:ticket.id, ...ticket.review });
    persist();
    form.innerHTML = '<div class="success-panel" role="status"><strong>Thank you for your feedback.</strong><p>Your rating is stored as a local preview only; it has not been submitted to Evently.</p></div>';
  });
  $('#comment')?.addEventListener('input', event => { const counter = $('#comment-count'); if (counter) counter.textContent = `${event.currentTarget.value.length} / 500`; });
}
function bindControls() {
  $('#camera-start')?.addEventListener('click', startScanner);
  $('#camera-stop')?.addEventListener('click', stopScanner);
}
function bindScanner() { scanLocked = false; }
async function startScanner() {
  const result = $('#scan-result');
  if (!window.Html5Qrcode) { showScanResult('danger', 'Scanner library unavailable', 'The QR scanner dependency did not load.'); return; }
  try {
    scanner = new Html5Qrcode('qr-reader');
    await scanner.start({ facingMode:'environment' }, { fps:10, qrbox:{ width:240, height:240 } }, onQrDecoded, () => {});
    $('#camera-start').disabled = true;
    $('#camera-stop').disabled = false;
    showScanResult('neutral', 'Camera ready', 'Point the camera at a demo ticket QR code.');
  } catch {
    scanner = null;
    showScanResult('danger', 'Camera unavailable', 'Camera access was denied or is not available. Check browser permissions and try again.');
  }
}
async function stopScanner() {
  if (scanner) {
    try { if (scanner.isScanning) await scanner.stop(); await scanner.clear(); } catch { /* camera may already be stopped */ }
    scanner = null;
  }
  const start = $('#camera-start'); if (start) start.disabled = false;
  const stop = $('#camera-stop'); if (stop) stop.disabled = true;
}
function showScanResult(kind, title, detail) {
  const result = $('#scan-result');
  if (!result) return;
  result.className = `scan-result ${kind}`;
  result.innerHTML = `<strong>${esc(title)}</strong><span>${esc(detail)}</span>`;
}
function onQrDecoded(decodedText) {
  if (scanLocked) return;
  scanLocked = true;
  const scanned = String(decodedText).trim();
  const ticket = state.tickets.find(item => item.id === scanned);
  if (!ticket) showScanResult('danger', 'Invalid Ticket.', 'This QR code is not a ticket in the local frontend demo.');
  else if (!ticket.checkedInAt) {
    ticket.checkedInAt = new Date().toISOString();
    state.scans.unshift({ ticketId:ticket.id, action:'check-in', time:ticket.checkedInAt });
    persist();
    showScanResult('success', `Welcome, ${ticket.attendee.firstName} ${ticket.attendee.lastName}.`, `${eventFor(ticket.eventId)?.name || 'Sample event'} · ${insideCount(ticket.eventId)} currently inside for this event (local preview).`);
  } else if (!ticket.checkedOutAt) {
    ticket.checkedOutAt = new Date().toISOString();
    state.scans.unshift({ ticketId:ticket.id, action:'check-out', time:ticket.checkedOutAt });
    persist();
    showScanResult('warning', `Goodbye, ${ticket.attendee.firstName} ${ticket.attendee.lastName}.`, `${eventFor(ticket.eventId)?.name || 'Sample event'} · ${insideCount(ticket.eventId)} currently inside for this event (local preview).`);
  } else showScanResult('danger', 'Ticket already used.', 'This demo ticket has already completed its check-in and check-out preview.');
  const count = $('#inside-count'); if (count && ticket) count.textContent = insideCount(ticket.eventId);
  window.setTimeout(() => { scanLocked = false; }, 3000);
}
function insideCount(eventId) { return state.tickets.filter(ticket => String(ticket.eventId) === String(eventId) && ticket.checkedInAt && !ticket.checkedOutAt).length; }
window.addEventListener('pageshow', () => { state = readDemoState(); });
window.addEventListener('beforeunload', () => { if (scanner) stopScanner(); });
render();
