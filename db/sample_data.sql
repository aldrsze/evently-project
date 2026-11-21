-- Fake rows for testing screens and the dashboard. Not real people.
-- Run after schema.sql and seed.sql. Needs empty tables (see the DELETE block below).
-- Ticket IDs are deterministic UUIDs so they can be tested with web links and QR codes.

USE evently_db;

-- Clean existing data in reverse order of foreign key dependencies.
DELETE FROM reviews;
DELETE FROM tickets;
DELETE FROM event_organizers;
DELETE FROM organizers;
DELETE FROM attendees;
DELETE FROM events;
DELETE FROM users WHERE username IN ('staff1', 'staff2');

-- Staff accounts for door check-in testing (password: admin123)
INSERT INTO users (username, password, role) VALUES
('staff1', '$2a$11$Zq5hVFTuuSmfdyUiCX9M7.CpLHM7iTkdmGHRGRKlpLYbJU6aZ8LVq', 'STAFF'),
('staff2', '$2a$11$Zq5hVFTuuSmfdyUiCX9M7.CpLHM7iTkdmGHRGRKlpLYbJU6aZ8LVq', 'STAFF');

-- QCU Campus Organization Events
INSERT INTO events (event_id, name, venue, description, event_date, capacity) VALUES
(1, 'QCU TechFest 2026: Code for the Community', 'San Bartolome Campus Auditorium', 'Annual tech conference hosted by the Association of Computer Studies Students (ACSS) featuring AI and web talks.', DATE_ADD(NOW(), INTERVAL 2 HOUR), 150),
(2, 'JPCS Hackathon: AI Para sa Bayan', 'Belmonte Hall Tech Lab 204', 'Hands-on coding challenge and project building organized by the Junior Philippine Computer Society (JPCS - QCU).', DATE_ADD(CURDATE(), INTERVAL 4 DAY) + INTERVAL 9 HOUR, 25),
(3, 'QCU Freshmen Leadership Summit', 'QCU Gymnasium', 'University-wide student leadership orientation and club recruitment hosted by the Supreme Student Council (SSC).', DATE_ADD(CURDATE(), INTERVAL -4 DAY) + INTERVAL 8 HOUR, 200),
(4, 'Peer Facilitators Mental Health & Wellness Assembly', 'Auditorium Audio-Visual Room', 'Interactive workshop on student well-being and campus life resilience organized by QCU Peer Circle.', DATE_ADD(CURDATE(), INTERVAL 10 DAY) + INTERVAL 13 HOUR, 50);

-- QCU Student Organization Leaders & Event Managers
INSERT INTO organizers (organizer_id, full_name, email, phone, organization) VALUES
(1, 'Danica Rose Bautista', 'danica.bautista@qcu.edu.ph', '09171234567', 'Association of Computer Studies Students (ACSS)'),
(2, 'Joshua Gabriel Santos', 'joshua.santos@qcu.edu.ph', '09189876543', 'Junior Philippine Computer Society (JPCS - QCU)'),
(3, 'Patricia Marie Macaraeg', 'patricia.macaraeg@qcu.edu.ph', '09223334455', 'QCU Supreme Student Council (SSC)'),
(4, 'Christian Dave Del Rosario', 'christian.delrosario@qcu.edu.ph', '09051112233', 'QCU Peer Facilitators Circle'),
(5, 'Nicole Angela Tolentino', 'nicole.tolentino@qcu.edu.ph', '09995556677', 'QCU Red Cross Youth');

-- Event-to-Organizer assignments
INSERT INTO event_organizers (event_id, organizer_id) VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3),
(3, 5),
(4, 4);

-- Filipino student attendees
INSERT INTO attendees (attendee_id, first_name, last_name, email, birthdate) VALUES
(1, 'Juan Carlos', 'Dela Cruz', 'juancarlos.delacruz@gmail.com', '2004-03-15'),
(2, 'Maria Kristina', 'Santos', 'maria.santos2004@yahoo.com', '2004-07-22'),
(3, 'Joshua Miguel', 'Reyes', 'joshua.reyes.qcu@gmail.com', '2003-11-09'),
(4, 'Alyssa Nicole', 'Ramos', 'alyssa.ramos@outlook.com', '2005-01-30'),
(5, 'Gabriel Paul', 'Villanueva', 'gabriel.villanueva@gmail.com', '2003-09-18'),
(6, 'Bea Bianca', 'Mendoza', 'bea.mendoza@gmail.com', '2004-05-12'),
(7, 'John Patrick', 'Garcia', 'johnpatrick.garcia@yahoo.com', '2003-12-05'),
(8, 'Sofia Louise', 'Fernandez', 'sofia.fernandez@gmail.com', '2005-04-19'),
(9, 'Christian Mark', 'Bautista', 'christian.bautista@outlook.com', '2002-10-25'),
(10, 'Kimberly Anne', 'Aquino', 'kimberly.aquino@gmail.com', '2004-08-14'),
(11, 'Angelo Joseph', 'Ocampo', 'angelo.ocampo@gmail.com', '2003-06-20'),
(12, 'Christine Joy', 'Castro', 'christinejoy.castro@yahoo.com', '2005-02-17'),
(13, 'Paolo Enrico', 'Salazar', 'paolo.salazar@gmail.com', '2004-11-28'),
(14, 'Maricar', 'Evangelista', 'maricar.evangelista@outlook.com', '2003-04-03'),
(15, 'Jerico Nathaniel', 'Dizon', 'jerico.dizon@gmail.com', '2004-09-08'),
(16, 'Rochelle Marie', 'Navarro', 'rochelle.navarro@yahoo.com', '2005-08-21'),
(17, 'Dominic Rafael', 'Pascual', 'dominic.pascual@gmail.com', '2003-03-11'),
(18, 'Katherine Mae', 'Soriano', 'katherine.soriano@gmail.com', '2004-12-04'),
(19, 'Lance Francis', 'David', 'lance.david@outlook.com', '2005-05-29'),
(20, 'Angelica Rose', 'Dimaculangan', 'angelica.dimaculangan@gmail.com', '2004-01-16');

-- EVENT 1: Active Event (Live Dashboard showcase: check-ins, check-outs, inside guests, not arrived)
INSERT INTO tickets (ticket_id, attendee_id, event_id, ticket_emailed, review_emailed, checked_in_at, checked_out_at) VALUES
('11111111-0001-4000-8000-000000000001', 1, 1, TRUE, TRUE, NOW() - INTERVAL 180 MINUTE, NOW() - INTERVAL 60 MINUTE),
('11111111-0002-4000-8000-000000000002', 2, 1, TRUE, TRUE, NOW() - INTERVAL 160 MINUTE, NOW() - INTERVAL 45 MINUTE),
('11111111-0003-4000-8000-000000000003', 3, 1, TRUE, FALSE, NOW() - INTERVAL 140 MINUTE, NULL),
('11111111-0004-4000-8000-000000000004', 4, 1, TRUE, FALSE, NOW() - INTERVAL 110 MINUTE, NULL),
('11111111-0005-4000-8000-000000000005', 5, 1, TRUE, FALSE, NOW() - INTERVAL 90 MINUTE, NULL),
('11111111-0006-4000-8000-000000000006', 6, 1, TRUE, FALSE, NOW() - INTERVAL 75 MINUTE, NULL),
('11111111-0007-4000-8000-000000000007', 7, 1, TRUE, FALSE, NOW() - INTERVAL 50 MINUTE, NULL),
('11111111-0008-4000-8000-000000000008', 8, 1, TRUE, FALSE, NOW() - INTERVAL 30 MINUTE, NULL),
('11111111-0009-4000-8000-000000000009', 9, 1, TRUE, FALSE, NOW() - INTERVAL 15 MINUTE, NULL),
('11111111-0010-4000-8000-000000000010', 10, 1, TRUE, FALSE, NOW() - INTERVAL 5 MINUTE, NULL),
('11111111-0011-4000-8000-000000000011', 11, 1, TRUE, FALSE, NULL, NULL),
('11111111-0012-4000-8000-000000000012', 12, 1, FALSE, FALSE, NULL, NULL),
('11111111-0013-4000-8000-000000000013', 13, 1, FALSE, FALSE, NULL, NULL);

-- EVENT 2: Upcoming Hackathon (Registrations ready for ticket emailing)
INSERT INTO tickets (ticket_id, attendee_id, event_id, ticket_emailed, review_emailed, checked_in_at, checked_out_at) VALUES
('22222222-0001-4000-8000-000000000001', 14, 2, FALSE, FALSE, NULL, NULL),
('22222222-0002-4000-8000-000000000002', 15, 2, FALSE, FALSE, NULL, NULL),
('22222222-0003-4000-8000-000000000003', 16, 2, TRUE, FALSE, NULL, NULL),
('22222222-0004-4000-8000-000000000004', 1, 2, TRUE, FALSE, NULL, NULL);

-- EVENT 3: Completed Summit (Full turnout, all left, ratings submitted)
INSERT INTO tickets (ticket_id, attendee_id, event_id, ticket_emailed, review_emailed, checked_in_at, checked_out_at) VALUES
('33333333-0001-4000-8000-000000000001', 17, 3, TRUE, TRUE, NOW() - INTERVAL 4 DAY + INTERVAL 1 HOUR, NOW() - INTERVAL 4 DAY + INTERVAL 6 HOUR),
('33333333-0002-4000-8000-000000000002', 18, 3, TRUE, TRUE, NOW() - INTERVAL 4 DAY + INTERVAL 1 HOUR, NOW() - INTERVAL 4 DAY + INTERVAL 7 HOUR),
('33333333-0003-4000-8000-000000000003', 19, 3, TRUE, TRUE, NOW() - INTERVAL 4 DAY + INTERVAL 2 HOUR, NOW() - INTERVAL 4 DAY + INTERVAL 6 HOUR),
('33333333-0004-4000-8000-000000000004', 20, 3, TRUE, TRUE, NOW() - INTERVAL 4 DAY + INTERVAL 2 HOUR, NOW() - INTERVAL 4 DAY + INTERVAL 7 HOUR);

-- EVENT 4: Upcoming Seminar (Fresh registrations)
INSERT INTO tickets (ticket_id, attendee_id, event_id, ticket_emailed, review_emailed, checked_in_at, checked_out_at) VALUES
('44444444-0001-4000-8000-000000000001', 2, 4, FALSE, FALSE, NULL, NULL),
('44444444-0002-4000-8000-000000000002', 4, 4, FALSE, FALSE, NULL, NULL),
('44444444-0003-4000-8000-000000000003', 6, 4, FALSE, FALSE, NULL, NULL);

-- Reviews submitted by attendees who checked in
INSERT INTO reviews (ticket_id, rating, comment, submitted_at) VALUES
('11111111-0001-4000-8000-000000000001', 5, 'Napakabilis ng QR check-in sa entrance ng auditorium, super organized ng ACSS!', NOW() - INTERVAL 40 MINUTE),
('11111111-0002-4000-8000-000000000002', 4, 'Maganda yung keynote session on AI web dev, pero medyo malamig sa auditorium.', NOW() - INTERVAL 25 MINUTE),
('33333333-0001-4000-8000-000000000001', 5, 'Super inspiring ng leadership talks. Looking forward sa next assembly ng SSC!', NOW() - INTERVAL 3 DAY),
('33333333-0002-4000-8000-000000000002', 4, 'Very engaging ang open forum and icebreakers for freshmen.', NOW() - INTERVAL 3 DAY),
('33333333-0003-4000-8000-000000000003', 5, 'Clear ang directions and accommodated lahat ng student clubs. Great job!', NOW() - INTERVAL 3 DAY),
('33333333-0004-4000-8000-000000000004', 4, 'Overall great summit! Sana may snacks next time pero ayos ang experience.', NOW() - INTERVAL 3 DAY);
