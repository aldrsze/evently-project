export const events = [
  { id: 1, name: 'University Student Leadership Seminar', venue: 'Quezon City Campus Hall', city: 'Quezon City', province: 'Metro Manila', date: '2026-11-28T09:00:00+08:00', description: 'A fictional sample seminar for student leaders and campus organizations.', capacity: 120, registered: 74 },
  { id: 2, name: 'Philippine Youth Technology Conference', venue: 'Quezon City Convention Center', city: 'Quezon City', province: 'Metro Manila', date: '2026-12-04T09:30:00+08:00', description: 'A fictional sample conference about technology, learning, and youth communities.', capacity: 200, registered: 200 },
  { id: 3, name: 'Campus Innovation and Research Expo', venue: 'Manila Science Campus', city: 'Manila', province: 'Metro Manila', date: '2026-12-10T10:00:00+08:00', description: 'A fictional sample showcase for student research and campus innovation.', capacity: 180, registered: 63 },
  { id: 4, name: 'Student Cultural Festival', venue: 'Cebu City University Grounds', city: 'Cebu City', province: 'Cebu', date: '2026-12-15T13:00:00+08:00', description: 'A fictional sample gathering celebrating student arts and Filipino culture.', capacity: 300, registered: 191 },
  { id: 5, name: 'Filipino Entrepreneurship Workshop', venue: 'Makati Learning Center', city: 'Makati City', province: 'Metro Manila', date: '2027-01-08T09:00:00+08:00', description: 'A fictional sample workshop for aspiring Filipino entrepreneurs.', capacity: 90, registered: 41 },
  { id: 6, name: 'Digital Skills and IT Career Seminar', venue: 'Pasig Community Hall', city: 'Pasig City', province: 'Metro Manila', date: '2027-01-16T13:30:00+08:00', description: 'A fictional sample career seminar for students exploring digital skills.', capacity: 150, registered: 87 },
  { id: 7, name: 'Community Environmental Awareness Program', venue: 'Davao City Civic Center', city: 'Davao City', province: 'Davao del Sur', date: '2027-01-23T08:00:00+08:00', description: 'A fictional sample community program focused on environmental awareness.', capacity: 160, registered: 122 },
  { id: 8, name: 'Philippine Student Research Presentation', venue: 'Baguio Academic Center', city: 'Baguio City', province: 'Benguet', date: '2027-02-03T09:00:00+08:00', description: 'A fictional sample student research presentation and learning session.', capacity: 110, registered: 56 },
  { id: 9, name: 'Campus Sports and Wellness Day', venue: 'Iloilo Community Sports Grounds', city: 'Iloilo City', province: 'Iloilo', date: '2027-02-12T07:00:00+08:00', description: 'A fictional sample campus sports and wellness day for the local community.', capacity: 240, registered: 109 },
  { id: 10, name: 'Filipino Arts and Culture Exhibition', venue: 'Quezon City Arts Center', city: 'Quezon City', province: 'Metro Manila', date: '2027-02-20T10:00:00+08:00', description: 'A fictional sample exhibition featuring Filipino student and community creativity.', capacity: 130, registered: 75 }
];

export const storeKey = 'evently-prd-frontend-demo-v1';

export function readDemoState() {
  try {
    const parsed = JSON.parse(localStorage.getItem(storeKey) || '{}');
    return {
      tickets: Array.isArray(parsed.tickets) ? parsed.tickets : [],
      reviews: Array.isArray(parsed.reviews) ? parsed.reviews : [],
      scans: Array.isArray(parsed.scans) ? parsed.scans : []
    };
  } catch {
    return { tickets: [], reviews: [], scans: [] };
  }
}

export function writeDemoState(state) {
  try {
    localStorage.setItem(storeKey, JSON.stringify(state));
  } catch {
    // The preview continues without local persistence when storage is unavailable.
  }
}
